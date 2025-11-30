using System;

namespace FloGen.Functions
{
    public static class FisherYatesShuffle
    {
        public static int[] RandomIndices(Random random, int numberOfCartItems)
        {
            // Make a 'deck' the same size as the total number of cart items
            int count = numberOfCartItems;

            // Instantiate an array of integers representing the indices of the cart items collection
            int[] indices = new int[count];
            for (int i = 0; i < count; i++)
            {
                indices[i] = i;
            }

            // Shuffle the array of integers 
            for (int i = 0; i <= count - 2; i++)
            {
                int j = random.Next(count - i);
                if (j > 0)
                {
                    int curVal = indices[i];
                    indices[i] = indices[i + j];
                    indices[i + j] = curVal;
                }
            }

            // Shuffle them again in descending order
            for (int i = count - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);
                if (j != i)
                {
                    int curVal = indices[i];
                    indices[i] = indices[j];
                    indices[j] = curVal;
                }
            }

            return indices;
        }
    }
}
