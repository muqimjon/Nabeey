## For single project
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
#WORKDIR /App
#COPY . ./
#RUN dotnet restore
#RUN dotnet publish -c Release -o out
#
#FROM mcr.microsoft.com/dotnet/aspnet:6.0
#WORKDIR /App
#COPY --from=build-env /App/out .
#EXPOSE 5000
#ENV ASPNETCORE_URLS=http://+:5000
#ENTRYPOINT ["dotnet", "Nabeey.WebApi.dll"]


# For multi project
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /App
COPY ./src/Nabeey.WebApi/ ./app/
RUN dotnet restore ./app/
COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /App
COPY --from=build-env /App/out .
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "Nabeey.WebApi.dll"]


