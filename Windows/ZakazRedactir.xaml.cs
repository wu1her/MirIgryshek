using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using MirIgryshek.Models;

namespace MirIgryshek.Windows
{
    public partial class ZakazRedactir : Window
    {
        private int? nomerZakaza;
        private ObservableCollection<ZakazTovar> sostav;

        public ZakazRedactir(int? nomer)
        {
            InitializeComponent();

            nomerZakaza = nomer;
            sostav = new ObservableCollection<ZakazTovar>();
            Tovary.ItemsSource = sostav;

            Title = nomer == null
                ? "Добавление заказа"
                : "Редактирование заказа";

            try
            {
                using (var db = new AppDbContext())
                {
                    Polzovatel.ItemsSource = db.Polzovatel.ToList();
                    Status.ItemsSource = db.Status.ToList();
                    Pynkt.ItemsSource = db.Pynkti.ToList();

                    if (nomerZakaza == null)
                    {
                        Nomer.Text = "Автоматически";
                        DataZakaza.SelectedDate = DateTime.Today;
                        DataDostavka.SelectedDate = DateTime.Today;

                        Kod.Text = checked(
                            (db.Zakaz.Max(zakaz => (int?)zakaz.Kod) ?? 0) + 1
                        ).ToString();
                    }
                    else
                    {
                        var zakaz = db.Zakaz.Find(nomerZakaza.Value);

                        if (zakaz == null)
                            throw new Exception("Заказ уже удалён.");

                        Nomer.Text = zakaz.Nomer.ToString();
                        Kod.Text = zakaz.Kod.ToString();
                        DataZakaza.SelectedDate = zakaz.DataZakaza;
                        DataDostavka.SelectedDate = zakaz.DataDostavka;

                        Polzovatel.SelectedValue = db.PolzovatelZakaz
                            .Where(svyaz => svyaz.Nomer == zakaz.Nomer)
                            .Select(svyaz => svyaz.Login)
                            .FirstOrDefault();

                        Status.SelectedValue = db.ZakazStatus
                            .Where(svyaz => svyaz.Nomer == zakaz.Nomer)
                            .Select(svyaz => (int?)svyaz.IdStatus)
                            .FirstOrDefault();

                        Pynkt.SelectedValue = db.ZakazPynkti
                            .Where(svyaz => svyaz.Nomer == zakaz.Nomer)
                            .Select(svyaz => (int?)svyaz.IdPynkt)
                            .FirstOrDefault();

                        var tovary = db.ZakazTovar
                            .AsNoTracking()
                            .Where(svyaz => svyaz.Nomer == zakaz.Nomer)
                            .ToList();

                        foreach (var tovar in tovary)
                            sostav.Add(tovar);
                    }
                }
            }
            catch (Exception oshibka)
            {
                SaveKnopkaNedostupna();

                MessageBox.Show(
                    oshibka.GetBaseException().Message,
                    "Ошибка загрузки заказа",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void SaveKnopkaNedostupna()
        {
            Tovary.IsReadOnly = true;
            dannieZagruzheni = false;
        }

        private bool dannieZagruzheni = true;

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!dannieZagruzheni)
                return;

            try
            {
                var errors = new StringBuilder();

                bool yacheikaGotova = Tovary.CommitEdit(
                    DataGridEditingUnit.Cell, true);

                bool strokaGotova = Tovary.CommitEdit(
                    DataGridEditingUnit.Row, true);

                if (!yacheikaGotova || !strokaGotova)
                    errors.AppendLine(
                        "Исправьте данные в таблице. Количество должно быть целым числом.");

                if (Polzovatel.SelectedItem == null)
                    errors.AppendLine("Выберите пользователя.");

                if (Status.SelectedItem == null)
                    errors.AppendLine("Выберите статус.");

                if (Pynkt.SelectedItem == null)
                    errors.AppendLine("Выберите пункт выдачи.");

                int kod;

                if (!int.TryParse(Kod.Text, out kod) || kod <= 0)
                    errors.AppendLine("Введите код получения целым числом больше 0.");

                if (DataZakaza.SelectedDate == null)
                    errors.AppendLine("Выберите корректную дату заказа.");

                if (DataDostavka.SelectedDate == null)
                    errors.AppendLine("Выберите корректную дату доставки.");

                if (DataZakaza.SelectedDate != null
                    && DataDostavka.SelectedDate != null
                    && DataDostavka.SelectedDate.Value.Date
                        < DataZakaza.SelectedDate.Value.Date)
                {
                    errors.AppendLine(
                        "Дата доставки не может быть раньше даты заказа.");
                }

                if (sostav.Count == 0)
                    errors.AppendLine("Добавьте хотя бы один товар.");

                foreach (var tovar in sostav)
                {
                    if (string.IsNullOrWhiteSpace(tovar.Articul))
                        errors.AppendLine("Укажите артикул в каждой строке.");

                    if (tovar.Kolichestvo <= 0)
                        errors.AppendLine(
                            "Количество каждого товара должно быть больше 0.");
                }

                var povtori = sostav
                    .Where(tovar => !string.IsNullOrWhiteSpace(tovar.Articul))
                    .GroupBy(
                        tovar => tovar.Articul.Trim(),
                        StringComparer.OrdinalIgnoreCase)
                    .Any(gruppa => gruppa.Count() > 1);

                if (povtori)
                    errors.AppendLine(
                        "Один артикул должен занимать одну строку. Увеличьте количество.");

                using (var db = new AppDbContext())
                {
                    foreach (var tovar in sostav)
                    {
                        if (string.IsNullOrWhiteSpace(tovar.Articul))
                            continue;

                        var articul = tovar.Articul.Trim();

                        if (!db.Tovar.Any(zapis => zapis.Articul == articul))
                            errors.AppendLine("Товар не найден: " + articul);
                    }

                    if (errors.Length > 0)
                    {
                        MessageBox.Show(
                            errors.ToString(),
                            "Ошибки заполнения",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);

                        return;
                    }

                    using (var tranzakcia = db.Database.BeginTransaction())
                    {
                        Zakaz zapis;

                        if (nomerZakaza == null)
                        {
                            zapis = new Zakaz
                            {
                                Nomer = checked(
                                    (db.Zakaz.Max(
                                        zakaz => (int?)zakaz.Nomer) ?? 0) + 1)
                            };

                            db.Zakaz.Add(zapis);
                        }
                        else
                        {
                            zapis = db.Zakaz.Find(nomerZakaza.Value);

                            if (zapis == null)
                                throw new Exception("Заказ уже удалён.");

                            db.ZakazTovar.RemoveRange(
                                db.ZakazTovar.Where(
                                    svyaz => svyaz.Nomer == zapis.Nomer));

                            db.ZakazStatus.RemoveRange(
                                db.ZakazStatus.Where(
                                    svyaz => svyaz.Nomer == zapis.Nomer));

                            db.ZakazPynkti.RemoveRange(
                                db.ZakazPynkti.Where(
                                    svyaz => svyaz.Nomer == zapis.Nomer));

                            db.PolzovatelZakaz.RemoveRange(
                                db.PolzovatelZakaz.Where(
                                    svyaz => svyaz.Nomer == zapis.Nomer));
                        }

                        zapis.Kod = kod;
                        zapis.DataZakaza = DataZakaza.SelectedDate.Value.Date;
                        zapis.DataDostavka = DataDostavka.SelectedDate.Value.Date;

                        db.SaveChanges();

                        db.PolzovatelZakaz.Add(new PolzovatelZakaz
                        {
                            Nomer = zapis.Nomer,
                            Login = (string)Polzovatel.SelectedValue
                        });

                        db.ZakazStatus.Add(new ZakazStatus
                        {
                            Nomer = zapis.Nomer,
                            IdStatus = (int)Status.SelectedValue
                        });

                        db.ZakazPynkti.Add(new ZakazPynkti
                        {
                            Nomer = zapis.Nomer,
                            IdPynkt = (int)Pynkt.SelectedValue
                        });

                        foreach (var tovar in sostav)
                        {
                            db.ZakazTovar.Add(new ZakazTovar
                            {
                                Nomer = zapis.Nomer,
                                Articul = tovar.Articul.Trim(),
                                Kolichestvo = tovar.Kolichestvo
                            });
                        }

                        db.SaveChanges();
                        tranzakcia.Commit();
                    }
                }

                DialogResult = true;
            }
            catch (Exception oshibka)
            {
                MessageBox.Show(
                    oshibka.GetBaseException().Message,
                    "Ошибка сохранения заказа",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}