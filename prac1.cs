using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace prac1
{
    public interface IAdvancedEnumerable<T> : IEnumerable<T>
    {
        void Combination(int k, List<int> line);
        void Subset();
        void Permutation(int k, int m);
    }

    class Advance_Comparator<T> : IEqualityComparer<T>
    {
        public bool Equals([AllowNull] T x, [AllowNull] T y)
        {
            if (x.Equals(y)) return true;
            else return false;
        }

        public int GetHashCode([DisallowNull] T obj)
        {
            return obj.GetHashCode();
        }
    }

    public sealed class IAdvanced : IAdvancedEnumerable<int>, ICloneable
    {

        private readonly int[] _innerArray;

        public IAdvanced(int[] innerArray)
        {
            _innerArray = (int[])innerArray.Clone();
        }

        public object Clone()
        {
            return new IAdvanced(_innerArray);
        }
        public void Combination(int k, List<int> line)
        {
            try
            {
                if (!CheckEq<int>(_innerArray)) throw new ArgumentException("In array detected equals elements");
                for (int i = 0; i < _innerArray.Length; i++)
                {

                    line.Add(_innerArray[i]);

                    if (k <= 1)
                    { // Condition that prevents infinite loop in recursion
                        for (var j = 0; j < line.Count; j++)
                            Console.Write($"{line[j]} "); // Simplified print to keep code shorter
                        Console.WriteLine("");
                        line.RemoveAt(line.Count - 1);

                    }

                    else
                    {
                        Combination(k - 1, line); // Recursion happens here
                        line.RemoveAt(line.Count - 1);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        static bool CheckEq<T>(IEnumerable<T> mas)
        {
            Advance_Comparator<T> comp = new Advance_Comparator<T>();
            for (int i = 0; i < mas.Count() - 1; i++)
                for (int j = i + 1; j < mas.Count(); j++)
                    if (comp.Equals(mas.ElementAt(i), mas.ElementAt(j))) return false;

            return true;
        }

        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 0; i < _innerArray.Length; i++)
            {
                yield return _innerArray[i];
            }
        }

        public void Permutation(int k, int m)
        {
            try
            {
                if (!CheckEq<int>(_innerArray)) throw new ArgumentException("In array detected equals elements");
                if (k == m)
                {
                    for (int o = 0; o <= m; o++)
                    {
                        Console.Write($"{_innerArray[o]}  ");
                    }
                    Console.WriteLine("");
                }
                else
                {
                    for (int i = k; i <= m; ++i)
                    {
                        Swap(ref _innerArray[k], ref _innerArray[i]);
                        Permutation(k + 1, m);
                        Swap(ref _innerArray[k], ref _innerArray[i]);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        private static void Swap(ref int a, ref int b)
        {
            if (a != b)
            {
                a ^= b;
                b ^= a;
                a ^= b;
            }
        }
        public void Subset()
        {
            try
            {
                if (!CheckEq<int>(_innerArray)) throw new ArgumentException("In array detected equals elements");
                int k = (int)Math.Pow(2, _innerArray.Length);
                for (int i = 0; i < k; i++)
                {
                    for (int j = 0; j < _innerArray.Length; j++)
                        if ((i & (1 << j)) > 0)
                            Console.Write($"{_innerArray[j]}  ");
                    Console.WriteLine("");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _innerArray.GetEnumerator(); ;
        }
    }

    class Program
        {
            static void Main(string[] args)
            {
            
            List<int> line = new List<int>();
            var Comb = new IAdvanced(new int[4] { 1, 4, 3, 4 }); //I LOVE HARDCODE
            Comb.Combination(3, line);
            var Sub = new IAdvanced(new int[3] { 1, 2, 3 });
            Sub.Subset();
            var Per = new IAdvanced(new int[4] { 1, 2, 3, 4 });
            Per.Permutation(0, 3);

            }
        }
}
