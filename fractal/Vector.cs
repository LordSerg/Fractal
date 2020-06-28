using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fractal
{
    class Vector
    {
        public double[] C;//некоторое количество "измерений" (или координат) вектора
        public Vector() { }
        public Vector(params double[] coord1)
        {
            C = new double[coord1.Length];
            for (int i = 0; i < coord1.Length; i++)
            {
                C[i] = coord1[i];
            }
        }
        public static Vector M(double skal, Vector v)
        {//умножение скаляра на вектор
            for (int i = 0; i < v.C.Length; i++)
                v.C[i] = v.C[i] * skal;
            return v;
        }
        public static Vector Add(Vector a, Vector b)
        {//суммирование векторов
            if (a.C.Length == b.C.Length)
            {
                Vector x = new Vector();// = new Vector(a.C[0] + b.C[0], a.C[1] + b.C[1],...);
                x.C = new double[a.C.Length];
                for (int i = 0; i < x.C.Length; i++)
                {
                    x.C[i] = a.C[i] + b.C[i];
                }
                return x;
            }
            else
                return null;
        }
    }
}
