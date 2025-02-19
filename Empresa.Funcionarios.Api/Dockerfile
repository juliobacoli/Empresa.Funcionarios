FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Empresa.Funcionarios.Api/Empresa.Funcionarios.Api.csproj", "Empresa.Funcionarios.Api/"]
COPY ["Empresa.Funcionarios.Application/Empresa.Funcionarios.Application.csproj", "Empresa.Funcionarios.Application/"]
COPY ["Empresa.Funcionarios.Domain/Empresa.Funcionarios.Domain.csproj", "Empresa.Funcionarios.Domain/"]
COPY ./Empresa.Funcionarios.Data/Empresa.Funcionarios.Data.csproj Empresa.Funcionarios.Data/

RUN dotnet restore "Empresa.Funcionarios.Api/Empresa.Funcionarios.Api.csproj"

COPY . .
WORKDIR "/src/Empresa.Funcionarios.Api"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Empresa.Funcionarios.Api.dll"]
