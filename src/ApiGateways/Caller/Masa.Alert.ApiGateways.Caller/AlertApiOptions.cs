// Copyright (c) MASA Stack All rights reserved.
// Licensed under the Apache License. See LICENSE.txt in the project root for license information.

namespace Masa.Alert.ApiGateways.Caller
{
    public class AlertApiOptions
    {
        public string AppId { get; set; } = string.Empty;
        public string ServiceBaseAddress { get; set; } = string.Empty;
        public string AuthorityEndpoint { get; set; } = string.Empty;

        public string ClientId { get; set; } = string.Empty;

        public string ClientSecret { get; set; } = string.Empty;
    }
}
