using System;

namespace Summator // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a number");

            int result = Int32.Parse(Console.ReadLine());
         
                if (result == 31)
                {
                    Console.WriteLine("Test Pass");
                }
                else
                {
                    Console.WriteLine("Test Failed");
                }
            

        }
    }
}