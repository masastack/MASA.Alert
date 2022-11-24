// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.Infrastructure.EntityFrameworkCore.EntityFrameworkCore.ValueConverters;

public class EnumerationConverter : JsonConverter<Enumeration>
{
    public override Enumeration Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return Enumeration.FromValue<Enumeration>(reader.GetInt32());
    }

    public override void Write(Utf8JsonWriter writer, Enumeration value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.Id);
    }
}