using System.Linq.Expressions;
using System.Reflection;

namespace LinkDevTask.Application.Helpers;
internal static class Mapper
{
    public static TSource MapTo<TSource>(object Destination, params string[] ignores)
    {
        var listOfDestinationProps = Destination.GetType().GetProperties();
        var listOfSourceProps = typeof(TSource).GetProperties();

        var InstanceOfSource = Activator.CreateInstance<TSource>();
        foreach (var prop in listOfSourceProps)
        {
            var PropVMValue = listOfDestinationProps.Where(p => !ignores.Contains(p.Name))
                .FirstOrDefault(p => p.Name.Contains(prop.Name) && p.CanRead)
                ?.GetValue(Destination);

            if (PropVMValue is not null && prop.CanWrite)
                prop.SetValue(InstanceOfSource, PropVMValue);
        }
        return InstanceOfSource;
    }

    public static void MapTo(object Source, object Destination)
    {
        var listOfDestinationProps = Destination.GetType().GetProperties();
        var listOfSourceProps = Source.GetType().GetProperties();

        foreach (var prop in listOfSourceProps)
        {
            var PropVMValue = listOfDestinationProps
                .FirstOrDefault(p => p.Name.Contains(prop.Name) && p.CanRead)
                ?.GetValue(Destination);

            if (PropVMValue is not null && prop.CanWrite)
                prop.SetValue(Source, PropVMValue);
        }
    }
}