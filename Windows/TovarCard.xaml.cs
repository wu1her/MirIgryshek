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
    public partial class TovarCard : UserControl
    {
        public TovarCard(Tovar tovar)
        {
            InitializeComponent();

            ManeCardText.Text =  tovar.TovarKategoria.First().IdKategoriaNavigation.Ima + " | "+ tovar.Ima;
            Opisanie.Text = "Описание: " + tovar.Opisanie;
            Proizvoditel.Text = "Производитель: " + tovar.TovarProizvoditel.First().IdProizvoditelNavigation.Ima;
            Postavshik.Text = "Поставщик: " + tovar.TovarPostavshik.First().IdPostavshikNavigation.Ima;
            Cena.Text = "Цена: " + tovar.Cena + " ";
            Edinica.Text = "Единица измерения: " + tovar.Izmerenie;
            Kolichestvo.Text = "Количество на складе: " + tovar.Kolichestvo;
            Skidka.Text = "Действующая скидка: " + tovar.Skidka;

            try
            {
                Photo.Source = new BitmapImage(new Uri("/Resources/Images/" + tovar.Photo, UriKind.Relative));
            }
            catch (System.IO.IOException)
            {
                Photo.Source = new BitmapImage(new Uri("/Resources/picture.png", UriKind.Relative));
            }

            if (tovar.Skidka > 17)
                Background = (Brush)new BrushConverter().ConvertFromString("#FFDEAD");
            if (tovar.Skidka != 0)
            {
                Cena.Foreground = Brushes.Red;
                Cena.TextDecorations = TextDecorations.Strikethrough;

                var itogcena = tovar.Cena - tovar.Cena * tovar.Skidka / 100;
                CenaSkidka.Text = itogcena.ToString(); 
                CenaSkidka.Foreground = Brushes.Black;
            }

            if (tovar.Kolichestvo <= 0)
                Background = (Brush)new BrushConverter().ConvertFromString("LightBlue");
        }
    }
}
