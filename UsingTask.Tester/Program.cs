using System;
using System.Collections.Generic;
using UsingTask.Library;
using UsingTask.Shared;

namespace UsingTask.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new PersonRepository();
            List<Person> people = repository.Get().Result;
            foreach (var person in people)
                Console.WriteLine(person.ToString());
            Console.ReadLine();
        }
    }
}
