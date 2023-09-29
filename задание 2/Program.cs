using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace задание_2
{
    class prograrm
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            List<Figure> figures = new List<Figure>();
            for (int i = 0; i < 10; i++)
            {
                int r = rnd.Next(3);
                switch (r)
                {
                    case 0:
                        Rectangle p = new Rectangle(rnd.Next(1,10), rnd.Next(1,10));
                        figures.Add(p);
                        break;
                    case 1:
                        Elipses e = new Elipses(rnd.Next(1,10), rnd.Next(1,10));
                        figures.Add(e);
                        break;
                    case 2:
                        bool d = true;
                        while (d)
                        {
                            double a = rnd.Next(1,10);
                            double b = rnd.Next(1,10);
                            double c = rnd.Next(1,10);
                            if (a + b > c && a + c > b &&b + c > a)
                            {
                                Triangle f = new Triangle(a, b, c);
                                figures.Add(f);
                                d = false;
                            }
                        }
                       break;
                }   
            }
            Console.WriteLine("{0,16}{1,14}{2,21}{3,17}{4,20}","Название:","Стороны:","Периметр:","Площадь:","Особые свойства:");
            foreach (var figure in figures )
            {
                figure.Info();
            }
            Console.ReadLine();
        }
    }
    public abstract class Figure
    {
        public abstract double Perimetr();
        public abstract double Area();
        public abstract void Info();
    }
    class Rectangle:Figure
    {
        double lenght;
        double width;

        public Rectangle(double lenght,double width)
        {
            this.lenght = lenght;
            this.width = width;
        }

        public override double Perimetr()
        {
            return (lenght + width)*2;
        }

        public override double Area()
        {
            return lenght * width;
        }

        public override void Info()
        {
            if (lenght == width)
                Console.WriteLine($"{"Квадрат",16}\t({lenght},{width})\t \t{Perimetr(),10}\t{Area(),10}");
            else
                Console.WriteLine($"{"Прямоугольник",16}\t({lenght},{width})\t \t{Perimetr(),10}\t{Area(),10}");
        }
    }
    class Elipses : Figure
    {
        double radius1;
        double radius2;

        public Elipses(double radius1,double radius2)
        {
            this.radius1 = radius1;
            this.radius2 = radius2;
        }

        public override double Perimetr()
        {
            if (radius1 != radius2)
                return 4 * ((Math.PI * radius1 * radius2 + Math.Pow(radius1 - radius2, 2)) / (radius1 + radius2));
            else
                return 2 * Math.PI * radius1;
        }

        public override double Area()
        {
            return Math.PI*radius1*radius2;
        }

        public override void Info()
        {
            if (radius1 == radius2)
               Console.WriteLine($"{"Круг",16}\t({radius1},{radius2})\t \t{Perimetr(),10:f2}\t{Area(),10:f2}");
            else
                Console.WriteLine($"{"Элипс",16}\t({radius1},{radius2})\t \t{Perimetr(),10:f2}\t{Area(),10:f2}");
        }
    }
    class Triangle:Figure
    {
        double side1;
        double side2;
        double side3;

        public Triangle(double side1,double side2,double side3)
        {
            this.side1 = side1;
            this.side2 = side2;
            this.side3 = side3;
        }

        public override double Perimetr()
        {
            return side1 + side2 + side3;
        }

        public override double Area()
        {
            double p = (side1 + side2 + side3) / 2;
            double s = Math.Sqrt(p*(p-side1)*(p-side2)*(p-side3));
            return s;
        }

        public override void Info()
        {
            if (side1 == side2 && side2 == side3)
                Console.WriteLine($"{"Треугольник",16}\t({side1},{side2},{side3})\t \t{Perimetr(),10:f2}\t{Area(),10:f2}\t{"Равносторонний"}");
            else if (side1 == side2 || side1 == side3 || side2 == side3)
                Console.WriteLine($"{"Треугольник",16}\t({side1},{side2},{side3})\t \t{Perimetr(),10:f2}\t{Area(),10:f2}\t{"Равнобедренный"}");
            else
                Console.WriteLine($"{"Треугольник",16}\t({side1},{side2},{side3})\t \t{Perimetr(),10:f2}\t{Area(),10:f2}\t{"Разносторонний"}");
        }
    }
}
