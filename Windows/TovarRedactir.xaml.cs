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
            {
                Title = "Добавление товара";
            }
            else
            {
                Title = "Редактирование товара";
            }
        }
    }
}
