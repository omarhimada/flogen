using System.Collections.Generic;
using System.Linq;

namespace FloGen.Functions
{
    public static class CartesianExtension
    {
        public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this IEnumerable<IEnumerable<T>> numOfSequences)
        {
            IEnumerable<IEnumerable<T>> emptyCartesianProduct = new[] { Enumerable.Empty<T>() };

            return numOfSequences.Aggregate(emptyCartesianProduct,
              (accumulator, sequence) =>
                from accSeq in accumulator
                from element in sequence
                select accSeq.Concat(new[] { element }));
        }
    }
}
