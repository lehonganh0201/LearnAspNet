FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["LearnPageRazor.csproj", "./"]
RUN dotnet restore "LearnPageRazor.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "LearnPageRazor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LearnPageRazor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LearnPageRazor.dll"]
