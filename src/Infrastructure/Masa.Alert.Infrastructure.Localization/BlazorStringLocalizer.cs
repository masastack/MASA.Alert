// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Infrastructure.Localization;

public class BlazorStringLocalizer : IStringLocalizer
{
    private readonly I18n _i18n;

    public BlazorStringLocalizer(I18n i18n)
    {
        _i18n = i18n;
    }

    public LocalizedString this[string name] => Get(name);

    public LocalizedString this[string name, params object[] arguments] => Get(name, arguments);

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        throw new NotImplementedException();
    }

    private LocalizedString Get(string name, params object[] arguments)
    {
        var value = _i18n.T(name, false) ?? name;

        if (arguments.Any())
        {
            return new LocalizedString(name, string.Format(value, arguments));
        }

        return new LocalizedString(name, value, true);
    }
}
