using System;
using System.Windows;


namespace ScribeSharp
{
    /// <summary>
    /// Interaction logic for CalculatorPage.xaml
    /// </summary>
    public partial class Calculator : Window
    {
        private string input;
        private string leftOp;
        private string rightOp;
        private double total;
        private char op;
        public Calculator()
        {
            InitializeComponent();
        }
        private void Button_0_Click(object sender, RoutedEventArgs e)
        {
            ResultTextBox.Text = "";
            input += "0";
            ResultTextBox.Text += input;
        }
        private void Button_1_Click(object sender, RoutedEventArgs e)
        {
            ResultTextBox.Text = "";
            input += "1";
            ResultTextBox.Text += input;
        }
        private void Button_2_Click(object sender, RoutedEventArgs e)
        {
            ResultTextBox.Text = "";
            input += "2";
            ResultTextBox.Text += input;
        }
        private void Button_3_Click(object sender, RoutedEventArgs e)
        {
            ResultTextBox.Text = "";
            input += "3";
            ResultTextBox.Text += input;
        }
        private void Button_4_Click(object sender, RoutedEventArgs e)
        {
            ResultTextBox.Text = "";
            input += "4";
            ResultTextBox.Text += input;
        }
        private void Button_5_Click(object sender, RoutedEventArgs e)
        {
            ResultTextBox.Text = "";
            input += "5";
            ResultTextBox.Text += input;
        }
        private void Button_6_Click(object sender, RoutedEventArgs e)
        {
            ResultTextBox.Text = "";
            input += "6";
            ResultTextBox.Text += input;
        }
        private void Button_7_Click(object sender, RoutedEventArgs e)
        {
            ResultTextBox.Text = "";
            input += "7";
            ResultTextBox.Text += input;
        }
        private void Button_8_Click(object sender, RoutedEventArgs e)
        {
            ResultTextBox.Text = "";
            input += "8";
            ResultTextBox.Text += input;
        }
        private void Button_9_Click(object sender, RoutedEventArgs e)
        {
            ResultTextBox.Text = "";
            input += "0";
            ResultTextBox.Text += input;
        }
        private void Dot_Click(object sender, RoutedEventArgs e)
        {
            ResultTextBox.Text = "";
            input += ".";
            ResultTextBox.Text += input;

        }
        private void Answer_click(object sender, RoutedEventArgs e)
        {
            try
            {
                Result();
            }
            catch (Exception)
            {
                ResultTextBox.Text = "Error in calculation!";
            }
        }

        private void Reset()
        {
            ResultTextBox.Text = "";
            input = "";
            leftOp = "";
            rightOp = "";
        }

        private void Result()
        {
            rightOp = input;
            double templeft = Double.Parse(leftOp);
            double tempright = Double.Parse(rightOp);
            Reset();
            switch (op)
            {
                case '+':
                    total = templeft + tempright;
                    break;
                case '-':
                    total = templeft - tempright;
                    break;
                case '*':
                    total = templeft * tempright;
                    break;
                case '/':
                    if (tempright != 0.0)
                    {
                        total = templeft / tempright;
                    }
                    else
                    {
                        ResultTextBox.Text = "Error: Division by zero!";
                    }
                    break;
                default:
                    // Error
                    break;
            }
            ResultTextBox.Text = total.ToString();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ResultTextBox.Text.Equals("Error in calculation!"))
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
            Reset();
        }

        private void Button_Divide_Click(object sender, RoutedEventArgs e)
        {
            leftOp = input;
            op = '/';
            input = "";
        }
        private void Button_Plus_Click(object sender, RoutedEventArgs e)
        {
            leftOp = input;
            op = '+';
            input = "";
        }
        private void Button_Minus_Click(object sender, RoutedEventArgs e)
        {
            leftOp = input;
            op = '-';
            input = "";
        }
        private void Button_Multiply_Click(object sender, RoutedEventArgs e)
        {
            leftOp = input;
            op = '*';
            input = "";
        }
    }
}