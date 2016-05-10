using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsingTask.Library;
using UsingTask.Shared;

namespace UsingTask.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new PersonRepository();
            Task<List<Person>> peopleTask = repository.Get();
            peopleTask.ContinueWith(FillConsole,
                TaskContinuationOptions.OnlyOnRanToCompletion);
            peopleTask.ContinueWith(ShowError,
                TaskContinuationOptions.OnlyOnFaulted);
            for (int i = 0; i < 5; i++)
                Console.WriteLine(i);
            Console.ReadLine();
        }

        private static void FillConsole(Task<List<Person>> peopleTask)
        {
            List<Person> people = peopleTask.Result;
            foreach (var person in people)
                Console.WriteLine(person.ToString());
        }

        private static void ShowError(Task<List<Person>> peopleTask)
        {
            foreach (var exception in peopleTask.Exception.Flatten().InnerExceptions)
                Console.WriteLine("Error: {0}", exception.Message);
        }
    }
}
