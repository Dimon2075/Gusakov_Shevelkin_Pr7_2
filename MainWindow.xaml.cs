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

namespace Pr7_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        // Обработчик для шифрования
        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string text = TextInput.Text;
                int rows = int.Parse(RowsInput.Text);
                int cols = int.Parse(ColsInput.Text);
                string encrypted = MatrixEncrypt(text, rows, cols);
                ResultOutput.Text = encrypted;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        // Обработчик для дешифрования
        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string encryptedText = ResultOutput.Text;
                int rows = int.Parse(RowsInput.Text);
                int cols = int.Parse(ColsInput.Text);
                string decrypted = MatrixDecrypt(encryptedText, rows, cols);
                ResultOutput.Text = decrypted;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        // Метод шифрования (по заданию)
        public static string MatrixEncrypt(string text, int rows, int cols)
        {
            if (rows <= 0 || cols <= 0)
                throw new ArgumentException("Размеры матрицы должны быть положительными.");

            int size = rows * cols;
            text = text.PadRight(size, ' ');

            char[,] matrix = new char[rows, cols];

            int index = 0;
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    matrix[r, c] = text[index++];

            StringBuilder encrypted = new StringBuilder();
            for (int c = 0; c < cols; c++)
                for (int r = 0; r < rows; r++)
                    encrypted.Append(matrix[r, c]);

            return encrypted.ToString();
        }

        // Метод дешифрования
        public static string MatrixDecrypt(string text, int rows, int cols)
        {
            if (rows <= 0 || cols <= 0)
                throw new ArgumentException("Размеры матрицы должны быть положительными.");

            int size = rows * cols;
            if (text.Length != size)
                throw new ArgumentException("Длина зашифрованного текста не совпадает с размером матрицы.");

            char[,] matrix = new char[rows, cols];

            int index = 0;
            // Заполняем по столбцам
            for (int c = 0; c < cols; c++)
                for (int r = 0; r < rows; r++)
                    matrix[r, c] = text[index++];

            StringBuilder original = new StringBuilder();
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    original.Append(matrix[r, c]);

            return original.ToString().TrimEnd(); // Убираем добавленные пробелы
        }
    }
}
