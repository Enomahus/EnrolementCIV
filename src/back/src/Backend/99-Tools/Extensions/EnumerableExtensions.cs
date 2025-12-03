namespace Tools.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(
            this IEnumerable<IEnumerable<T>> sequences
        )
        {
            IEnumerable<IEnumerable<T>> emptyProduct =
            [
                [],
            ];
            return sequences.Aggregate(
                emptyProduct,
                (accumulator, sequence) =>
                    from accseq in accumulator
                    from item in sequence
                    select accseq.Concat([item])
            );
        }
    }
}
