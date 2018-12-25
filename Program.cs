﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab12
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Reflector.WriteClassInfo();

            
            Console.WriteLine("---- MyType:");
            Reflector.ClassPublicMethods("lab12.MyType");

            Console.WriteLine("\n---- Reflector:");
            Reflector.ClassPublicMethods("lab12.Reflector");

            
            Console.WriteLine();
            Reflector.PropertiesAndFields();

            
            Console.WriteLine("\n---- Interfaces in MyType:");
            Reflector.InterfacesInClass();

            
            Console.WriteLine("\n---- Methods with specified param:");
            Reflector.MethodsByParametres("lab12.MyType", "Int32 value");
        }
    }
}