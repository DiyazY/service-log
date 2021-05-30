FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY . ./
RUN dotnet restore "sl.web" --disable-parallel && \
    dotnet publish "sl.web" -c Release -o publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim 
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "sl.web.dll"]