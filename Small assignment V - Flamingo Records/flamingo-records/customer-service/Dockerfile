FROM mcr.microsoft.com/dotnet/core/sdk:3.1

LABEL maintainer="Davíð V. Arnalds"

ENV ASPNETCORE_URLS=http://+:80

COPY . /app
WORKDIR /app/customer-service
RUN dotnet restore
ENTRYPOINT [ "dotnet", "run" ]