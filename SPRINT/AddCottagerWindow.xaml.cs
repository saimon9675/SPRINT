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

namespace SPRINT
{
    /// <summary>
    /// Логика взаимодействия для AddCottagerWindow.xaml
    /// </summary>
    public partial class AddCottagerWindow : Window
    {
        string connString;
        public AddCottagerWindow(string connectionString)
        {
            connString = connectionString;
            InitializeComponent();
        }

        private void AddCottagerButton_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(SquareTextBox.Text, out decimal t) == true && t > 0)
            {
                ModelSprintDBContainer container = new ModelSprintDBContainer(connString);               
                List<Cottager> lst = container.Set<Cottager>().ToList();//cottagers.Add(cottager);

                Cottager cottager = new Cottager
                {
                    Author = AuthorOfCottageTextBox.Text,
                    Square = t
                };

                if (lst.Contains(cottager) == true)
                {
                    MessageBox.Show("Не должно быть одинаковых записей", "Ошибка");
                }
                else if (cottager.Author == "")
                {
                    MessageBox.Show("У дачника должно быть ФИО", "Ошибка");
                }
                else
                {
                    container.CottagerSet.Add(cottager);
                    container.SaveChanges();
                    Close();
                }

            }
            else
            {
                MessageBox.Show("Площадь должна быть неотрицательным числом", "Ошибка");
            }
        }
    }
    
}
