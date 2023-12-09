using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.API
{
    internal static class MathHelper
    {
        /// <summary>
        /// Find lowest Commom denominator of a two of numbers 
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static long LCD(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        /// <summary>
        /// Find Least Commom Multiple of a two of numbers 
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static long LCM(long a, long b)
        {
            return (a * b) / LCD(a, b);
        }

        /// <summary>
        /// Find Least Commom Multiple of a list of numbers 
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static long LCM(long[] numbers)
        {
            long ppcm = numbers[0];
            for (int i = 1; i < numbers.Length; i++)
            {
                ppcm = LCM(ppcm, numbers[i]);
            }
            return ppcm;
        }

        /// <summary>
        /// Solve quadratic equation
        /// ax^2 + bx + c = 0
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static double[] QuadraticEquation(double a, double b, double c)
        {
            double delta = (b*b) - (4 * a * c);
            if(delta< 0)
            {
                return new double[0];
            }
            if(delta == 0)
            {
                return new double[1] {-b/(2*a)};
            }

            return new double[2]
            {
                (-b+Math.Sqrt(delta))/(2*a),
                (-b-Math.Sqrt(delta))/(2*a)
            };

        }
    }
}
