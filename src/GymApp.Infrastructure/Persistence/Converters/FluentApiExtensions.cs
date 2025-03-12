using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymApp.Infrastructure.Persistence.Converters;

public static class FluentApiExtensions
{
    public static PropertyBuilder<T> HasListOfIdsConverter<T>(this PropertyBuilder<T> propertyBuilder)
    {
        return propertyBuilder.HasConversion(
            new ListOfIdsConverter(), 
            new ListOfIdsComparer());
    }

    public static PropertyBuilder<T> HasValueJsonConverter<T>(this PropertyBuilder<T> propertyBuilder)
    {
        return propertyBuilder.HasConversion(
            new JsonValueConverter<T>(),
            new JsonValueComparer<T>());
    }
}