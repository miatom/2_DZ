using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CetvrtiZad
{
    class Program
    {
        static void Main(string[] args)
        {
            Example1();
            Example2();
            Console.ReadLine();
        }

        public static void Example1()
        {
            var list = new List<Student>()

            {
            new Student ("Ivan", jmbag :" 001234567 ",spol: Gender.Male)

            };
            var ivan = new Student("Ivan", jmbag: " 001234567 ",spol:Gender.Male); // nije dio liste
            list.Add(ivan); //dodajemo ga u listu
            
            bool anyIvanExists = list.Any(s=> s.Name == "Ivan"); // vratit će true
            Console.WriteLine(anyIvanExists);

        }
       public static void Example2()
        {
            var list = new List<Student>()
            {
                new Student (" Ivan ", jmbag :" 001234567 ",spol:Gender.Male) ,
                new Student (" Ivan ", jmbag :" 001234567 ",spol:Gender.Male)
            };
            
            var distinctStudents =FindDistinct(list);
            Console.WriteLine(distinctStudents);
        }
        public static int FindDistinct(List<Student> lista)
        {
            var find = lista.GroupBy(s => s.Jmbag);
            return find.Count();

        }

    }
    public class Student
    {
        public string Name { get; set; }
        public string Jmbag { get; set; }
        public Gender Gender { get; set; }
        private List<Student> listaStudenata;


        public Student(string name, string jmbag,Gender spol)
        {
            
            Name = name;
            Jmbag = jmbag;
            Gender = spol;
        }

        
        
    }
    public enum Gender
    {
        Male, Female
    }
    

}
