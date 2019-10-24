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
using Microsoft.Win32;
using System.Security.Cryptography;
using System.IO;

namespace HashComparer
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeHashAlgorithms();
        }

        private void btn_Browse_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            dialog.Multiselect = false;

            var result = dialog.ShowDialog();
            if (result == true)
            {
                tb_File.Text = dialog.FileName;
            }
        }

        private void btn_Compare_Click(object sender, RoutedEventArgs e)
        {
            // remove special characters from hash
            var equals = String.Equals(
                tb_Hash1.Text.RemoveSpecialCharactersAndWhiteSpace(),
                tb_Hash2.Text.RemoveSpecialCharactersAndWhiteSpace(), 
                StringComparison.OrdinalIgnoreCase);
            if (equals)
            {
                tb_Hash1.Background = tb_Hash2.Background = Brushes.PaleGreen;
            }
            else
            {
                tb_Hash1.Background = tb_Hash2.Background = Brushes.Tomato;
            }
        }

        private void tb_File_TextChanged(object sender, TextChangedEventArgs e)
        {
            ComputeHash();
        }

        private void cbb_HashAlgo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComputeHash();
        }

        private void ComputeHash()
        {
            if (String.IsNullOrWhiteSpace(tb_File.Text)) return;

            var hashAlgo = ((MyHashAlgorithm)cbb_HashAlgo.SelectedItem).Algorithm;
            using (var stream = new FileStream(tb_File.Text, FileMode.Open))
            {
                stream.Position = 0;
                var hash = hashAlgo.ComputeHash(stream);
                tb_Hash1.Text = BitConverter.ToString(hash);
            }
        }

        private string NormalizeHash(string hash)
        {
            return hash.Replace("-", "");
        }

        private void InitializeHashAlgorithms()
        {
            var hashAlgos = new List<MyHashAlgorithm>();
            hashAlgos.Add(new MyHashAlgorithm("MD5", new MD5CryptoServiceProvider()));
            hashAlgos.Add(new MyHashAlgorithm("RIPEMD160", new RIPEMD160Managed()));
            hashAlgos.Add(new MyHashAlgorithm("SHA1", new SHA1CryptoServiceProvider()));
            hashAlgos.Add(new MyHashAlgorithm("SHA256", new SHA256CryptoServiceProvider()));
            hashAlgos.Add(new MyHashAlgorithm("SHA384", new SHA384CryptoServiceProvider()));
            hashAlgos.Add(new MyHashAlgorithm("SHA512", new SHA512CryptoServiceProvider()));

            cbb_HashAlgo.ItemsSource = hashAlgos;
            cbb_HashAlgo.DisplayMemberPath = "Name";
            cbb_HashAlgo.SelectedIndex = 0;
        }
    }
}
