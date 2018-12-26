using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Runtime.Serialization.Json;

namespace Lab14
{
    [Serializable]
    [DataContract]
    public class Point
    {
        [DataMember(Name = "CoordinateX")]
        public int x;
        [DataMember(Name ="CoordinateY")]
        public int y;

        public Point() { }

        public Point(int v1, int v2)
        {
            x = v1;
            y = v2;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Point dot = new Point(100, 500);

            #region Bin
            BinaryFormatter binForm = new BinaryFormatter();

            using(FileStream fs = new FileStream("points.dat", FileMode.OpenOrCreate))
            {
                binForm.Serialize(fs, dot);
            }

            Console.WriteLine("\nБинарный формат:");
            using (FileStream fs = new FileStream("points.dat", FileMode.OpenOrCreate))
            {
                Point newDot = (Point)binForm.Deserialize(fs);
                Console.WriteLine($"координата x: {newDot.x}, координата y: {newDot.y}.");
            }
            #endregion

            #region SOAP
            SoapFormatter soapForm = new SoapFormatter();

            using(FileStream fs = new FileStream("points.soap", FileMode.OpenOrCreate))
            {
                soapForm.Serialize(fs, dot);
            }

            Console.WriteLine("\nSOAP:");
            using (FileStream fs = new FileStream("points.soap", FileMode.OpenOrCreate))
            {
                Point newDot = (Point)soapForm.Deserialize(fs);
                Console.WriteLine($"координата x: {newDot.x}, координата y: {newDot.y}.");

            }
            #endregion

            #region XML
            XmlSerializer xmlSer = new XmlSerializer(typeof(Point));

            using(FileStream fs = new FileStream("points.xml", FileMode.OpenOrCreate))
            {
                xmlSer.Serialize(fs, dot);
            }

            Console.WriteLine("\nXML:");
            using (FileStream fs = new FileStream("points.xml", FileMode.OpenOrCreate))
            {
                Point newDot = xmlSer.Deserialize(fs) as Point;
                Console.WriteLine($"координата x: {newDot.x}, координата y: {newDot.y}.");
            }
            #endregion

            #region JSON
            Point dot1 = new Point(4, 7);
            Point dot2 = new Point(11, 3);
            Point[] points = new Point[] { dot1, dot2 };

            DataContractJsonSerializer jsonForm = new DataContractJsonSerializer(typeof(Point[]));

            using (FileStream fs = new FileStream("points.json", FileMode.OpenOrCreate))
            {
                jsonForm.WriteObject(fs, points);
            }

            Console.WriteLine("\nJSON (массив):");
            using (FileStream fs = new FileStream("points.json", FileMode.OpenOrCreate))
            {
                Point[] newPoints = (Point[])jsonForm.ReadObject(fs);
                foreach(Point p in points)
                {
                    Console.WriteLine($"координата x: {p.x}, координата y: {p.y}.");
                }
            }
            #endregion

            #region XML Selectors
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("points.xml");
            XmlElement xRoot = xDoc.DocumentElement;

            Console.WriteLine("\nСелекторы:");
            Console.WriteLine("\tПервый селектор (все узлы):");
            XmlNodeList childnodes1 = xRoot.SelectNodes("*");
            foreach(XmlNode node in childnodes1)
            {
                Console.WriteLine("\t" + node.OuterXml);
            }

            Console.WriteLine("\n\tВторой селектор (по имени):");
            XmlNodeList childnodes2 = xRoot.SelectNodes("x");
            foreach (XmlNode node in childnodes2)
            {
                Console.WriteLine("\t" + node.OuterXml);
            }
            #endregion
        }
    }
}