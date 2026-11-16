using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using MirIgryshek.Models;

namespace MirIgryshek.Windows
{
    public partial class Zakazi : Window
    {
        private bool pravoredactirovania;

        public Zakazi(string fio, bool pravo)
        {
            InitializeComponent();

            UserBlock.Text = fio;
            pravoredactirovania = pravo;

            if (pravo)
            {
                ZakazAdd.Visibility = Visibility.Visible;
                ZakazDelete.Visibility = Visibility.Visible;
            }

            ZagruzitZakazi();
        }

        private void ZagruzitZakazi()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var zakazi = db.Zakaz
                        .Include(zakaz => zakaz.ZakazStatus)
                        .ThenInclude(svyaz => svyaz.IdStatusNavigation)
                        .Include(zakaz => zakaz.ZakazTovar)
                        .Include(zakaz => zakaz.PolzovatelZakaz)
                        .OrderBy(zakaz => zakaz.Nomer)
                        .ToList();

                    var pynkti = db.ZakazPynkti
                        .Include(svyaz => svyaz.IdPynktNavigation)
                        .ToList();

                    SpisokZakazi.ItemsSource = zakazi.Select(zakaz => new
                    {
                        Nomer = zakaz.Nomer,

                        Login = string.Join(", ",
                            zakaz.PolzovatelZakaz.Select(
                                svyaz => svyaz.Login)),

                        Tovar = string.Join(", ",
                            zakaz.ZakazTovar.Select(
                                svyaz => svyaz.Articul
                                    + " × " + svyaz.Kolichestvo)),

                        Pynkt = pynkti
                            .Where(svyaz => svyaz.Nomer == zakaz.Nomer)
                            .Select(svyaz => svyaz.IdPynktNavigation.Adres)
                            .FirstOrDefault(),

                        Status = zakaz.ZakazStatus
                            .Select(svyaz => svyaz.IdStatusNavigation.Ima)
                            .FirstOrDefault(),

                        Kod = zakaz.Kod,
                        DataZakaza = zakaz.DataZakaza,
                        DataDostavka = zakaz.DataDostavka
                    }).ToList();
                }
            }
            catch (Exception oshibka)
            {
                MessageBox.Show(
                    oshibka.GetBaseException().Message,
                    "Ошибка загрузки заказов",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ZakazAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!pravoredactirovania)
                return;

            var okno = new ZakazRedactir(null);

            if (okno.ShowDialog() == true)
                ZagruzitZakazi();
        }

        private void SpisokZakazi_MouseDoubleClick(
            object sender, MouseButtonEventArgs e)
        {
            var stroka = ItemsControl.ContainerFromElement(
                SpisokZakazi,
                e.OriginalSource as DependencyObject) as DataGridRow;

            if (stroka == null || SpisokZakazi.SelectedValue == null)
                return;

            int nomer = (int)SpisokZakazi.SelectedValue;

            if (pravoredactirovania)
            {
                var okno = new ZakazRedactir(nomer);

                if (okno.ShowDialog() == true)
                    ZagruzitZakazi();
            }
            else
            {
                try
                {
                    using (var db = new AppDbContext())
                    {
                        var zakaz = db.Zakaz.Find(nomer);

                        if (zakaz == null)
                            throw new Exception("Заказ уже удалён.");

                        var okno = new OknoZakaza(zakaz);
                        okno.ShowDialog();
                    }
                }
                catch (Exception oshibka)
                {
                    MessageBox.Show(
                        oshibka.GetBaseException().Message,
                        "Ошибка просмотра заказа",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }

        private void ZakazDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!pravoredactirovania)
                return;

            if (SpisokZakazi.SelectedValue == null)
            {
                MessageBox.Show(
                    "Выберите заказ в таблице.",
                    "Удаление заказа",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                return;
            }

            int nomer = (int)SpisokZakazi.SelectedValue;

            if (MessageBox.Show(
                "Удалить заказ № " + nomer + " и его состав?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning) != MessageBoxResult.Yes)
            {
                return;
            }

            try
            {
                using (var db = new AppDbContext())
                {
                    var zakaz = db.Zakaz.Find(nomer);

                    if (zakaz == null)
                        throw new Exception("Заказ уже удалён.");

                    db.ZakazTovar.RemoveRange(
                        db.ZakazTovar.Where(svyaz => svyaz.Nomer == nomer));

                    db.ZakazStatus.RemoveRange(
                        db.ZakazStatus.Where(svyaz => svyaz.Nomer == nomer));

                    db.ZakazPynkti.RemoveRange(
                        db.ZakazPynkti.Where(svyaz => svyaz.Nomer == nomer));

                    db.PolzovatelZakaz.RemoveRange(
                        db.PolzovatelZakaz.Where(svyaz => svyaz.Nomer == nomer));

                    db.Zakaz.Remove(zakaz);
                    db.SaveChanges();
                }

                ZagruzitZakazi();
            }
            catch (Exception oshibka)
            {
                MessageBox.Show(
                    oshibka.GetBaseException().Message,
                    "Ошибка удаления заказа",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}