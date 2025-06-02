using System.ComponentModel;

namespace Funcfy.Extensions;

/// <summary>
/// Provides extension methods for working with enums, specifically to retrieve the category of an enum value.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Gets the category of the specified enum value.
    /// </summary>
    /// <param name="value">The enum value for which to retrieve the category.</param>
    /// <returns>
    /// The category of the enum value, as specified by the <see cref="CategoryAttribute"/> applied to the enum field.
    /// </returns>
    public static string GetCategory(this Enum value)
    {
        ArgumentNullException.ThrowIfNull(value);

        var type = value.GetType();

        var name = Enum.GetName(type, value);
        if (name is null)
            return string.Empty;

        var fieldInfo = type.GetField(name)!;

        var attribute = fieldInfo
            .GetCustomAttributes(false)
            .OfType<CategoryAttribute>()
            .SingleOrDefault();

        return attribute?.Category ?? string.Empty;
    }
}
