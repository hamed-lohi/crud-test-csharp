# .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS dotnet

# Copy Projects
COPY Application/Application.csproj Application/
COPY Infrastructure/Infrastructure.csproj Infrastructure/
COPY Domain/Domain.csproj Domain/
COPY Mc2.CrudTest.Presentation/Front/Mc2.CrudTest.Presentation.Front.csproj Mc2.CrudTest.Presentation/Front/
COPY Mc2.CrudTest.Presentation/Shared/Mc2.CrudTest.Shared.csproj Mc2.CrudTest.Presentation/Shared/
COPY Mc2.CrudTest.Presentation/Server/Mc2.CrudTest.Presentation.Server.csproj Mc2.CrudTest.Presentation/Server/

# .NET Restore
RUN dotnet restore Mc2.CrudTest.Presentation/Server/Mc2.CrudTest.Presentation.Server.csproj

# Copy All Files
COPY . .

# .NET Publish
RUN dotnet publish Mc2.CrudTest.Presentation/Server/Mc2.CrudTest.Presentation.Server.csproj -c Release -o /dist --no-restore

# .NET Runtime
FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
WORKDIR /app
COPY --from=dotnet /dist .
ENTRYPOINT ["dotnet", "Mc2.CrudTest.Presentation.Server.dll"]
