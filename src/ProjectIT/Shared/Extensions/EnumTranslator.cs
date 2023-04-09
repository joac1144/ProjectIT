using Microsoft.Extensions.Localization;
using ProjectIT.Shared.Resources;

namespace ProjectIT.Shared.Extensions;

public static class EnumTranslator
{
    public static string GetTranslatedString(this Enum enumValue, IStringLocalizer<EnumsResource> enumsResource)
    {
        return enumsResource[$"{enumValue.GetType().Name}.{enumValue}"];
    }
}