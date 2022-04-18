FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["FormsCreator.App/FormsCreator.App.csproj", "FormsCreator.App/"]
RUN dotnet restore "FormsCreator.App/FormsCreator.App.csproj"
COPY . .
WORKDIR "/src/FormsCreator.App"
RUN dotnet build "FormsCreator.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FormsCreator.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FormsCreator.App.dll"]
