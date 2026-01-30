using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class UserNameMatchBAL
    {
        // Calculate Levenshtein distance
        public static int LevenshteinDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            for (int i = 0; i <= n; i++) d[i, 0] = i;
            for (int j = 0; j <= m; j++) d[0, j] = j;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (s[i - 1] == t[j - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost
                    );
                }
            }

            return d[n, m];
        }

        // Calculate similarity percentage
        public static double NameSimilarity(string name1, string name2)
        {
            int maxLen = Math.Max(name1.Length, name2.Length);
            if (maxLen == 0) return 100.0; // both empty
            int distance = LevenshteinDistance(name1.ToLower(), name2.ToLower());
            return (1.0 - (double)distance / maxLen) * 100;
        }
    }
}
