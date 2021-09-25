FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/DevOpsChallenge.SalesApi/DevOpsChallenge.SalesApi.csproj", "src/DevOpsChallenge.SalesApi/"]
COPY ["src/DevOpsChallenge.SalesApi.Business/DevOpsChallenge.SalesApi.Business.csproj", "src/DevOpsChallenge.SalesApi.Business/"]
COPY ["src/DevOpsChallenge.SalesApi.Database/DevOpsChallenge.SalesApi.Database.csproj", "src/DevOpsChallenge.SalesApi.Database/"]

RUN dotnet restore "src/DevOpsChallenge.SalesApi/DevOpsChallenge.SalesApi.csproj"
#Build the .csproj
COPY . .
WORKDIR "/src/src/DevOpsChallenge.SalesApi"
RUN dotnet build "DevOpsChallenge.SalesApi.csproj" -c Release -o /app/build

# publish the API
FROM build AS publish
RUN dotnet publish "DevOpsChallenge.SalesApi.csproj" -c Release -o /app/publish


#run unit tests
COPY ["tests/DevOpsChallenge.SalesApi.Business.UnitTests/DevOpsChallenge.SalesApi.Business.UnitTests.csproj", "tests/DevOpsChallenge.SalesApi.Business.UnitTests/"]

RUN dotnet restore "tests/DevOpsChallenge.SalesApi.Business.UnitTests/DevOpsChallenge.SalesApi.Business.UnitTests.csproj"

COPY . .
WORKDIR "/src/tests/DevOpsChallenge.SalesApi.Business.UnitTests"
#RUN dotnet build "DevOpsChallenge.SalesApi.Business.UnitTests.csproj" -c Release -o /app/build

FROM build AS test  
ARG BuildId
LABEL test=${BuildId}
RUN dotnet test "DevOpsChallenge.SalesApi.Business.UnitTests.csproj" --results-directory /testresults --logger "trx;LogFileName=test_results.trx" 





FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevOpsChallenge.SalesApi.dll"]