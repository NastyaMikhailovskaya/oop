using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Threading;

namespace Lab.work_15
{
    public class Threads
    {
        public Thread firstTread;
        public Thread secondTread;
        public int n;

        public Threads(int _n)
        {
            firstTread = new Thread(this.IsEven);
            secondTread = new Thread(this.IsOdd) { Priority = ThreadPriority.AboveNormal };
            n = _n;
        }

        public void IsEven()
        {
            Console.Write("\t\t");
            for (int i = 0; i < n; i++)
            {
                if (i % 2 == 0)
                {
                    Console.Write(i + " ");
                }
                Thread.Sleep(50);
            }
        }

        public void IsOdd()
        {
            for (int i = 0; i < n; i++)
            {
                if (i % 2 != 0)
                {
                    Console.Write(i + " ");
                }
                Thread.Sleep(50);
            }
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            var allProcesses = Process.GetProcesses();
            foreach (Process proc in allProcesses)
            {
                Console.WriteLine($"Имя: {proc.ProcessName}, ID: {proc.Id}, приоритет {proc.BasePriority}.\n");
            }

            #region Домены
            Console.WriteLine("Текущий домен: ");
            AppDomain dom = AppDomain.CurrentDomain;
            Console.WriteLine("Имя: " + dom.FriendlyName);
            Console.WriteLine("Текущая конфигурация: " + dom.SetupInformation);

            Console.WriteLine("Сборки, загруженные в текущий домен: ");
            Assembly[] assemblies = dom.GetAssemblies();
            foreach (Assembly a in assemblies)
            {
                Console.WriteLine("\t" + a.GetName().Name);
            }

            AppDomain newDom = AppDomain.CreateDomain("Новый домен.");
            string path = dom.BaseDirectory + "Lab.work_15.exe";
            AppDomain.Unload(newDom);
            #endregion

            #region Потоки
            Console.WriteLine("\nПростые числа от 1 до заданного: ");

            Thread thread = new Thread(Num);
            thread.Start();
            thread.Name = "Запись простых чисел в файл и вывод в консольное окно.";
            Console.WriteLine("\nСтатус: " + thread.ThreadState + " Имя: " + thread.Name + "\nПриоритет: " + thread.Priority + "\nЧисловой идентификатор: " + thread.ManagedThreadId);
            thread.Join();
            #endregion

            #region Два потока
            Console.WriteLine("\nЧётные и нечётные числа:");

            Console.Write("\tВведите количесвто чисел: ");
            int n = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("\n\t\tЗапуск потоков по очереди: ");
            Threads thrhan = new Threads(n);
            thrhan.firstTread.Start();
            thrhan.firstTread.Join();
            thrhan.secondTread.Start();
            thrhan.secondTread.Join();
            Console.WriteLine();

            Console.WriteLine("\n\t\tЗапуск одновременно: ");
            Threads thrhan1 = new Threads(n);
            thrhan1.firstTread.Start();
            thrhan1.secondTread.Start();
            thrhan1.firstTread.Join();
            thrhan1.secondTread.Join();
            Console.WriteLine();
            #endregion

            #region Timer
            TimerCallback timerDel = new TimerCallback(ForTimer);
            Timer timer = new Timer(timerDel, 0, 0, 1000);
            Console.ReadKey();
            #endregion
        }

        public static void Num()
        {
            Console.Write("\n Введите количесвто чисел: ");

            int n = Convert.ToInt32(Console.ReadLine());
            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
            {
                arr[i] = i + 1;
            }

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(arr[i] + " ");
                using (StreamWriter file = new StreamWriter("D:\\учёба\\2к\\ООП\\Laboratory works\\Lab. work 15\\bin\\Debug\\fs.txt", true, System.Text.Encoding.Default))
                {
                    file.WriteLine(arr[i] + " ");
                }
            }
        }

        public static void ForTimer(object obj)
        {
            Console.WriteLine("Hoba!");
        }
    }
}
