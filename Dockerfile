

# Giai đoạn 1: Build
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["SE160548_IdetityAjaxASP.NETCoreWebAPI/SE160548_IdetityAjaxASP.NETCoreWebAPI.csproj", "SE160548_IdetityAjaxASP.NETCoreWebAPI/"]
RUN dotnet restore "SE160548_IdetityAjaxASP.NETCoreWebAPI/SE160548_IdetityAjaxASP.NETCoreWebAPI.csproj"
COPY . .
WORKDIR "/src/SE160548_IdetityAjaxASP.NETCoreWebAPI"
RUN dotnet build "SE160548_IdetityAjaxASP.NETCoreWebAPI.csproj" -c Release -o /app/build

# Giai đoạn 2: Publish
FROM build AS publish
RUN dotnet publish "SE160548_IdetityAjaxASP.NETCoreWebAPI.csproj" -c Release -o /app/publish

# Giai đoạn 3: Final
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 5000
ENTRYPOINT ["dotnet", "SE160548_IdetityAjaxASP.NETCoreWebAPI.dll"]
