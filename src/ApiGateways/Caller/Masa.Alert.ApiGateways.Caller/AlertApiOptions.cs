// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.ApiGateways.Caller;

public class AlertApiOptions
{
    public string ServiceBaseAddress { get; set; }

    public AlertApiOptions(string serviceBaseAddress)
    {
        ServiceBaseAddress = serviceBaseAddress;
    }
}
