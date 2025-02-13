# Usa a imagem oficial do .NET 8 como base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

# Usa a imagem do SDK para construir a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo de solução e restaura dependências
COPY ["Empresa.Funcionarios.Api/Empresa.Funcionarios.Api.csproj", "Empresa.Funcionarios.Api/"]
COPY ["Empresa.Funcionarios.Application/Empresa.Funcionarios.Application.csproj", "Empresa.Funcionarios.Application/"]
COPY ["Empresa.Funcionarios.Domain/Empresa.Funcionarios.Domain.csproj", "Empresa.Funcionarios.Domain/"]
COPY ["Empresa.Funcionarios.Data/Empresa.Funcionarios.Data.csproj", "Empresa.Funcionarios.Data/"]
COPY ["Empresa.Funcionarios.Infrastructure/Empresa.Funcionarios.Infrastructure.csproj", "Empresa.Funcionarios.Infrastructure/"]
RUN dotnet restore "Empresa.Funcionarios.Api/Empresa.Funcionarios.Api.csproj"

# Copia todo o código para dentro do container e compila
COPY . .
WORKDIR "/src/Empresa.Funcionarios.Api"
RUN dotnet build -c Release -o /app/build

# Publica a aplicação
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Usa a imagem base para rodar a aplicação
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Empresa.Funcionarios.Api.dll"]
