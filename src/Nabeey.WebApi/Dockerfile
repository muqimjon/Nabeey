# For single project

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

# Copy the project file
COPY ./src/Nabeey.WebApi/Nabeey.WebApi.csproj ./src/Nabeey.WebApi/

# Restore dependencies using the project file
RUN dotnet restore ./src/Nabeey.WebApi/Nabeey.WebApi.csproj

# Copy the rest of the application
COPY . .

# Publish the application
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /App

# Copy the published output from build-env
COPY --from=build-env /App/out .

EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "Nabeey.WebApi.dll"]


