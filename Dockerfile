FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["Lab10-NagelyArpi/Lab10-NagelyArpi.csproj", "Lab10-NagelyArpi/"]
COPY ["Lab10-NagelyArpi.Application/Lab10-NagelyArpi.Application.csproj", "Lab10-NagelyArpi.Application/"]
COPY ["Lab10-NagelyArpi.Domain/Lab10-NagelyArpi.Domain.csproj", "Lab10-NagelyArpi.Domain/"]
COPY ["Lab10-NagelyArpi.Infrastructure/Lab10-NagelyArpi.Infrastructure.csproj", "Lab10-NagelyArpi.Infrastructure/"]

RUN dotnet restore "Lab10-NagelyArpi/Lab10-NagelyArpi.csproj"

COPY . .
WORKDIR /src/Lab10-NagelyArpi
RUN dotnet publish "Lab10-NagelyArpi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000
ENTRYPOINT ["dotnet", "Lab10-NagelyArpi.dll"]
