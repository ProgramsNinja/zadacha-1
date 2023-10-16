using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace задание_2
{
    interface IFigureCollection : IEnumerable<IFigure> { }
    abstract class FigureCollection : IFigureCollection
    {
        public List<IFigure> figures = new List<IFigure>();
        public IEnumerator<IFigure> GetEnumerator()
        {
            return figures.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
    interface IFigure
    {
        double Perimeter();
        double Area();
        string SpecialProperties();
        void Info();
    }

    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            List<IFigure> figures = new List<IFigure>();
            int tr = 0;
            int re = 0;
            int el = 0;
            for (int i = 0; i < 10; i++)
            {
                int r = rnd.Next(3);
                switch (r)
                {
                    case 0:
                        Rectangle p = new Rectangle(rnd.Next(1, 4), rnd.Next(1, 4));
                        figures.Add(p); re++;
                        break;
                    case 1:
                        Ellipse e = new Ellipse(rnd.Next(1, 4), rnd.Next(1, 4));
                        figures.Add(e); el++;
                        break;
                    case 2:
                        double side1, side2, side3;
                        do
                        {
                            side1 = rnd.Next(1, 10);
                            side2 = rnd.Next(1, 10);
                            side3 = rnd.Next(1, 10);
                        } while (!(side1 + side2 > side3) || !(side1 + side3 > side2) || !(side2 + side3 > side1));
                        Triangle f = new Triangle(side1, side2, side3);
                        figures.Add(f); tr++;
                        break;
                }
            }
            Console.WriteLine("{0,16}{1,14}{2,20}{3,17}{4,20}", "Название:", "Стороны:", "Периметр:", "Площадь:", "Особые свойства:");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            foreach (var figure in figures)
            {
                figure.Info();
            }
            stopwatch.Stop();
            Console.WriteLine(new string('_', 90));
            Console.WriteLine("Время выолнения: " + stopwatch.ElapsedMilliseconds + " миллисекунд");

            Stopwatch stopwatch2 = new Stopwatch();
            IEnumerator<IFigure> enumerator = figures.GetEnumerator();
            stopwatch2.Start();
            while (enumerator.MoveNext())
            {
                IFigure figure = enumerator.Current;
                figure.Info();
            }
            stopwatch2.Stop();
            Console.WriteLine(new string('_', 90));
            Console.WriteLine("Время выолнения: " + stopwatch2.ElapsedMilliseconds + " миллисекунд");

            
            var literk = figures
                .Where(figure => figure.SpecialProperties().StartsWith("К"));


            if (literk.Count() > 0)
            {
                Console.WriteLine("Все фигуры начинающие с буквы К:");
                foreach (var figure in literk)
                {
                    figure.Info();

                }
                Console.WriteLine(new string('_', 90));
            }
            else
            {
                Console.WriteLine("Фигуры на букву 'К' отсутствуют ");
            }
           
            var sortedByPerimetr = figures
                .OrderBy(rectangle => rectangle.Perimeter());
            Console.WriteLine("Фигуры отсортированные по периметру:");
            foreach (var rectangle in sortedByPerimetr)
            {
                rectangle.Info();
            }
            Console.WriteLine(new string('_', 90));

            var groupedByType = figures
                .GroupBy(figure => figure.GetType().Name);

            Console.WriteLine("Группировка фигур по типу:");
            foreach (var group in groupedByType)
            {
                Console.WriteLine("Тип: " + group.Key);
                foreach (var figure in group)
                {
                    figure.Info();
                }
                Console.WriteLine();
            }
            Console.WriteLine(new string('_', 90));


            bool hasTriangles = figures.
                Any(figure => figure is Triangle);
            Console.WriteLine("Есть треугольники: " + hasTriangles);

            bool trin = figures
               .OfType<Triangle>().All(triangle => triangle.type == "Равносторонний");
            Console.WriteLine("Есть равносторонний треугольники: " + trin);

            bool allPerimetrGreaterThan10 = figures
                .All(figure => figure.Perimeter() > 10);
            Console.WriteLine("Все периметры больше 10: " + allPerimetrGreaterThan10);
            
            Console.WriteLine("Количество фигур: " + figures.Count());
            Console.WriteLine("Количество треугольников: " + tr);
            Console.WriteLine("Количество прямоугольников: " + re);
            Console.WriteLine("Количество кругов: " + el);

            double minSquare = figures
               .Min(figure => figure.Area());
            Console.WriteLine("Минимальная площадь: " + minSquare);

            double maxSquare = figures
                 .Max(figure => figure.Area());
            Console.WriteLine("Максимальная площадь: {0:f3}", maxSquare);
            Console.ReadLine();
        }
    }

    public class Rectangle : IFigure
    {
        double length;
        double width;
        string name;

        public Rectangle(double length, double width)
        {
            this.length = length;
            this.width = width;
        }

        public double Perimeter()
        {
            return (length + width) * 2;
        }

        public double Area()
        {
            return length * width;
        }

        public string SpecialProperties()
        {
            return length == width ? name = "Квадрат" : name = "Прямоугольник";
        }

        public void Info()
        {
            Console.WriteLine($"{SpecialProperties(),16}\t({length},{width})\t \t{Perimeter(),10}\t{Area(),10}");
        }
    }

    public class Ellipse : IFigure
    {
        double radius1;
        double radius2;
        string name;

        public Ellipse(double radius1, double radius2)
        {
            this.radius1 = radius1;
            this.radius2 = radius2;
        }

        public double Perimeter()
        {
            return radius1 == radius2
                ? 2 * Math.PI * radius1
                : 4 * ((Math.PI * radius1 * radius2 + Math.Pow(radius1 - radius2, 2)) / (radius1 + radius2));
        }

        public double Area()
        {
            return Math.PI * radius1 * radius2;
        }

        public string SpecialProperties()
        {
            return radius1 == radius2 ? name = "Круг" : name = "Эллипс";
        }

        public void Info()
        {
            Console.WriteLine($"{SpecialProperties(),16}\t({radius1},{radius2})\t \t{Perimeter(),10:f2}\t{Area(),10:f2}");
        }
    }

    public class Triangle : IFigure
    {
        double side1;
        double side2;
        double side3;
        string name;
        public string type;
        public Triangle(double side1, double side2, double side3)
        {
            this.side1 = side1;
            this.side2 = side2;
            this.side3 = side3;

        }

        public double Perimeter()
        {
            return side1 + side2 + side3;
        }

        public double Area()
        {
            double p = (side1 + side2 + side3) / 2;
            double s = Math.Sqrt(p * (p - side1) * (p - side2) * (p - side3));
            return s;
        }

        public string SpecialProperties()
        {
            name = "Треугольник";
            if (side1 == side2 && side2 == side3)
            {
                type = "Равносторонний";
                return type;
            }
            else if (side1 == side2 || side1 == side3 || side2 == side3)
            {
                type = "Равнобедренный";
                return type;
            }
            else
            {
                type = "Разносторонний";
                return type;
            }
        }

        public void Info()
        {
            type = SpecialProperties();
            Console.WriteLine($"{name,16}\t({side1},{side2},{side3})\t \t{Perimeter(),10:f2}\t{Area(),10:f2}\t{type}");

        }
    }
}
