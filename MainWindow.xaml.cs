using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReFileNameHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectFolderButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                this.FolderPathTextBox.Text = dialog.SelectedPath;
            }
        }

        private void RenameButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.FolderPathTextBox.Text))
            {
                MessageBox.Show("请先选择文件夹路径。");
                return;
            }

            if (string.IsNullOrEmpty(this.FileNamePrefixTextBox.Text))
            {
                MessageBox.Show("请输入文件名前缀。");
                return;
            }

            if (!int.TryParse(this.StartNumberTextBox.Text, out int startNumber))
            {
                MessageBox.Show("请输入文件名起始编号。");
                return;
            }

            var directory = new DirectoryInfo(this.FolderPathTextBox.Text);
            var files = directory.GetFiles().OrderBy(f => f.CreationTimeUtc);
            int index = startNumber;

            foreach (var file in files)
            {
                string extension = Path.GetExtension(file.FullName);
                string newName = string.Format("{0}_{1:D3}{2}", this.FileNamePrefixTextBox.Text, index, extension);
                string newPath = Path.Combine(directory.FullName, newName);
                file.MoveTo(newPath);
                index++;
            }

            MessageBox.Show("文件重命名完成。");
        }

    }
}
