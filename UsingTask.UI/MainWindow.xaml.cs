using System.Windows;
using System.Collections.Generic;
using UsingTask.Library;
using UsingTask.Shared;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace UsingTask.UI
{
    public partial class MainWindow : Window
    {
        PersonRepository repository = new PersonRepository();
        CancellationTokenSource tokenSource;
        int count = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private IProgress<PersonProgressData> GetFreshProgress()
        {
            count = 0;
            return new Progress<PersonProgressData>(
                d => ProgressTextBlock.Text =
                         $"Processing: {d.Item} of {d.Total}\n {d.Name}");
        }

        private void FetchWithTaskButton_Click(object sender, RoutedEventArgs e)
        {
            tokenSource = new CancellationTokenSource();
            FetchWithTaskButton.IsEnabled = false;
            CancelButton.IsEnabled = true;
            ClearListBox();
            Task<List<Person>> peopleTask = repository.Get(
                GetFreshProgress(), tokenSource.Token);
            peopleTask.ContinueWith(t =>
                {
                    switch (t.Status)
                    {
                        case TaskStatus.Canceled:
                            MessageBox.Show("Operation Canceled", "Canceled");
                            break;
                        case TaskStatus.Faulted:
                            foreach (var exception in t.Exception.Flatten().InnerExceptions)
                                MessageBox.Show(exception.Message, "Error");
                            break;
                        case TaskStatus.RanToCompletion:
                            List<Person> people = t.Result;
                            foreach (var person in people)
                                PersonListBox.Items.Add(person);
                            break;
                        default:
                            break;
                    }
                    FetchWithTaskButton.IsEnabled = true;
                    CancelButton.IsEnabled = false;
                },
                TaskScheduler.FromCurrentSynchronizationContext());
        }

        private async void FetchWithAwaitButton_Click(object sender, RoutedEventArgs e)
        {
            tokenSource = new CancellationTokenSource();
            FetchWithAwaitButton.IsEnabled = false;
            CancelButton.IsEnabled = true;
            try
            {
                ClearListBox();
                List<Person> people = await repository.Get(
                    GetFreshProgress(), tokenSource.Token);
                foreach (var person in people)
                    PersonListBox.Items.Add(person);
            }
            catch (OperationCanceledException ex)
            {
                MessageBox.Show(ex.Message, "Canceled");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                FetchWithAwaitButton.IsEnabled = true;
                CancelButton.IsEnabled = false;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            tokenSource.Cancel();
            CancelButton.IsEnabled = false;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearListBox();
        }

        private void ClearListBox()
        {
            ProgressTextBlock.Text = "";
            PersonListBox.Items.Clear();
        }
    }
}
