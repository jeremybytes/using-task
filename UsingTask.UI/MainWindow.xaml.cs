using System.Windows;
using System.Collections.Generic;
using UsingTask.Library;
using UsingTask.Shared;
using System.Threading.Tasks;

namespace UsingTask.UI
{
    public partial class MainWindow : Window
    {
        PersonRepository repository = new PersonRepository();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void FetchWithTaskButton_Click(object sender, RoutedEventArgs e)
        {
            ClearListBox();
        }

        private void FetchWithAwaitButton_Click(object sender, RoutedEventArgs e)
        {
            ClearListBox();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearListBox();
        }

        private void ClearListBox()
        {
            PersonListBox.Items.Clear();
        }
    }
}
