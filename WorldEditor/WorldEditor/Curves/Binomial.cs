using System.Collections.Generic;

namespace WorldEditor.Curves
{  
    static class Binomial
    {
        private static List<int[]> coeficients = new List<int[]> { new int[] { }, new int[] { }, new int[] { }, new int[] { }, new int[3] { 1, 4, 6 }, new int[3] { 1, 5, 10 } };

        private static int addCoef(int n, int k, int coef)
        {
            if (getCoef(n, k) != 0)
                return getCoef(n, k);

            if (coeficients.Count <= n)
                coeficients.Add(new int[(n - n % 2) / 2 + 1]);

            if ((n - n % 2) / 2 >= k)
                coeficients[n][k] = coef;
            else
                coeficients[n][n - k] = coef;
            return coef;
        }

        private static int getCoef(int n, int k)
        {
            if (k == 0 || k == n)
                return 1;
            if (k == 1 || k == n - 1)
                return n;
            else if (coeficients.Count > n)
                if ((n - n % 2) / 2 >= k)
                    return coeficients[n][k];
                else
                    return coeficients[n][n - k];
            else
                return 0;
        }

        public static int BinomialCoefficient(int n, int k)
        {
            if (getCoef(n, k) != 0)
                return getCoef(n, k);
            else
                return addCoef(n - 1, k, BinomialCoefficient(n - 1, k)) + addCoef(n - 1, k - 1, BinomialCoefficient(n - 1, k - 1));
        }
    }
}
