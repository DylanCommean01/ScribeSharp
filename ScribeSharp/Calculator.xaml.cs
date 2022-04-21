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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScribeSharp
{
    /// <summary>
    /// Interaction logic for CalculatorPage.xaml
    /// </summary>
    public partial class Calculator : Window
    {
        private string leftOp;
        private string rightOp;
        private double total;
        private bool left = true;
        private string op;
        public Calculator()
        {
            InitializeComponent();
        }
        private void Button_0_Click(object sender, RoutedEventArgs e)
        {
            if (left)
            {
                leftOp += 0;
                ResultTextBox.Text = leftOp;
            }
            else
            {
                rightOp += 0;
                ResultTextBox.Text = rightOp;
            }
        }
        private void Button_1_Click(object sender, RoutedEventArgs e)
        {
            if (left)
            {
                leftOp += 1;
                ResultTextBox.Text = leftOp;
            }
            else
            {
                rightOp += 1;
                ResultTextBox.Text = rightOp;
            }
        }
        private void Button_2_Click(object sender, RoutedEventArgs e)
        {
            if (left)
            {
                leftOp += 2;
                ResultTextBox.Text = leftOp;
            }
            else
            {
                rightOp += 2;
                ResultTextBox.Text = rightOp;
            }
        }
        private void Button_3_Click(object sender, RoutedEventArgs e)
        {
            if (left)
            {
                leftOp += 3;
                ResultTextBox.Text = leftOp;
            }
            else
            {
                rightOp += 3;
                ResultTextBox.Text = rightOp;
            }
        }
        private void Button_4_Click(object sender, RoutedEventArgs e)
        {
            if (left)
            {
                leftOp += 4;
                ResultTextBox.Text = leftOp;
            }
            else
            {
                rightOp += 4;
                ResultTextBox.Text = rightOp;
            }
        }
        private void Button_5_Click(object sender, RoutedEventArgs e)
        {
            if (left)
            {
                leftOp += 5;
                ResultTextBox.Text = leftOp;
            }
            else
            {
                rightOp += 5;
                ResultTextBox.Text = rightOp;
            }
        }
        private void Button_6_Click(object sender, RoutedEventArgs e)
        {
            if (left)
            {
                leftOp += 6;
                ResultTextBox.Text = leftOp;
            }
            else
            {
                rightOp += 6;
                ResultTextBox.Text = rightOp;
            }
        }
        private void Button_7_Click(object sender, RoutedEventArgs e)
        {
            if (left)
            {
                leftOp += 7;
                ResultTextBox.Text = leftOp;
            }
            else
            {
                rightOp += 7;
                ResultTextBox.Text = rightOp;
            }
        }
        private void Button_8_Click(object sender, RoutedEventArgs e)
        {
            if (left)
            {
                leftOp += 8;
                ResultTextBox.Text = leftOp;
            }
            else
            {
                rightOp += 8;
                ResultTextBox.Text = rightOp;
            }
        }
        private void Button_9_Click(object sender, RoutedEventArgs e)
        {
            if (left)
            {
                leftOp += 9;
                ResultTextBox.Text = leftOp;
            }
            else
            {
                rightOp += 9;
                ResultTextBox.Text = rightOp;
            }
        }
        private void Answer_click(object sender, RoutedEventArgs e)
        {
            try
            {
                result();
            }
            catch (Exception exception)
            {
                ResultTextBox.Text = "Error in calculation!";
            }
        }

        private void result()
        {
            double templeft = Double.Parse(leftOp);
            double tempright = Double.Parse(rightOp);
            switch (op)
            {
                case "+":
                    total = templeft + tempright;
                    break;
                case "-":
                    total = templeft - tempright;
                    break;
                case "*":
                    total = templeft * tempright;
                    break;
                case "/":
                    total = templeft / tempright;
                    break;
                default:
                    // Error
                    break;
            }

            if (left)
            {
                ResultTextBox.Text = total.ToString();
                leftOp = "";
                rightOp = "";
                left = !left;
            }
            else 
            {
                leftOp = total.ToString();
                ResultTextBox.Text = total.ToString();
            }
            
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ResultTextBox.Text == "Error in Calculation!")
            {
                ResultTextBox.Text = "";
            }
            if (ResultTextBox.Text.Length > 0)
            {
                ResultTextBox.Text = ResultTextBox.Text.Substring(0, ResultTextBox.Text.Length - 1);
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            ResultTextBox.Text = "";
            leftOp = "";
            rightOp = "";
            left = true;
        }

        private void Button_Divide_Click(object sender, RoutedEventArgs e)
        {
            op = "/";
            left = !left;
        }
        private void Button_Plus_Click(object sender, RoutedEventArgs e)
        {
            op = "+";
            left = !left;
        }
        private void Button_Minus_Click(object sender, RoutedEventArgs e)
        {
            op = "-";
            left = !left;
        }
        private void Button_Multiply_Click(object sender, RoutedEventArgs e)
        {
            op = "*";
            left = !left;
        }
    }
}