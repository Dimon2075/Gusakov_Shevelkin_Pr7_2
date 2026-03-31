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
    /// Логика взаимодействия для MainWindow.xaml — основной класс окна.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Конструктор — инициализация компонента окна.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик нажатия на кнопку "Зашифровать".
        /// Производит шифрование текста по матрице.
        /// </summary>
        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получение текста из поля ввода
                string text = TextInput.Text;
                if (string.IsNullOrWhiteSpace(text))
                {
                    MessageBox.Show("Введите текст для шифрования!");
                    return;
                }

                // Проверка ввода размеров матрицы
                if (!int.TryParse(RowsInput.Text, out int rows) || !int.TryParse(ColsInput.Text, out int cols))
                {
                    MessageBox.Show("Введите корректные целые числа в поля 'Строки' и 'Столбцы'.");
                    return;
                }

                // Проверка, что текст помещается в матрицу
                int capacity = rows * cols;
                if (text.Length > capacity)
                {
                    MessageBox.Show($"Текст слишком длинный ({text.Length} симв.) для матрицы {rows}x{cols} ({capacity} мест).\n" +
                                    $"Увеличьте количество строк или столбцов.");
                    return;
                }

                // Выполнение шифрования
                string encrypted = MatrixEncrypt(text, rows, cols);
                ResultOutput.Text = encrypted;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Критическая ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Расшифровать".
        /// Восстановление исходного текста из зашифрованной строки.
        /// </summary>
        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получение зашифрованного текста из результата
                string encryptedText = ResultOutput.Text;

                if (string.IsNullOrWhiteSpace(encryptedText)) return;

                // Вытягивание размеров матрицы из полей
                int rows = int.Parse(RowsInput.Text);
                int cols = int.Parse(ColsInput.Text);

                // Дешифрование
                string decrypted = MatrixDecrypt(encryptedText, rows, cols);
                // Вывод результата
                MessageBox.Show($"Расшифрованный текст: {decrypted}");
                TextInput.Text = decrypted; // Можно также вставить обратно в поле
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Метод шифрования текста по матрице с перестановкой по столбцам.
        /// </summary>
        /// <param name="text">Исходный текст.</param>
        /// <param name="rows">Количество строк матрицы.</param>
        /// <param name="cols">Количество столбцов матрицы.</param>
        /// <returns>Зашифрованный текст.</returns>
        public static string MatrixEncrypt(string text, int rows, int cols)
        {
            int size = rows * cols;
            // Дополняем текст пробелами, чтобы он точно заполнил матрицу
            text = text.PadRight(size, ' ');

            char[,] matrix = new char[rows, cols];
            int index = 0;

            // Заполнение матрицы построчно
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    matrix[r, c] = text[index++];

            // Чтение по столбцам
            StringBuilder sb = new StringBuilder();
            for (int c = 0; c < cols; c++)
                for (int r = 0; r < rows; r++)
                    sb.Append(matrix[r, c]);

            return sb.ToString();
        }

        /// <summary>
        /// Метод дешифровки текста, восстановление исходного текста из зашифрованной строки.
        /// </summary>
        /// <param name="text">Зашифрованный текст.</param>
        /// <param name="rows">Количество строк в матрице.</param>
        /// <param name="cols">Количество столбцов в матрице.</param>
        /// <returns>Восстановленный исходный текст.</returns>
        public static string MatrixDecrypt(string text, int rows, int cols)
        {
            int size = rows * cols;
            // Дополняем текст до размера матрицы, если нужно
            if (text.Length < size)
                text = text.PadRight(size, ' ');

            char[,] matrix = new char[rows, cols];
            int index = 0;

            // Заполнение матрицы по столбцам (так мы читали при шифровании)
            for (int c = 0; c < cols; c++)
                for (int r = 0; r < rows; r++)
                    matrix[r, c] = text[index++];

            // Восстановление текста по строкам
            StringBuilder sb = new StringBuilder();
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    sb.Append(matrix[r, c]);

            // Удаление лишних пробелов в конце
            return sb.ToString().TrimEnd();
        }
    }
}
