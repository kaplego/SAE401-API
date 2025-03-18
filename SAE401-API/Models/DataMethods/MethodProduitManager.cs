namespace SAE401_API.Models.DataMethods
{
    public static class MethodProduitManager
    {
        public static int LevenshteinDistance(string source, string target)
        {
            int[,] dp = new int[source.Length + 1, target.Length + 1];

            for (int i = 0; i <= source.Length; i++)
            {
                for (int j = 0; j <= target.Length; j++)
                {
                    if (i == 0)
                        dp[i, j] = j;
                    else if (j == 0)
                        dp[i, j] = i;
                    else
                    {
                        dp[i, j] = Math.Min(
                            Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1),
                            dp[i - 1, j - 1] + (source[i - 1] == target[j - 1] ? 0 : 1)
                        );
                    }
                }
            }
            return dp[source.Length, target.Length];
        }




    }
}
