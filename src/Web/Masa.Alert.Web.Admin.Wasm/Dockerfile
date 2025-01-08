FROM mcr.microsoft.com/dotnet/sdk:6.0 AS publish
WORKDIR /src
COPY . .
ENV CSPROJ="src/Web/Masa.Alert.Web.Admin.Wasm/Masa.Alert.Web.Admin.Wasm.csproj"
RUN dotnet nuget add source http://devops.lonsid.cn:8080/nuget
RUN dotnet restore $CSPROJ && dotnet publish $CSPROJ -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=publish /app/publish .
# ENV ASPNETCORE_URLS=https://0.0.0.0:443
# EXPOSE 80
# EXPOSE 443
# ENTRYPOINT ["dotnet", "Masa.Auth.Web.Admin.Wasm.dll"]

FROM nginx:1.21.1
WORKDIR /usr/share/nginx/html
RUN ln -sf /usr/share/zoneinfo/Asia/Shanghai /etc/localtime
COPY --from=front-base /app /usr/share/nginx/html
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["nginx","-g","daemon off;"]