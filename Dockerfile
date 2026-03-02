# ===== Build stage =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore "./Blazor Serial Test.csproj"
RUN dotnet publish "./Blazor Serial Test.csproj" -c Release -o /app/publish --no-restore

# (디버그용) publish 결과 확인
RUN ls -al /app/publish

# ===== Runtime stage =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish/ ./

ENV ASPNETCORE_URLS=http://0.0.0.0:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "./Blazor Serial Test.dll"]
