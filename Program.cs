using System;

namespace LabWork
{
    // Даний проект є шаблоном для виконання лабораторних робіт
    // з курсу "Об'єктно-орієнтоване програмування та патерни проектування"
    // Необхідно змінювати і дописувати код лише в цьому проекті
    // Відео-інструкції щодо роботи з github можна переглянути 
    // за посиланням https://www.youtube.com/@ViktorZhukovskyy/videos 

    public static class Randomizer
    {
        private static Random _random = new Random();
        
        public static int Next(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
    public interface IMatrix
    {
        void InputFromKeyboard();
        void InputRandom();
        int MinElement();
        void Show();
    }
    
    public abstract class BaseMatrix : IMatrix
    {
        public BaseMatrix()
        {
            Console.WriteLine("-> BaseMatrix constructor called");
        }

        ~BaseMatrix()
        {
            Console.WriteLine("-> BaseMatrix destructor called");
        }
        
        public abstract void InputFromKeyboard();
        public abstract void InputRandom();
        public abstract int MinElement();
        public abstract void Show();
        
        public void PrintInfo()
        {
            Console.WriteLine("This is a matrix object inheriting from BaseMatrix.");
        }
    }
    
    class Matrix2D : BaseMatrix
    {
        private const int Rows = 3;
        private const int Cols = 3;
        private readonly int[,] _matrix = new int[Rows, Cols];
        
        public Matrix2D()
        {
            Console.WriteLine("--> Matrix2D constructor called");
        }
        
        ~Matrix2D()
        {
            Console.WriteLine("--> Matrix2D destructor called");
        }

        public override void InputFromKeyboard()
        {
            Console.WriteLine("Enter elements of 2D matrix (3x3):");
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    int value;
                    while (true)
                    {
                        Console.Write($"matrix[{i},{j}] = ");
                        if (int.TryParse(Console.ReadLine(), out value))
                        {
                            _matrix[i, j] = value;
                            break;
                        }
                        Console.WriteLine("Invalid input! Enter integer and try again.");
                    }
                }
            }
        }

        public override void InputRandom()
        {
            for (int i = 0; i < Rows; i++)
            for (int j = 0; j < Cols; j++)
                _matrix[i, j] = Randomizer.Next(0, 100);
            Console.WriteLine("2D Matrix filled randomly.");
        }

        public override int MinElement()
        {
            int min = _matrix[0, 0];
            for (int i = 0; i < Rows; i++)
            for (int j = 0; j < Cols; j++)
                if (_matrix[i, j] < min)
                    min = _matrix[i, j];
            return min;
        }

        public override void Show()
        {
            Console.WriteLine("2D matrix:");
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                    Console.Write(_matrix[i, j] + "\t");
                Console.WriteLine();
            }
        }
    }
    
    class Matrix3D : BaseMatrix
    {
        private readonly int[,,] _matrix3D = new int[3, 3, 3];

        public Matrix3D()
        {
            Console.WriteLine("--> Matrix3D constructor called");
        }
        
        ~Matrix3D()
        {
            Console.WriteLine("--> Matrix3D destructor called");
        }

        public override void InputFromKeyboard()
        {
            Console.WriteLine("Enter elements of 3D matrix (3x3x3):");
            for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
            for (int k = 0; k < 3; k++)
            {
                int value;
                while (true)
                {
                    Console.Write($"matrix[{i},{j},{k}] = ");
                    if (int.TryParse(Console.ReadLine(), out value))
                    {
                        _matrix3D[i, j, k] = value;
                        break;
                    }
                    Console.WriteLine("Invalid input! Enter integer and try again.");
                }
            }
        }

        public override void InputRandom()
        {
            for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
            for (int k = 0; k < 3; k++)
                _matrix3D[i, j, k] = Randomizer.Next(0, 100);
            Console.WriteLine("3D Matrix filled randomly.");
        }

        public override int MinElement()
        {
            int min = _matrix3D[0, 0, 0];
            for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
            for (int k = 0; k < 3; k++)
                if (_matrix3D[i, j, k] < min)
                    min = _matrix3D[i, j, k];
            return min;
        }

        public override void Show()
        {
            Console.WriteLine("3D matrix:");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"Depth {i + 1}:");
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                        Console.Write(_matrix3D[i, j, k] + "\t");
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
    }
    
    class Program
    {
        static void ShowMinFromBase(BaseMatrix matrix)
        {
            Console.WriteLine("Call via BaseMatrix: Minimum element = " + matrix.MinElement());
            matrix.PrintInfo();
        }
        
        static void ProcessMatrixViaInterface(IMatrix matrix)
        {
            Console.WriteLine("\n--- Processing matrix via IMatrix interface ---");
            matrix.InputRandom();
            matrix.Show();
            Console.WriteLine("Call via IMatrix: Minimum element = " + matrix.MinElement());
            
            Console.WriteLine("-----------------------------------------------");
        }


        static void Main(string[] args)
        {
            Console.WriteLine("--- 1. Polymorphism demonstration with Abstract Class ---");
            BaseMatrix baseMat2D = new Matrix2D();
            BaseMatrix baseMat3D = new Matrix3D();
            
            Console.WriteLine("\n");
            baseMat2D.InputRandom();
            baseMat3D.InputRandom();

            baseMat2D.Show();
            baseMat3D.Show();
            
            Console.WriteLine("\nCalling ShowMinFromBase method:");
            ShowMinFromBase(baseMat2D);
            ShowMinFromBase(baseMat3D);
            
            
            Console.WriteLine("\n\n--- 2. Polymorphism demonstration with Interface ---");
            IMatrix iMat2D = new Matrix2D();
            IMatrix iMat3D = new Matrix3D();
            
            ProcessMatrixViaInterface(iMat2D);
            ProcessMatrixViaInterface(iMat3D);
        }
    }
}