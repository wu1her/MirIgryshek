using System;
using System.Linq;
using System.Windows;
using MirIgryshek.Models;

namespace MirIgryshek.Windows
{
    public partial class OknoZakaza : Window
    {
        public OknoZakaza(Zakaz zakaz)
        {
            InitializeComponent();

            Title = "Заказ № " + zakaz.Nomer;

            Articul.Text = "Номер заказа: " + zakaz.Nomer;

            DataZakaza.Text = "Дата заказа: "
                + zakaz.DataZakaza.ToString("dd.MM.yyyy");

            DataDostavki.Text = "Дата доставки: "
                + zakaz.DataDostavka.ToString("dd.MM.yyyy");

            try
            {
                using (var db = new AppDbContext())
                {
                    var status = db.ZakazStatus
                        .Where(svyaz => svyaz.Nomer == zakaz.Nomer)
                        .Select(svyaz => svyaz.IdStatusNavigation.Ima)
                        .FirstOrDefault();

                    var adres = db.ZakazPynkti
                        .Where(svyaz => svyaz.Nomer == zakaz.Nomer)
                        .Select(svyaz => svyaz.IdPynktNavigation.Adres)
                        .FirstOrDefault();

                    Status.Text = "Статус: " + (status ?? "Не указан");
                    Pynkt.Text = "Пункт выдачи: " + (adres ?? "Не указан");
                }
            }
            catch (Exception oshibka)
            {
                MessageBox.Show(
                    oshibka.GetBaseException().Message,
                    "Ошибка загрузки заказа",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}