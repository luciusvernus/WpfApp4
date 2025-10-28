using System;
using System.Globalization;
using System.Windows;

namespace WpfApp4
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InputTextBox.Focus();
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            var raw = InputTextBox.Text?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(raw))
            {
                MessageBox.Show("Wprowadź wartość temperatury.", "Brak danych", MessageBoxButton.OK, MessageBoxImage.Information);
                InputTextBox.Focus();
                return;
            }

            // Umożliwiamy przecinek lub kropkę jako separator dziesiętny:
            var normal = raw.Replace(',', '.');

            if (!double.TryParse(normal, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out double value))
            {
                MessageBox.Show("Niepoprawny format liczby. Użyj cyfr, opcjonalnie przecinka lub kropki jako separatora dziesiętnego.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                InputTextBox.Focus();
                InputTextBox.SelectAll();
                return;
            }

            double result;
            string unit;

            if (RbToFahrenheit.IsChecked == true)
            {
                // C -> F
                result = value * 9.0 / 5.0 + 32.0;
                unit = "°F";
            }
            else
            {
                // F -> C
                result = (value - 32.0) * 5.0 / 9.0;
                unit = "°C";
            }

            ResultTextBlock.Text = $"{result:F2} {unit}";
            InputTextBox.Focus();
            InputTextBox.SelectAll();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            InputTextBox.Clear();
            ResultTextBlock.Text = string.Empty;
            InputTextBox.Focus();
        }
    }
}