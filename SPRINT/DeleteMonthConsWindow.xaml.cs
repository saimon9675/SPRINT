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
    /// Логика взаимодействия для DeleteMonthConsWindow.xaml
    /// </summary>
    public partial class DeleteMonthConsWindow : Window
    {
        string connString;
        public DeleteMonthConsWindow(string connectionString)
        {
            InitializeComponent();
            connString = connectionString;
            ModelSprintDBContainer container = new ModelSprintDBContainer(connString);
            List<MonthConsumption> lst = container.Set<MonthConsumption>().ToList();
            lst.Sort();
            foreach (MonthConsumption item in lst)
            {
                PeriodsComboBox.Items.Add((item.Date/100).ToString() + "/" + (item.Date%100 < 10 ? "0" + (item.Date%100).ToString() : (item.Date % 100).ToString()));
            }
        }

        private void DeletePeriodButton_Click(object sender, RoutedEventArgs e)
        {
            if (PeriodsComboBox.SelectedIndex > -1)
            {
                ModelSprintDBContainer container = new ModelSprintDBContainer(connString);
                List<MonthConsumption> lst = container.Set<MonthConsumption>().ToList();
                string deleteStr = PeriodsComboBox.SelectedItem.ToString();
                foreach (MonthConsumption item in lst)
                {
                    string str = (item.Date / 100).ToString() + "/" + (item.Date % 100 < 10 ? "0" + (item.Date % 100).ToString() : (item.Date % 100).ToString());
                    if (string.Equals(str, deleteStr))
                    {
                        var billingLst = container.Set<Billing>().ToList();
                        foreach (var bill in billingLst)
                        {
                            if (bill.MonthConsumption.MonthBillingID == item.MonthBillingID)
                                container.BillingSet.Remove(bill);
                        }
                        container.MonthConsumptionSet.Remove(item);
                        break;
                    }
                }
                container.SaveChanges();
                Close();
            }
            else
            {
                MessageBox.Show("Выберите нужный период из списка", "Ошибка");
            }
        }
    }
}
