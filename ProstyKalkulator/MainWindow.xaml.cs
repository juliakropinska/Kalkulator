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

namespace ProstyKalkulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            tbScreen.Text = "0";

        }

        private string _firstValue;

        private string _secondValue;

        private string clickedValue;
        private void OnBtnNumberClick(object sender, RoutedEventArgs e)
        {
            if (tbScreen.Text == "0")
                tbScreen.Text = string.Empty;

            var clickedValue = (sender as Button).Content;
            tbScreen.Text += clickedValue;

            if (_currentOperation != Operation.None)
                _secondValue += clickedValue;

            if (_isTheResultOnTheScreen)
            {
                _isTheResultOnTheScreen = false;
                tbScreen.Text = string.Empty;
            }

            SetOperationBtnState(true);

            if (_currentOperation != Operation.None)
                _secondValue += clickedValue;
            else
                SetOperationBtnState(true);

            SetResultBtnState(true);

            SetOperationBtnState(true);
            SetResultBtnState(true);

        }

        public enum Operation
        {
            None,
            Addition,
            Subtraction,
            Division,
            Multiplication
        }

        private Operation _currentOperation = Operation.None;
        private void OnBtnOperationClick(object sender, RoutedEventArgs e)
        {
            var operation = (sender as Button).Content;
            _firstValue = tbScreen.Text;
            tbScreen.Text += $" {operation} ";
            _currentOperation = operation switch
            {
                "+" => Operation.Addition,
                "-" => Operation.Subtraction,
                "/" => Operation.Division,
                "*" => Operation.Multiplication,
                _ => Operation.None,
            };


        }


        private void OnBtnResultClick(object sender, RoutedEventArgs e)
        {
            var firstNumber = double.Parse(_firstValue);
            var secondNumber = double.Parse(_secondValue);

            var result = 0d;
            switch (_currentOperation)
            {
                case Operation.None:
                    result = firstNumber;
                    break;
                case Operation.Addition:
                    result = firstNumber + secondNumber;
                    break;
                case Operation.Subtraction:
                    result = firstNumber - secondNumber;
                    break;
                case Operation.Division:
                    result = firstNumber / secondNumber;
                    break;
                case Operation.Multiplication:
                    result = firstNumber * secondNumber;
                    break;
            }
            _isTheResultOnTheScreen = true;

            if (_currentOperation == Operation.None)
                return;



        }
        private bool _isTheResultOnTheScreen;

        private double Calculate(double firstNumber, double secondNumber)
        {
            var result = Calculate(firstNumber, secondNumber);

            tbScreen.Text = result.ToString();

            _secondValue = string.Empty;
            _currentOperation = Operation.None;

            switch (_currentOperation)

            {
                case Operation.None:
                    return firstNumber;
                case Operation.Addition:
                    return firstNumber + secondNumber;
                case Operation.Subtraction:
                    return firstNumber - secondNumber;
                case Operation.Division:
                    if (secondNumber == 0)
                    {
                        MessageBox.Show("Nie można dzielić przez 0!");
                        return 0;
                    }
                    return firstNumber / secondNumber;
                case Operation.Multiplication:
                    return firstNumber * secondNumber;
            }
            return 0;



        }

        private void OnBtnClearClick(object sender, RoutedEventArgs e)
        {
            tbScreen.Text = "0";
            _firstValue = string.Empty;
            _secondValue = string.Empty;
            _currentOperation = Operation.None;

            if (tbScreen.Text == "0" && clickedValue != ",")
                tbScreen.Text = string.Empty;

            if (_isTheResultOnTheScreen)
            {
                _isTheResultOnTheScreen = false;
                tbScreen.Text = string.Empty;

                if (clickedValue == ",")
                    clickedValue = "0,";
            }

        }

        private void SetOperationBtnState(bool value)
        {
            btnAdd.IsEnabled = value;
            btnMultiplication.IsEnabled = value;
            btnDivision.IsEnabled = value;
            btnSubtraction.IsEnabled = value;
        }

        private void SetResultBtnState(bool value)
        {
            btnResult.IsEnabled = value;
            SetOperationBtnState(true);
            SetResultBtnState(true);

            if (_isTheResultOnTheScreen)
                _isTheResultOnTheScreen = false;
        }


    }
}
