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
using MirIgryshek.Models;

namespace MirIgryshek.Windows
{
    /// <summary>
    /// Логика взаимодействия для TovarRedactir.xaml
    /// </summary>
    public partial class TovarRedactir : Window
    {
        private Tovar tovar;
        public TovarRedactir(Tovar peredantovar)
        {
            tovar = peredantovar;
            InitializeComponent();

            if (tovar == null)
                Title = "Добавление товара";
            else
                Title = "Редактирование товара";

            using (var db = new AppDbContext()) 
            {
                Kategoria.ItemsSource = db.Kategoria.ToList();
                Postavshik.ItemsSource = db.Postavshik.ToList();
                Proizvoditel.ItemsSource = db.Proizvoditel.ToList();


                if (tovar != null)
                {
                    Ima.Text = tovar.Ima;
                    Opisanie.Text = tovar.Opisanie;
                    Cena.Text = tovar.Cena.ToString();
                    Izmerenie.Text = tovar.Izmerenie;
                    Kolichestvo.Text = tovar.Kolichestvo.ToString();
                    Skidka.Text = tovar.Skidka.ToString();

                    Kategoria.SelectedValue = tovar.TovarKategoria.First().IdKategoria;
                    Postavshik.SelectedValue = tovar.TovarPostavshik.First().IdPostavshik;
                    Proizvoditel.SelectedValue = tovar.TovarProizvoditel.First().IdProizvoditel;
                }
            }
            
        }

        private void PhotoAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
