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

namespace Kalkulator_PS2
{
    public partial class MainWindow : Window
    {
        #region
        //Variables...
        private float result;
        private float tempNumber;
        private float number;
        //Definition of flags
        bool isFirstOperation = true;
        bool divisionByZero = false;
        bool isOperSet = false;
        #endregion

        //Definiton of all functional buttons
        enum FunctionalButtons : byte
        {
            Addition,
            Subtraction,
            Multiplication,
            Divison,
            Empty
        }
        FunctionalButtons oper;

        public MainWindow()
        {
            InitializeComponent();
        }

        //Function for all number buttons (0-9)
        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            if (mainText.Text.Equals("0") ||
                mainText.Text.Contains("NaN") ||
                isOperSet
              )
            {
                ClearScreen();
                isOperSet = false;
            }

            mainText.Text += (sender as Button).Content.ToString();
        }

        //Function for functional-arithmetical buttons (-, +, *, /)
        private void FunctionalButton_Click(object sender, RoutedEventArgs e)
        {
            if (isOperSet)
            {
                if (tempNumber == 0) float.TryParse(mainText.Text, out tempNumber);
                signLabel.Content = (sender as Button).Content.ToString();
                oper = SetOper((sender as Button).Content.ToString());
            }
            else
            {
                if (tempNumber == 0) float.TryParse(mainText.Text, out tempNumber);

                if (!isFirstOperation)
                {
                    signLabel.Content = (sender as Button).Content.ToString();
                    isOperSet = true;
                    EqualsButton_Click(sender, e);
                    oper = SetOper((sender as Button).Content.ToString());
                }
                else
                {
                    isOperSet = true;
                    signLabel.Content = (sender as Button).Content.ToString();
                    oper = SetOper((sender as Button).Content.ToString());
                    result = tempNumber;
                    isFirstOperation = false;
                }
            }
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {

            if (isFirstOperation)
            {
                //do nothing
            }
            else
            {
                if(tempNumber !=0) float.TryParse(mainText.Text, out number);

                switch (oper)
                {
                    case FunctionalButtons.Addition:

                        result = result + number;
                        break;

                    case FunctionalButtons.Divison:

                        if (number == 0)
                        {
                            divisionByZero = true;
                        }
                        else
                        {
                            result = result / number;
                        }
                        break;

                    case FunctionalButtons.Multiplication:

                        result = result * number;
                        break;

                    case FunctionalButtons.Subtraction:

                        result = result - number;
                        break;
                }

                if (divisionByZero)
                {
                    mainText.Text = "NaN";
                    divisionByZero = false;
                }
                else if (!mainText.Text.Equals(result.ToString()))
                {
                    mainText.Text = result.ToString();
                }
                tempNumber = 0;
                isOperSet = true;
            }
        }

        //Functions...
        //Function that clears the screen, resets the state and set mainText = 0
        private void FunctionalButtonC_Click(object sender, RoutedEventArgs e)
        {
            ClearScreen();
            signLabel.Content = string.Empty;
            HardResetState();
        }
        //Function that adds a coma
        private void FunctionalButtonDot_Click(object sender, RoutedEventArgs e)
        {
            if (!mainText.Text.Contains(",") && !mainText.Text.Contains("∞") && !mainText.Text.Contains("NaN")) mainText.Text += ",";
        }
        //Function that clears the mainText
        private void ClearScreen()
        {
            mainText.Text = string.Empty;;
        }
        //Fuction that resets the state to default setting
        private void ResetState()
        {
            isFirstOperation = true;
        }
        //Function that's like factory reset
        private void HardResetState()
        {
            result = 0;
            isFirstOperation = true;
            mainText.Text = "0";
        }
        //Function that sets the arithmetical operator
        private FunctionalButtons SetOper(string c)
        {
            switch (c)
            {
                case "*":
                    return FunctionalButtons.Multiplication;

                case "/":
                    return FunctionalButtons.Divison;

                case "+":
                    return FunctionalButtons.Addition;

                case "-":
                    return FunctionalButtons.Subtraction;

                default:
                    return FunctionalButtons.Addition;
            }
        }
    }
}
