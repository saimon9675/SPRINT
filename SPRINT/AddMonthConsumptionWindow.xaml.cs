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
    /// Логика взаимодействия для AddMonthConsumptionWindow.xaml
    /// </summary>
    public partial class AddMonthConsumptionWindow : Window
    {
        string connString;
        public AddMonthConsumptionWindow(string connectionString)
        {
            InitializeComponent();
            connString = connectionString;
        }

        private void AddMonthConsumptionBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MonthCalendar.SelectedDate.HasValue == false)
            {
                MessageBox.Show("Выберите рассчётный месяц");                
            }
            else if (float.TryParse(MonthConsumptionTB.Text, out float cons) == false)
            {
                MessageBox.Show("Введите расход воды числом");
            }
            else if (cons <= 0)
            {
                MessageBox.Show("Расход воды должен быть больше нуля");
            }
            else
            {
                string str = MonthCalendar.SelectedDate.Value.ToShortDateString();
                str = str.Substring(6, 4) + str.Substring(3, 2);

                ModelSprintDBContainer container = new ModelSprintDBContainer(connString);

                List<MonthConsumption> collMonth = container.Set<MonthConsumption>().ToList();

                MonthConsumption monthConsumption = new MonthConsumption()
                {
                    Date = Convert.ToInt32(str),
                    Consumption = Convert.ToDecimal(cons)
                };

                if (collMonth.Contains(monthConsumption) == true)
                {
                    MessageBox.Show("Такой период уже существует");

                }
                else
                {
                    container.MonthConsumptionSet.Add(monthConsumption);       
                    List<CostOfWater> costOfWater = container.Set<CostOfWater>().ToList();
                    decimal square = 0;                    
                    foreach (Cottager cottager in container.CottagerSet)
                    {
                        square += cottager.Square;
                    }

                    foreach (Cottager cottager in container.CottagerSet)
                    {
                        Billing bill = new Billing()
                        {
                            MonthConsumption = monthConsumption,
                            Cottager = cottager,
                            Author = cottager.Author,
                            Date = monthConsumption.Date,
                            Bill = Convert.ToDecimal((cottager.Square / square) * monthConsumption.Consumption * costOfWater[0].Cost),
                        };
                        container.BillingSet.Add(bill);

                       
                    }
                    container.SaveChanges();
                    Close();
                }
            }
            
        }

    }
}
