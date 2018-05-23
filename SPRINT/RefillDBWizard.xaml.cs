using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SPRINT
{
    /// <summary>
    /// Логика взаимодействия для RefillDBWizard.xaml
    /// </summary>
    public partial class RefillDBWizard : Window
    {
        string connString;

        public RefillDBWizard(string connectionString)
        {
            connString = connectionString;
            InitializeComponent();
        }

        float costOfWater = 0;

        List<Cottager> cottagers = new List<Cottager>();


        private void CostOfWaterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (float.TryParse(CostOfWaterTextBox.Text, out costOfWater) == true && costOfWater > 0)
            {
                if ((float)Math.Round(costOfWater, 2) == costOfWater)
                {
                    Page1.CanSelectNextPage = true;
                    if (CostOfWaterTextBox.Text.Contains(','))
                    {
                        if (CostOfWaterTextBox.Text.Length < 4)
                        {
                            Page1.CanSelectNextPage = false;
                        }
                        else if (CostOfWaterTextBox.Text[CostOfWaterTextBox.Text.Length - 3] != ',')
                        {
                            Page1.CanSelectNextPage = false;
                        }
                    }
                }
                else
                {
                    Page1.CanSelectNextPage = false;
                }
            }
            else
            {
                Page1.CanSelectNextPage = false;
            }
        }

        private void AddCottagerButton_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(SquareTextBox.Text, out decimal t) == true && t > 0)
            {
                Cottager cottager = new Cottager
                {
                    Author = AuthorOfCottageTextBox.Text,
                    Square = t
                };

                if (cottagers.Contains(cottager) == true)
                {
                    MessageBox.Show("Не должно быть одинаковых записей", "Ошибка");
                }
                else if (cottager.Author == "")
                {
                    MessageBox.Show("У дачника должно быть ФИО", "Ошибка");
                }
                else
                {
                    cottagers.Add(cottager);
                    AuthorOfCottageTextBox.Text = "";
                    SquareTextBox.Text = "";
                    BindListToDataGrid();
                }
            }
            else
            {
                MessageBox.Show("Площадь должна быть неотрицательным числом", "Ошибка");
            }
        }

        void BindListToDataGrid()
        {
            AuthorsDataGrid.ItemsSource = null;
            AuthorsDataGrid.ItemsSource = cottagers;
            AuthorsDataGrid.Columns.RemoveAt(0);
            AuthorsDataGrid.Columns.RemoveAt(2);
            AuthorsDataGrid.Columns[0].Header = "ФИО дачника";
            AuthorsDataGrid.Columns[1].Header = "Площадь";
            AuthorsDataGrid.Columns[0].Width = this.ActualWidth / 3 * 2;
            AuthorsDataGrid.Columns[1].Width = this.ActualWidth / 3;
        }

        private void DeleteCottagerFromList_Click(object sender, RoutedEventArgs e)
        {
            DataGridRow row = (DataGridRow)AuthorsDataGrid.ItemContainerGenerator.ContainerFromIndex(AuthorsDataGrid.SelectedIndex);

            cottagers.Remove((Cottager)row.BindingGroup.Items[0]);
            BindListToDataGrid();
        }

        private void DeleteAllFromTablesCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Page3.CanSelectNextPage = true;
        }

        private void DeleteAllFromTablesCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Page3.CanSelectNextPage = false;
        }


        private void Wizard_Next(object sender, Xceed.Wpf.Toolkit.Core.CancelRoutedEventArgs e)
        {
            if (WizardWindow.CurrentPage == Page3)
            { 
                int cost = Convert.ToInt32(costOfWater * 100);
                costTextBlock.Text += (cost / 100).ToString() + " руб. ";
                if (cost % 100 > 0)
                {
                    costTextBlock.Text += (cost % 100).ToString() + " коп.";
                }                                

                countCottagersTextBlock.Text += cottagers.Count.ToString();

                ModelSprintDBContainer container = new ModelSprintDBContainer(connString);

                var CollCost = from item in container.CostOfWaterSet
                               select item;
                container.CostOfWaterSet.RemoveRange(CollCost);                
                container.CostOfWaterSet.Add(new CostOfWater()
                {
                    Cost = Convert.ToDecimal(costOfWater)
                });

                var CollCottagers = from item in container.CottagerSet
                                    select item;
                container.CottagerSet.RemoveRange(CollCottagers);
                foreach (var item in cottagers)
                {
                    container.CottagerSet.Add(new Cottager()
                    {
                        Author = item.Author,
                        Square = item.Square
                    });
                }

                var CollBilling = from item in container.BillingSet
                                  select item;
                container.BillingSet.RemoveRange(CollBilling);

                var CollMonthConsumption = from item in container.MonthConsumptionSet
                                           select item;
                container.MonthConsumptionSet.RemoveRange(CollMonthConsumption);

                container.SaveChanges();
            }          

        }
    }
}





