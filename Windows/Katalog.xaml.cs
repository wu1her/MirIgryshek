using Microsoft.EntityFrameworkCore;
using MirIgryshek.Models;
using System.Linq;
using System.Windows;

namespace MirIgryshek.Windows
{
    public partial class Katalog : Window
    {
        public Katalog(Polzovatel user)
        {
            InitializeComponent();
            if (user != null )
                UserBlock.Text = user.Fio;
            else
                UserBlock.Text = "Гость";

            using (var db = new AppDbContext())
            {
                var product = db.Tovar
                    .Include(tovar => tovar.TovarPostavshik)
                    .ThenInclude(svyaz => svyaz.IdPostavshikNavigation)
                    .Include(tovar => tovar.TovarProizvoditel)
                    .ThenInclude(svyaz => svyaz.IdProizvoditelNavigation)
                    .Include(tovar => tovar.TovarKategoria)
                    .ThenInclude(svyaz => svyaz.IdKategoriaNavigation);

                foreach (var tovar in product)
                {
                    Cards.Children.Add(new TovarCard(tovar));
                }

                if (user != null && db.PolzovatelRole.Any(
                    role => role.Login == user.Login && role.IdRole == 1 || role.IdRole == 2))
                {
                    Filtr.Visibility = Visibility.Visible;
                }

                var postavshik = db.Postavshik.ToList();
                postavshik.Insert(0, new Postavshik
                {
                    IdPostavshik = 0,
                    Ima = "Все поставщики"
                });

                Filtr_postavshik.ItemsSource = postavshik;
                Filtr_postavshik.SelectedIndex = 0;

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
    }
}
