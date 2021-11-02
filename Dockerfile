FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 44342

ENV ASPNETCORE_URLS=http://+:44342

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Resturent/Restaurant.csproj", "Resturent/"]
RUN dotnet restore "Resturent\Restaurant.csproj"
COPY . .
WORKDIR "/src/Resturent"
RUN dotnet build "Restaurant.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Restaurant.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Restaurant.dll"]
