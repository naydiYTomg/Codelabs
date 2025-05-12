using System.Globalization;
using System.Text;

namespace Codelabs.UI.Components.InternalTypes;

public static class Extensions
{
    public static string Repr<T>(this IEnumerable<T> self, CultureInfo? ci = null, string sep = ",")
    {
        var _builder = new StringBuilder("[");
        var list = self.ToList();
        foreach (var (idx, val) in list.Enumerate())
        {
            _builder.Append($"{Convert.ToString(val, ci ?? CultureInfo.CurrentCulture)}{(idx != list.Count - 1 ? sep : "")}");
        }

        _builder.Append(']');
        return _builder.ToString();
    }

    public static IEnumerable<(int index, T item)> Enumerate<T>(this IEnumerable<T> source)
    {
        var i = 0;
        foreach (var item in source)
        {
            yield return (i++, item);
        }
    }

    public static IEnumerable<T> ExtendTo<T>(this IEnumerable<T> self, IEnumerable<object> other, T? fillElem = default)
    {
        var (selfList, otherList) = (self.ToList(), other.ToList());
        var align = otherList.Count - selfList.Count;
        for (var _ = 0; _ < align; _++)
        {
            selfList.Add(fillElem);
        }

        return selfList;
    }
    
}
