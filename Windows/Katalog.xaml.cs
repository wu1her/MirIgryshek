using Microsoft.EntityFrameworkCore;
using MirIgryshek.Models;
using System.Linq;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Input;

namespace MirIgryshek.Windows
{
    public partial class Katalog : Window
    {
        private bool pravoredactirovania;
        private List<Tovar> vseTovary;
        public Katalog(Polzovatel user)
        {
            InitializeComponent();
            if (user != null )
                UserBlock.Text = user.Fio;
            else
                UserBlock.Text = "Гость";

            using (var db = new AppDbContext())
            {
                vseTovary = db.Tovar
                    .Include(tovar => tovar.TovarPostavshik)
                    .ThenInclude(svyaz => svyaz.IdPostavshikNavigation)
                    .Include(tovar => tovar.TovarProizvoditel)
                    .ThenInclude(svyaz => svyaz.IdProizvoditelNavigation)
                    .Include(tovar => tovar.TovarKategoria)
                    .ThenInclude(svyaz => svyaz.IdKategoriaNavigation)
                    .ToList();

                if (user != null && db.PolzovatelRole.Any(
                    role => role.Login == user.Login && (role.IdRole == 1 || role.IdRole == 2)))
                {
                    Filtr.Visibility = Visibility.Visible;
                    Zakazi.Visibility = Visibility.Visible;
                }

                if (user != null && db.PolzovatelRole.Any(
                    role => role.Login == user.Login && role.IdRole == 1))
                {
                    TovarAdd.Visibility = Visibility.Visible;
                    pravoredactirovania = true;
                }

                var postavshik = db.Postavshik.ToList();
                postavshik.Insert(0, new Postavshik
                {
                    IdPostavshik = 0,
                    Ima = "Все поставщики"
                });

                Filtr_postavshik.ItemsSource = postavshik;
                Filtr_postavshik.SelectedIndex = 0;

                Obnovatovar();
            }
        }

        private void Obnovatovar()
        {
            if(vseTovary == null)
                return;
            var tovary = vseTovary.AsEnumerable();
            

            var poisk = Poisk.Text.Trim().ToLower();
            tovary = tovary.Where(
                tovar => tovar.Ima.ToLower().Contains(poisk));


            var postavshik = Filtr_postavshik.SelectedItem as Postavshik;
            if (postavshik != null && postavshik.IdPostavshik != 0) 
            {
                tovary = tovary.Where(tovar => tovar.TovarPostavshik.Any(svyaz => svyaz.IdPostavshik == postavshik.IdPostavshik));
            }

            if (Sortirovka.SelectedIndex == 0)
                tovary = tovary.OrderBy(tovar => tovar.Cena);
            else if (Sortirovka.SelectedIndex == 1)
                tovary = tovary.OrderByDescending(tovar => tovar.Cena);
            else if (Sortirovka.SelectedIndex == 2)
                tovary = tovary.OrderBy(tovar => tovar.Kolichestvo);
            else if (Sortirovka.SelectedIndex == 3)
                tovary = tovary.OrderByDescending(tovar => tovar.Kolichestvo);
            

            Cards.Children.Clear();
            foreach(var tovar in tovary)
            {
                var card = new TovarCard(tovar);
                card.Tag = tovar;

                if(pravoredactirovania == true)
                {
                    card.MouseLeftButtonUp += Card_Click;
                }
                Cards.Children.Add(card);

            }

        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            var windowauth = new Auth();
            windowauth.Show();
            Close();
        }

        private void PoiskVvod(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            Obnovatovar();
        }

        private void FiltrSelect(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Obnovatovar();
        }

        private void TovarAdd_Click(object sender, RoutedEventArgs e)
        {
            var okno = new TovarRedactir(null);
            okno.ShowDialog();
        }

        private void Zakazi_Click(object sender, RoutedEventArgs e)
        {
            var okno = new MirIgryshek.Windows.Zakazi(UserBlock.Text, pravoredactirovania);
            okno.ShowDialog();
            Close();
        }

        private void Card_Click(object sender, RoutedEventArgs e)
        {
            var card = (TovarCard)sender;
            var tovar = (Tovar)card.Tag;

            var okno = new TovarRedactir(tovar);
            okno.ShowDialog();
        }
    }
}
