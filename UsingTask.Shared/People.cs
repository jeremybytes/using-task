using System;
using System.Collections.Generic;

namespace UsingTask.Shared
{
    public static class People
    {
        public static List<Person> GetPeople()
        {
            var p = new List<Person>()
            {
                new Person() { Id=1, FirstName="John", LastName="Koenig",
                    StartDate = new DateTime(1975, 10, 17), Rating=6 },
                new Person() { Id=2, FirstName="Dylan", LastName="Hunt", 
                    StartDate = new DateTime(2000, 10, 2), Rating=8 },
                new Person() { Id=3, FirstName="John", LastName="Crichton", 
                    StartDate = new DateTime(1999, 3, 19), Rating=7 },
                new Person() { Id=4, FirstName="Dave", LastName="Lister", 
                    StartDate = new DateTime(1988, 2, 15), Rating=9 },
                new Person() { Id=5, FirstName="John", LastName="Sheridan", 
                    StartDate = new DateTime(1994, 1, 26), Rating=6 },
                new Person() { Id=6, FirstName="Dante", LastName="Montana", 
                    StartDate = new DateTime(2000, 11, 1), Rating=5 },
                new Person() { Id=7, FirstName="Isaac", LastName="Gampu", 
                    StartDate = new DateTime(1977, 9, 10), Rating=4 }
            };
            return p;
        }
    }
}
