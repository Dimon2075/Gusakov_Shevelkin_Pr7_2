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
                string encrypted = MatrixCipher.MatrixEncrypt(text, rows, cols);
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
                string decrypted = MatrixCipher_d.MatrixDecrypt(encryptedText, rows, cols);
                // Вывод результата
                MessageBox.Show($"Расшифрованный текст: {decrypted}");
                TextInput.Text = decrypted; // Можно также вставить обратно в поле
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}
