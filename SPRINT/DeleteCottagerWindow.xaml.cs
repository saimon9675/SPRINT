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
    /// Логика взаимодействия для DeleteCottagerWindow.xaml
    /// </summary>
    public partial class DeleteCottagerWindow : Window
    {
        string connString;
        public DeleteCottagerWindow(string connectionString)
        {
            InitializeComponent();
            connString = connectionString;
            ModelSprintDBContainer container = new ModelSprintDBContainer(connString);
            List<Cottager> lst = container.Set<Cottager>().ToList();
            lst.Sort();
            foreach (Cottager item in lst)
            {
                CottagersComboBox.Items.Add(item.Author + "; S = " + item.Square.ToString());
            }    
        }

        private void DeleteCottagerButton_Click(object sender, RoutedEventArgs e)
        {
            if (CottagersComboBox.SelectedIndex != -1)
            {
                ModelSprintDBContainer container = new ModelSprintDBContainer(connString);
                List<Cottager> lst = container.Set<Cottager>().ToList();
                string deleteStr = CottagersComboBox.SelectedItem.ToString();
                foreach (Cottager item in lst)
                {
                    string str = item.Author + "; S = " + item.Square.ToString();
                    if (string.Equals(str, deleteStr))
                    {                        
                        var billingLst = container.Set<Billing>().ToList();
                        foreach (var bill in billingLst)
                        {
                            if (bill.Cottager.CottagerID == item.CottagerID)
                                container.BillingSet.Remove(bill);
                        }
                        container.CottagerSet.Remove(item);                        
                        break;
                    }
                }
                container.SaveChanges();
                Close();
            }
            else
            {
                MessageBox.Show("Выберите нужный элемент из списка", "Ошибка");
            }
        }
    }
}
