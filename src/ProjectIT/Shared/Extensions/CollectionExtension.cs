namespace ProjectIT.Shared.Extensions;

public static class CollectionExtension
{
    public static bool IsNullOrEmpty<T>(this List<T> list)
    {
        return list == null || list.Count == 0;
    }
}