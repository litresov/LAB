using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ConsoleApp2
{
    class DLL1
    {
        private const string DLL = "Lib.dll";
        [DllImport(DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern double TheFunc(double x);
        [DllImport(DLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FuncName();
    }
    class DLL2
    {
        private const string DLL = "Lib2-2-1.dll";
        [DllImport(DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern double TheFunc(double x);
        [DllImport(DLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FuncName();
    }
    class DLL3
    {
        private const string DLL = "Lib2-2-2.dll";
        [DllImport(DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern double TheFunc(double x);
        [DllImport(DLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FuncName();
    }
    class DLL4
    {
        private const string DLL = "Lib2-2-3-1.dll";
        [DllImport(DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern double TheFunc(double x);
        [DllImport(DLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FuncName();
    }
    class DLL5
    {
        private const string DLL = "Lib2-2-3-2.dll";
        [DllImport(DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern double TheFunc(double x);
        [DllImport(DLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FuncName();
    }
    class DLL6
    {
        private const string DLL = "Lib2-2-3.dll";
        [DllImport(DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern double TheFunc(double x);
        [DllImport(DLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FuncName();
    }
    class DLL7
    {
        private const string DLL = "mydll1.dll";
        [DllImport(DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern double TheFunc(double x);
        [DllImport(DLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FuncName();
    }
    class DLL8
    {
        private const string DLL = "mydll2.dll";
        [DllImport(DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern double TheFunc(double x);
        [DllImport(DLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FuncName();
    }
    class DLL9
    {
        private const string DLL = "mydll3.dll";
        [DllImport(DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern double TheFunc(double x);
        [DllImport(DLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FuncName();
    }
    class dll
    {
        public static List<Func<IntPtr>> ListFuncName = new List<Func<IntPtr>>
            {
                DLL1.FuncName,
                DLL2.FuncName,
                DLL3.FuncName,
                DLL4.FuncName,
                DLL5.FuncName,
                DLL6.FuncName,
                DLL7.FuncName,
                DLL8.FuncName,
                DLL9.FuncName
            };
        public static List<Func<double, double>> ListTheFunc = new List<Func<double, double>>
            {
                DLL1.TheFunc,
                DLL2.TheFunc,
                DLL3.TheFunc,
                DLL4.TheFunc,
                DLL5.TheFunc,
                DLL6.TheFunc,
                DLL7.TheFunc,
                DLL8.TheFunc,
                DLL9.TheFunc
            };
    }
    class Program
    {
        static string Ts(IntPtr x)
        {
            return (Marshal.PtrToStringAnsi(x));
        }
        static string Ts1(IntPtr x)
        {
            return (Marshal.PtrToStringUni(x));
        }
        static string Conv(string x)
        {
            Encoding ascii = Encoding.ASCII;
            Encoding unicode = Encoding.Unicode;
            byte[] unicodeBytes = unicode.GetBytes(x);
            byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);
            char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
            ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
            string asciiString = new string(asciiChars);
            return asciiString;
        }
        static void Sch(int x)
        {
            var fname = dll.ListFuncName.ElementAt(x-1);
            var tfunc = dll.ListTheFunc.ElementAt(x-1);
            StreamWriter w = new StreamWriter("E:\\LAB2.2\\Lab2.2\\ConsoleApp2\\bin\\Debug\\dotpos.txt", false, System.Text.Encoding.UTF8);
            double X = 0;
            int N = 1;
            while (N <= 100)
            {
                if (X == 0)
                {
                    w.WriteLine("f");
                    if (x < 7) { w.WriteLine(Ts(fname())); }
                    else { w.WriteLine(Ts1(fname())); }
                }
                w.WriteLine(tfunc(X));
                X += 0.1;
                N++;
            }
            w.Close();
        }
        static void error( int x)
        {
            string er1 = ": Ошибка. Функции не найдены.";
            string er2 = ": Ошибка. Не удалось загрузить библиотеку.";
            var fname = dll.ListFuncName.ElementAt(x-1);
            try
            {
                dll.ListFuncName.ElementAt(x-1);
                dll.ListTheFunc.ElementAt(x-1)(1.0);
                if (x < 7) { Console.WriteLine(x + ": " + Ts(fname())); }
                else { Console.WriteLine(x + ": " + Ts1(fname())); }
            }
            catch (System.EntryPointNotFoundException) { Console.WriteLine(x + er1); }
            catch (System.BadImageFormatException) { Console.WriteLine(x + er2); }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Lab2.2\n" +
                   "Демещик Артем\n\n" +
                   "На выбор имеются следующие функции: \n");
            int n = 1;
            int ddlmax = 9;
            while (n <= ddlmax)
            {
                error(n++);
            }
            bool Flag = true;
            while (Flag)
            {
                Console.WriteLine("\nКакую функцию посчитать? ");
                Sch(Convert.ToInt32(Console.ReadLine()));
                Process.Start("draw.pyw");
            }
        }
    }
}
