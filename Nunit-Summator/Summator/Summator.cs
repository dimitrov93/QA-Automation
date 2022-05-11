using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Summator
{
    public class Summator
    {
        public int Sum(int[] arr)
        {
            int sum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                sum += arr[i];
            }
            return sum;
        }


        public  int Avarage(int[] arr)
        {
            int avg = 0;
            for(int i = 0; i < arr.Length; i++)
            {
                avg += arr[i];
            }
            avg /= arr.Length;
            return avg;
        }


        public  double Avarage2(double[] arr)
        {
            double avg = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                avg += arr[i];
            }
            avg /= arr.Length;
            return avg;
        }
    }
}
