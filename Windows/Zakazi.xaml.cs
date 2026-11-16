using Microsoft.EntityFrameworkCore;
using MirIgryshek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MirIgryshek.Windows
{
    public partial class Zakazi : Window
    {
        public Zakazi(string fio, bool pravo)
        {
            InitializeComponent();
            UserBlock.Text = fio;
            if (pravo == true)
                ZakazAdd.Visibility = Visibility.Visible;


            using (var db = new AppDbContext())
            {
                var zakazi = db.Zakaz
                    .Include(zakaz => zakaz.ZakazStatus)
                    .ThenInclude(svyaz => svyaz.IdStatusNavigation)
                    .Include(zakaz => zakaz.ZakazTovar)
                    .Include(zakaz => zakaz.PolzovatelZakaz)
                    .ToList();
                var pynkti = db.ZakazPynkti
                    .Include(svyaz => svyaz.IdPynktNavigation)
                    .ToList();

                SpisokZakazi.ItemsSource = zakazi.Select(zakaz => new
                {
                    Nomer = zakaz.Nomer,
                    Login = string.Join(", ", zakaz.PolzovatelZakaz.Select(svyaz => svyaz.Login)),
                    Tovar = string.Join(", ", zakaz.ZakazTovar.Select(svyaz => svyaz.Articul)),
                    Pynkt = pynkti
                    .Where(svyaz => svyaz.Nomer == zakaz.Nomer)
                    .Select(svyaz => svyaz.IdPynktNavigation)
                    .FirstOrDefault(),
                    Status = zakaz.ZakazStatus
                    .Select(svyaz => svyaz.IdStatusNavigation.Ima)
                    .FirstOrDefault(),
                    Kod = zakaz.Kod,
                    DataZakaza = zakaz.DataZakaza,
                    DataDoskavka = zakaz.DataDostavka
                });
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ZakazAdd_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
