FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY Qatu.API/Qatu.API.csproj Qatu.API/
COPY Qatu.Application/Qatu.Application.csproj Qatu.Application/
COPY Qatu.Domain/Qatu.Domain.csproj Qatu.Domain/
COPY Qatu.Infrastructure/Qatu.Infrastructure.csproj Qatu.Infrastructure/

RUN dotnet restore Qatu.API/Qatu.API.csproj

COPY . . 
WORKDIR /src/Qatu.API
RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

COPY --from=build /out .

EXPOSE 7229

CMD ["dotnet", "Qatu.API.dll"]
