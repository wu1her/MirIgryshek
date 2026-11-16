using System;
using System.Collections.Generic;
using System.Drawing;
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
using Microsoft.Win32;
using MirIgryshek.Models;

namespace MirIgryshek.Windows
{
    /// <summary>
    /// Логика взаимодействия для TovarRedactir.xaml
    /// </summary>
    public partial class TovarRedactir : Window
    {
        private string photoputh;
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
                    try
                    {
                        Photo.Source = new BitmapImage(new Uri("/Resources/Images/" + tovar.Photo, UriKind.Relative));
                    }
                    catch (System.IO.IOException)
                    {
                        Photo.Source = new BitmapImage(new Uri("/Resources/picture.png", UriKind.Relative));
                    }

                    Kategoria.SelectedValue = tovar.TovarKategoria.First().IdKategoria;
                    Postavshik.SelectedValue = tovar.TovarPostavshik.First().IdPostavshik;
                    Proizvoditel.SelectedValue = tovar.TovarProizvoditel.First().IdProizvoditel;
                }
            }
            
        }

        private void PhotoAdd_Click(object sender, RoutedEventArgs e)
        {
            var dialogphoto = new OpenFileDialog();

            dialogphoto.Title = "Выбор фото";

            if(dialogphoto.ShowDialog() != true)
            {
                return;
            }

            try
            {
                var photo = new BitmapImage(new Uri(dialogphoto.FileName));
                if (photo.Width > 300 ||  photo.Height > 200)
                {
                    MessageBox.Show("Фото слишком большое");
                    return;
                }

                Photo.Source = photo;
                photoputh = dialogphoto.FileName;
            }
            catch (Exception)
            {
                MessageBox.Show("Фото не загрузилось");
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(Ima.Text))
                errors.AppendLine("Некорректное имя");
            if (string.IsNullOrWhiteSpace(Opisanie.Text))
                errors.AppendLine("Некорректное описание");
            if (string.IsNullOrWhiteSpace(Izmerenie.Text))
                errors.AppendLine("Некорректное измерение");
            if(Kategoria.SelectedItem == null)
                errors.AppendLine("Некорректноя категория");
            if (Postavshik.SelectedItem == null)
                errors.AppendLine("Некорректный поставщик");
            if (Proizvoditel.SelectedItem == null)
                errors.AppendLine("Некорректный производитель");
            decimal cena;
            if (!decimal.TryParse(Cena.Text, out cena) || cena < 0)
                errors.AppendLine("Некорректноя цена");
            int kolichestvo;
            if (!int.TryParse(Kolichestvo.Text, out kolichestvo) || kolichestvo < 0)
                errors.AppendLine("Некорректноя количество");
            int skidka;
            if (!int.TryParse(Skidka.Text, out skidka) || skidka < 0 || skidka > 100)
                errors.AppendLine("Некорректноя скидка");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
        }
    }
}
