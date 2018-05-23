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
    /// Логика взаимодействия для AddCostOfWaterWindow.xaml
    /// </summary>
    public partial class AddCostOfWaterWindow : Window
    {
        string connString;
        public AddCostOfWaterWindow(string connectionString)
        {
            connString = connectionString;
            InitializeComponent();
        }

        private void CreateCostBtn_Click(object sender, RoutedEventArgs e)
        {
            ModelSprintDBContainer container = new ModelSprintDBContainer(connString);
            container.CostOfWaterSet.Add(new CostOfWater()
            {
                Cost = Decimal.Parse(CostOfWaterTB.Text)
            });
            container.SaveChanges();
            Close();
        }

        private void CostOfWaterTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (float.TryParse(CostOfWaterTB.Text, out float costOfWater) == true && costOfWater > 0)
            {
                if ((float)Math.Round(costOfWater, 2) == costOfWater)
                {

                    CreateCostBtn.IsEnabled = true;
                    if (CostOfWaterTB.Text.Contains(','))
                    {
                        if (CostOfWaterTB.Text.Length < 4)
                        {
                            CreateCostBtn.IsEnabled = false;
                        }
                        else if (CostOfWaterTB.Text[CostOfWaterTB.Text.Length - 3] != ',')
                        {
                            CreateCostBtn.IsEnabled = false;
                        }
                    }
                }
                else
                {
                    CreateCostBtn.IsEnabled = false;
                }
            }
            else
            {
                CreateCostBtn.IsEnabled = false;
            }
        }
    }
}
