namespace Spectre.Helper;

public static class TOC
{
    public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
    {
        return new ObservableCollection<T>(source);
    }

    public static bool ReturnSuccess<TI>(this TI input) where TI : class
    {
        return input != null;
    }
}