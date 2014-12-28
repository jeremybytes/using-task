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
            Task<List<Person>> peopleTask = repository.Get();
            peopleTask.ContinueWith(t =>
                {
                    List<Person> people = peopleTask.Result;
                    foreach (var person in people)
                        PersonListBox.Items.Add(person);
                },
                TaskScheduler.FromCurrentSynchronizationContext());
        }

        private async void FetchWithAwaitButton_Click(object sender, RoutedEventArgs e)
        {
            ClearListBox();
            List<Person> people = await repository.Get();
            foreach (var person in people)
                PersonListBox.Items.Add(person);
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
