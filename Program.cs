using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab10
{
    public class Student
    {
        public string Name;
        public int Course;
        public Student()
        {

        }
        public Student(string name, int course)
        {
            Name = name;
            Course = course;
        }
        public override string ToString()
        {
            return Name + ' ' + Course + ' ' + base.ToString();
        }
   
    }
    
    class Program
    {
        private static void CollectionChanged(object sender,
            System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add: // если добавление
                    Student newUser = e.NewItems[0] as Student;
                    Console.WriteLine("Добавлен новый объект: {0}", newUser.Name);
                    break;
                case NotifyCollectionChangedAction.Remove: // если удаление
                    Student oldUser = e.OldItems[0] as Student;
                    Console.WriteLine("Удален объект: {0}", oldUser.Name);
                    break;
                case NotifyCollectionChangedAction.Replace: // если замена
                    Student replacedUser = e.OldItems[0] as Student;
                    Student replacingUser = e.NewItems[0] as Student;
                    Console.WriteLine("Объект {0} заменен объектом {1}",
                                        replacedUser.Name, replacingUser.Name);
                    break;
            }
        }
        static void Main(string[] args)
        {
            ArrayList arr1 = new ArrayList();
            for(int i=0;i<5;i++)
            {
                Random rand = new Random();
                int r = rand.Next();
                arr1.Add(r);
            }
            arr1.Add("lolkek");
            Student student = new Student("John", 2);
            arr1.Add(student);
            Console.WriteLine("Array:");
            foreach (object item in arr1)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
            arr1.RemoveAt(2);
            string examp = Console.ReadLine();
            if (arr1.Contains(examp))
            {
                Console.WriteLine("Array contains element");
            }
            else
            {
                Console.WriteLine("Array doesn't contain element");
            }
            Console.WriteLine("New array:");
            foreach (object item in arr1)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();



            Dictionary<double, string> arr2 = new Dictionary<double, string>();
            arr2.Add(1.002, "lol");
            arr2.Add(2.001, "kek");
            arr2.Add(1.001, "cheburek");
            arr2.Add(2.002, "pelmen");
            Console.WriteLine("Dictionary:");
            foreach (object item in arr2)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
            var keysToRemove = new List<double>();
            foreach (var e in arr2)
            {
                if (e.Key < 2)
                {
                    keysToRemove.Add(e.Key);
                }
            }

            foreach (var key in keysToRemove)
            {
                arr2.Remove(key);
            }
            arr2[1.25] = "queen";
            arr2[3.665] = "we";
            arr2[2.005] = "will";
            arr2[7.321] = "rock";
            arr2[0.1111] = "you";
            Console.WriteLine("New dictionary:");
            foreach (object item in arr2)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            Queue <KeyValuePair<double, string>> queue = new Queue<KeyValuePair<double,string>>();
            foreach (var item in arr2)
            {
                queue.Enqueue(item);
            }
            Console.WriteLine("Queue:");
            foreach (var item in queue)
            {
                Console.WriteLine(item);
            }
            string search = Console.ReadLine();
            Console.WriteLine(queue.First((n) => (n.Value == search)));


            Queue<Student> Students = new Queue<Student>();
            Student[] students = new Student[4];
            students[0] = new Student("fred", 3);
            students[1] = new Student("brian", 4);
            students[2] = new Student("roger", 4);
            students[3] = new Student("john", 2);

            foreach (var item in students)
            {
                Students.Enqueue(item);
            }
            foreach (var item in Students)
            {
                Console.WriteLine(item);
            }



            ObservableCollection<Student> obsev = new ObservableCollection<Student>();
            obsev.CollectionChanged += CollectionChanged;
            obsev.Add(students[1]);
            obsev.Add(students[2]);
            obsev.Add(students[0]);
            obsev.RemoveAt(1);
            obsev.Insert(1, students[3]);
            obsev[0] = students[2];
            

            Console.ReadKey();
        }
    }
}
