using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;

namespace SPRINT
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string connString = "Data Source=DESKTOP;Initial Catalog=sprintDB;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
        public MainWindow()
        {
            InitializeComponent();            
        }

        private void CloseWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RefillDataBase_Click(object sender, RoutedEventArgs e)
        {
            var window = new RefillDBWizard(connString);
            window.Owner = this;
            window.ShowDialog();
            SetCottagersAsItemsSource();
        }

        private void ViewDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ViewDataGrid.SelectedIndex > -1)
            {
                DataGridRow row = (DataGridRow)ViewDataGrid.ItemContainerGenerator.ContainerFromIndex(ViewDataGrid.SelectedIndex);
                if (ViewDataGrid.Columns[0].Header.ToString() == "ФИО дачника" && ViewDataGrid.Columns[1].Header.ToString() == "Площадь участка")
                {                    
                    SetCottagerBillsAsItemSource((Cottager)row.BindingGroup.Items[0], null);
                }
                else if (ViewDataGrid.Columns[0].Header.ToString() == "Дата" && ViewDataGrid.Columns[1].Header.ToString() == "Потребление за месяц")
                {
                    SetMonthBillingAsItemSource((MonthConsumption)row.BindingGroup.Items[0], null);
                }
                else if (ViewDataGrid.Columns[0].Header.ToString() == "ФИО дачника" && ViewDataGrid.Columns[1].Header.ToString() == "Счёт за месяц")
                {                   
                    SetCottagerBillsAsItemSource(null, (Billing)row.BindingGroup.Items[0]);
                }
                else if (ViewDataGrid.Columns[0].Header.ToString() == "Дата" && ViewDataGrid.Columns[1].Header.ToString() == "Счёт")
                {
                    SetMonthBillingAsItemSource(null, (Billing)row.BindingGroup.Items[0]);
                }
            }
        }

        private void ShowCottagersBtn_Click(object sender, RoutedEventArgs e)
        {
            SetCottagersAsItemsSource();
        }

        private void ShowMonthConsumptionBtn_Click(object sender, RoutedEventArgs e)
        {
            SetMonthConsAsItemSource();
        }

        private void ShowCostOfWaterBtn_Click(object sender, RoutedEventArgs e)
        {
            ModelSprintDBContainer container = new ModelSprintDBContainer(connString);
            List<CostOfWater> lst = container.Set<CostOfWater>().ToList();
            string str = "";
            int cost = Convert.ToInt32(lst[0].Cost * 100);

            str = (cost / 100).ToString() + " руб. ";
            if (cost % 100 > 0)
            {
                str += (cost % 100).ToString() + " коп.";
            }

            MessageBox.Show("Кубический метр воды стоит " + str, "Цена за воду");
        }

        private void AddCottagerBtn_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddCottagerWindow(connString);
            window.Owner = this;
            window.ShowDialog();
            SetCottagersAsItemsSource();
        }

        private void DeleteCottagerBtn_Click(object sender, RoutedEventArgs e)
        {
            var window = new DeleteCottagerWindow(connString);
            window.Owner = this;
            window.ShowDialog();
            SetCottagersAsItemsSource();
            isRecordsExist();
           
        }

        private void AddMonthConsumptionBtn_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddMonthConsumptionWindow(connString);
            window.Owner = this;
            window.ShowDialog();
            SetCottagersAsItemsSource();
        }

        private void ViewDataGrid_Loaded(object sender, RoutedEventArgs e)
        {           
            try
            {
                BinaryReader r = new BinaryReader(new FileStream("path.bin", FileMode.OpenOrCreate));
                connString = r.ReadString();
                r.Close();
                ModelSprintDBContainer container = new ModelSprintDBContainer(connString);
                container.Database.Connection.Open();                
                container.Database.Connection.Close();                
            }
            catch (SqlException)
            {                
                var window = new ConnectToBDWindow(connString);
                window.Owner = this;
                window.ShowDialog();
                if (window.DialogResult == false)
                {
                    Close();
                }
                BinaryWriter w = new BinaryWriter(new FileStream("path.bin", FileMode.OpenOrCreate));
                w.Write(connString);
                w.Close();
            }
            SetCottagersAsItemsSource();
            
        }        

        private void SetCottagersAsItemsSource()
        {
            ModelSprintDBContainer container = new ModelSprintDBContainer(connString);
            List<Cottager> lst = container.Set<Cottager>().ToList();
            if (lst.Count > 0)
            {
                ViewDataGrid.ItemsSource = lst;
                ViewDataGrid.Columns.RemoveAt(0);
                ViewDataGrid.Columns.RemoveAt(2);
                ViewDataGrid.Columns[0].Header = "ФИО дачника";
                ViewDataGrid.Columns[1].Header = "Площадь участка";
                
                SortByFirstColumn();
                currentViewTextBlock.Text = "Дачники";
            }
            
        }        

        private void SetCottagerBillsAsItemSource(Cottager cottager, Billing billEx)
        {
            ModelSprintDBContainer container = new ModelSprintDBContainer(connString);
            IEnumerable<Billing> lst = null;
            if (billEx == null)
            {
                lst = from bill in cottager.Billing
                      select bill;
            }
            else
            {
                lst = from bill in billEx.Cottager.Billing
                      select bill;
            }                        
            List<Billing> billing = lst.ToList();

            if (billing.Count > 0)
            {
                ViewDataGrid.ItemsSource = lst;
                ViewDataGrid.Columns.RemoveAt(0);
                ViewDataGrid.Columns.RemoveAt(0);
                ViewDataGrid.Columns.RemoveAt(0);
                ViewDataGrid.Columns.RemoveAt(0);
                ViewDataGrid.Columns[0].Header = "Дата";
                ViewDataGrid.Columns[1].Header = "Счёт";
                currentViewTextBlock.Text = (billEx == null ? cottager.Author : billEx.Cottager.Author) +": счета по месяцам";
                SortByFirstColumn();
            }
            else
            {
                MessageBox.Show("Для выбранного дачника нет рассчитанных периодов");
            }
        }        

        private void SetMonthBillingAsItemSource(MonthConsumption consumption, Billing billEx)
        {
            ModelSprintDBContainer container = new ModelSprintDBContainer(connString);
            IQueryable<Billing> lst = null;
            if (billEx == null)
            {               
                lst = from bill in container.BillingSet
                          where bill.MonthConsumption.MonthBillingID == consumption.MonthBillingID
                          select bill;
            }
            else
            {
                lst = from bill in container.BillingSet
                      where bill.Date == billEx.Date
                      select bill;
            }
            List<Billing> bills = lst.ToList();

            if (bills.Count > 0)
            {
                ViewDataGrid.ItemsSource = bills;
                ViewDataGrid.Columns.RemoveAt(0);
                ViewDataGrid.Columns.RemoveAt(1);
                ViewDataGrid.Columns.RemoveAt(2);
                ViewDataGrid.Columns.RemoveAt(2);
                ViewDataGrid.Columns[0].Header = "ФИО дачника";
                ViewDataGrid.Columns[1].Header = "Счёт за месяц";                
                int date = (billEx == null ? consumption.Date : billEx.MonthConsumption.Date);
                currentViewTextBlock.Text = "Отчёт за период: " + (date / 100).ToString() + "/" + (date % 100 < 10 ? "0" + (date % 100).ToString() : (date % 100).ToString());
                SortByFirstColumn();
            }
            else if (consumption != null)
            {
                MessageBox.Show("Отсутствуют дачники для выбранного периода");
            }
        }       

        private void SetMonthConsAsItemSource()
        {
            ModelSprintDBContainer container = new ModelSprintDBContainer(connString);
            List<MonthConsumption> lst = container.Set<MonthConsumption>().ToList();
            if (lst.Count > 0)
            {
                ViewDataGrid.ItemsSource = lst;
                ViewDataGrid.Columns.RemoveAt(0);
                ViewDataGrid.Columns.RemoveAt(2);
                ViewDataGrid.Columns[0].Header = "Дата";
                ViewDataGrid.Columns[1].Header = "Потребление за месяц";
                SortByFirstColumn();
                currentViewTextBlock.Text = "Потребление воды по месяцам";
            }
        }

        private void SortByFirstColumn()
        {
            if (ViewDataGrid.Columns.Count > 0)
            {
                SetColumnsWidth();
                ListSortDirection sortDirection = ListSortDirection.Ascending;
                var column = ViewDataGrid.Columns[0];
                ViewDataGrid.Items.SortDescriptions.Clear();
                ViewDataGrid.Items.SortDescriptions.Add(new SortDescription(column.SortMemberPath, sortDirection));
                foreach (var col in ViewDataGrid.Columns)
                {
                    col.SortDirection = null;
                }
                column.SortDirection = sortDirection;
                ViewDataGrid.Items.Refresh();
            }            
        }

        private void FillDBForExampleBtn_Click(object sender, RoutedEventArgs e)
        {
            ModelSprintDBContainer container = new ModelSprintDBContainer(connString);

            ClearDB();
            //пример
            CostOfWater costOfWater = new CostOfWater()
            {
                Cost = 1.57M
            };
            container.CostOfWaterSet.Add(costOfWater);

            container.CottagerSet.Add(new Cottager()
            {
                Author = "Шубин Семён Николаевич",
                Square = 12
            });
            container.CottagerSet.Add(new Cottager()
            {
                Author = "Иванов Пётр Михайлович",
                Square = 15
            });
            container.CottagerSet.Add(new Cottager()
            {
                Author = "Сидоров Александр Анатольевич",
                Square = 20
            });
            container.CottagerSet.Add(new Cottager()
            {
                Author = "Машинцев Илья Константинович",
                Square = 9.5M
            });

            var cottagers = from item in container.CottagerSet.Local
                            select item;

            MonthConsumption monthConsumption = new MonthConsumption()
            {
                Consumption = 300,
                Date = 201804
            };
            decimal square = 0;
            foreach (Cottager cottager in cottagers)
            {
                square += cottager.Square;
            }
            foreach (Cottager cottager in cottagers)
            {
                Billing bill = new Billing()
                {
                    MonthConsumption = monthConsumption,
                    Cottager = cottager,
                    Author = cottager.Author,
                    Date = monthConsumption.Date,
                    Bill = Convert.ToDecimal((cottager.Square / square) * monthConsumption.Consumption * costOfWater.Cost),
                };
                container.BillingSet.Add(bill);
            }
                        
            monthConsumption = new MonthConsumption()
            {
                Consumption = 470.5M,
                Date = 201803
            };            
            
            foreach (Cottager cottager in cottagers)
            {
                Billing bill = new Billing()
                {
                    MonthConsumption = monthConsumption,
                    Cottager = cottager,
                    Author = cottager.Author,
                    Date = monthConsumption.Date,
                    Bill = Convert.ToDecimal((cottager.Square / square) * monthConsumption.Consumption * costOfWater.Cost),
                };
                container.BillingSet.Add(bill);
            }

            container.SaveChanges();
            SetCottagersAsItemsSource();
        }

        private void DeleteMonthConsumptionBtn_Click(object sender, RoutedEventArgs e)
        {
            var window = new DeleteMonthConsWindow(connString);
            window.Owner = this;
            window.ShowDialog();
            SetCottagersAsItemsSource();                    
        }       

        private void isRecordsExist()
        {
            ModelSprintDBContainer container = new ModelSprintDBContainer(connString);
            if (container.CottagerSet.Count() == 0 && container.MonthConsumptionSet.Count() == 0)
            {
                ViewDataGrid.ItemsSource = null;
                currentViewTextBlock.Text = "";
            }           
        }

        private void ClearDBBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearDB();
            var window = new AddCostOfWaterWindow(connString);
            window.ShowDialog();
        }

        private void ClearDB()
        {
            ModelSprintDBContainer container = new ModelSprintDBContainer(connString);

            var CollBilling = from item in container.BillingSet
                              select item;
            container.BillingSet.RemoveRange(CollBilling);

            var CollMonthConsumption = from item in container.MonthConsumptionSet
                                       select item;
            container.MonthConsumptionSet.RemoveRange(CollMonthConsumption);

            var CollCost = from item in container.CostOfWaterSet
                           select item;
            container.CostOfWaterSet.RemoveRange(CollCost);

            var CollCottagers = from item in container.CottagerSet
                                select item;
            container.CottagerSet.RemoveRange(CollCottagers);
            container.SaveChanges();

            ViewDataGrid.ItemsSource = null;
            currentViewTextBlock.Text = "";
        }                

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetColumnsWidth();
        }

        private void SetColumnsWidth()
        {
            if (ViewDataGrid.Columns.Count != 0)
            {
                if (ViewDataGrid.Columns[0].Header.ToString() == "ФИО дачника")
                {
                    ViewDataGrid.Columns[0].Width = this.ActualWidth / 3 * 2;
                    ViewDataGrid.Columns[1].Width = this.ActualWidth / 3;
                }
                else
                {
                    ViewDataGrid.Columns[0].Width = this.ActualWidth / 2;
                    ViewDataGrid.Columns[1].Width = this.ActualWidth / 2;
                }
            }
        }
    }
}
