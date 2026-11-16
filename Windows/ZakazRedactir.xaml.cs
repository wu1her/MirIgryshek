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
    /// <summary>
    /// Логика взаимодействия для ZakazRedactir.xaml
    /// </summary>
    public partial class ZakazRedactir : Window
    {
        private Zakazi zakazi;
        public ZakazRedactir()
        {
            if (zakazi == null)
                Title = "Добавление заказа";
            else
                Title = "Редактирование заказа";
            InitializeComponent();
        }
    }
}
