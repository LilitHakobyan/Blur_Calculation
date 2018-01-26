using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace test
{


    public class Human
    {
        public int Age { get; set; }
        public string Name { get; set; }

        public Human(int hage, string hname)
        {
            Age = hage;
            Name = hname;
        }

        public void DisplayHuman()
        {
            Console.WriteLine($"Human Name={Name}, Human Age={Age}");
        }

    }

    class Program
    {
        public static void ChangeHuman(ref Human h)
        {

            //h.Age = 99;
            //h.Name = "ha";
            h = new Human(33, "Narek");
        }

        static void Main(string[] args)
        {
            Human createHuman = new Human(55, "David");

            createHuman.DisplayHuman();
            ChangeHuman(ref createHuman);
            createHuman.DisplayHuman();

        }
    }
}
