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
    public partial class Auth : Window
    {
        public Auth()
        {
            InitializeComponent();
            
        }
        private readonly AppDbContext db = new AppDbContext();

        private bool CheckTextBox()
        {
            var errors = new StringBuilder();

            if (string.IsNullOrEmpty(LoginBox.Text))
            {
                errors.AppendLine("Логин пустой");
            }

            if (string.IsNullOrEmpty(PasswordBox.Text))
            {
                errors.AppendLine("Пароль пустой");
            }

            if (errors.Length == 0)
            {
                return true;
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка");
            }
            return false;
        }
        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckTextBox()) return;

            var user = db.Polzovatel.FirstOrDefault
                (
                dannie => dannie.Login == LoginBox.Text && dannie.Parol == PasswordBox.Text
                );
            if (user == null)
                MessageBox.Show("Неверный логин или пароль", "Ошибка авторизации");
            else
            {
                var window = new MirIgryshek.Windows.Katalog(user);
                window.Show();
                Close();
            }
        }

        private void GoustButton_Click(object sender, RoutedEventArgs e)
        {
            new Katalog(null).Show();
            Close();
        }


    }
}
