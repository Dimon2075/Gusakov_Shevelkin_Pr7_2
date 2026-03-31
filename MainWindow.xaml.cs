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
                // 1. Проверка на пустой текст
                string text = TextInput.Text;
                if (string.IsNullOrWhiteSpace(text))
                {
                    MessageBox.Show("Введите текст для шифрования!");
                    return;
                }

                // 2. Безопасное чтение чисел (чтобы не было ошибки, если в полях буквы)
                if (!int.TryParse(RowsInput.Text, out int rows) || !int.TryParse(ColsInput.Text, out int cols))
                {
                    MessageBox.Show("Введите корректные целые числа в поля 'Строки' и 'Столбцы'.");
                    return;
                }

                // 3. Проверка: влезет ли текст в матрицу?
                int capacity = rows * cols;
                if (text.Length > capacity)
                {
                    MessageBox.Show($"Текст слишком длинный ({text.Length} симв.) для матрицы {rows}x{cols} ({capacity} мест).\n" +
                                    $"Увеличьте количество строк или столбцов.");
                    return;
                }

                // 4. Шифрование
                string encrypted = MatrixEncrypt(text, rows, cols);
                ResultOutput.Text = encrypted;

                // Опционально: можно очистить верхнее поле, чтобы не путаться
                // TextInput.Clear(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Критическая ошибка: {ex.Message}");
            }
        }

        // Обработчик для дешифрования
        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Берем текст ПРЯМО из результата для расшифровки
                string encryptedText = ResultOutput.Text;

                if (string.IsNullOrWhiteSpace(encryptedText)) return;

                int rows = int.Parse(RowsInput.Text);
                int cols = int.Parse(ColsInput.Text);

                string decrypted = MatrixDecrypt(encryptedText, rows, cols);

                // Выводим расшифрованный текст обратно в верхнее поле или в MessageBox
                MessageBox.Show($"Расшифрованный текст: {decrypted}");
                TextInput.Text = decrypted;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        // Метод шифрования (по заданию)
        public static string MatrixEncrypt(string text, int rows, int cols)
        {
            int size = rows * cols;
            // 1. Дополняем текст пробелами, чтобы матрица была полной
            text = text.PadRight(size, ' ');

            char[,] matrix = new char[rows, cols];
            int index = 0;

            // 2. ЗАПИСЬ: идем по СТРОКАМ (горизонтально)
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    matrix[r, c] = text[index++];

            // 3. ЧТЕНИЕ: идем по СТОЛБЦАМ (вертикально)
            StringBuilder sb = new StringBuilder();
            for (int c = 0; c < cols; c++)
                for (int r = 0; r < rows; r++)
                    sb.Append(matrix[r, c]);

            return sb.ToString();
        }

        // ИСПРАВЛЕННЫЙ МЕТОД ДЕШИФРОВАНИЯ
        public static string MatrixDecrypt(string text, int rows, int cols)
        {
            int size = rows * cols;
            // Если текст короче матрицы (вдруг обрезался), дополняем
            if (text.Length < size) text = text.PadRight(size, ' ');

            char[,] matrix = new char[rows, cols];
            int index = 0;

            // 1. ЗАПОЛНЯЕМ ПО СТОЛБЦАМ (как читали при шифровании)
            for (int c = 0; c < cols; c++)
                for (int r = 0; r < rows; r++)
                    matrix[r, c] = text[index++];

            // 2. ЧИТАЕМ ПО СТРОКАМ (восстанавливаем оригинал)
            StringBuilder sb = new StringBuilder();
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    sb.Append(matrix[r, c]);

            return sb.ToString().TrimEnd();
        }
    }
}
