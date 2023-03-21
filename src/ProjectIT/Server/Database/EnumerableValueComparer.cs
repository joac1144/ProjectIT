using Microsoft.EntityFrameworkCore.ChangeTracking;

public class EnumerableValueComparer<T> : ValueComparer<IEnumerable<T>>
{
    public EnumerableValueComparer() : base(
        (c1, c2) => c1.SequenceEqual(c2),
        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
        c => (IEnumerable<T>)c.ToList())
    {
    }
}