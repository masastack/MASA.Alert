FROM registry.cn-hangzhou.aliyuncs.com/masa/dotnet_sdk:6.0.403 AS publish
WORKDIR /src
COPY . .
ENV CSPROJ="src/Web/Masa.Alert.Web.Admin.Server/Masa.Alert.Web.Admin.Server.csproj"
RUN dotnet restore $CSPROJ && dotnet publish $CSPROJ -c Release -o /app/publish


FROM registry.cn-hangzhou.aliyuncs.com/masa/dotnet_aspnet:6.0.4
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["src/Web/Masa.Alert.Web.Admin/wwwroot/i18n","wwwroot/i18n"]
ENV ASPNETCORE_URLS=https://0.0.0.0:443
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "Masa.Alert.Web.Admin.Server.dll"]