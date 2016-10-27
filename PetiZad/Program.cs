using CetvrtiZad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetiZad
{
    class Program
    {
        static void Main(string[] args)
        {
            Student[] studenti = new Student[2];
            studenti[0] = new Student("MT","0036471019",Gender.Female);
            studenti[1] = new Student("TT","0036471020",Gender.Male);

            Student[] stud = new Student[2];
            stud[0]= new Student("MT", "0036471019",Gender.Female);
            stud[1]= new Student("TT", "0036471021",Gender.Male);

            Student[] stud1 = new Student[2];
            stud1[0] = new Student("KK", "0036471023",Gender.Male);
            stud1[1] = new Student("TF", "0036471045",Gender.Male);

            var uni = new List<University>()
            {
                new University("FER",studenti),
                new University("FFZG",stud),
                new University("EFZG",stud1)
            };

            //LINQ izrazi
            var listaStudenataRH = //svi studenti na sveučilištima
                from item in uni
                select item.Students;

            var lista = listaStudenataRH.SelectMany(w=>w); //lista svih studenata, još ih treba grupirati po jmbagu
            var ListaJedinstvenih = lista.GroupBy(w=>w.Jmbag).Select(w=>w.First());
            //studenti koji studiraju na dva ili više fakulteta
            var studirajuNaDvaIliVise = lista.GroupBy(w => w.Jmbag).Where(w=>w.Count()>=2).Select(w=>w.First());
            //studenti koji studiraju na fakultetima na kojima nema žena
            var listaStudenataPoSveucilistu = uni.GroupBy(w => w.Name).SelectMany(w=>w.Where(s=>s.Students.Where(h=>h.Gender==Gender.Female).Count()==0).SelectMany(h=>h.Students));//grupirana sveučilišta
            var listaStudenata = listaStudenataPoSveucilistu.GroupBy(w => w.Jmbag).Select(w => w.First());


            //ispis
            Console.WriteLine("Jedinstvena lista studenata:");
            foreach (Student item in ListaJedinstvenih)
            {
                Console.WriteLine("Ime: {0} jmbag: {1}",item.Name,item.Jmbag);
            }
            Console.WriteLine("Studenti koji studiraju na dva ili više fakulteta:");
            foreach (Student item in studirajuNaDvaIliVise)
            {
                Console.WriteLine("Ime: {0} jmbag: {1}", item.Name, item.Jmbag);
            }
            Console.WriteLine("Studenti koji studiraju fakultetima na kojima nema žena:");
            foreach (Student item in listaStudenata)
            {
                Console.WriteLine("Ime: {0} jmbag: {1}", item.Name,item.Jmbag);
            }
            Console.ReadLine();
              


        }
    }
    public class University
    {
        public string Name { get; set; }
        public Student[] Students { get; set; }
        public University(string name, Student[] studenti)
        {
            Name = name;
            Students = studenti;
        }
    }

}
