FROM mcr.microsoft.com/dotnet/sdk:5.0

LABEL maintainer="Einar Sigurðsson (einarsig16@ru.is)"

ENV ASPNETCORE_URLS=http://+:5000;https://+:5001
ENV RMQ_HOST=message-broker
ENV RMQ_USER=user
ENV RMQ_PASS=pass
ENV PATH $PATH:/root/.dotnet/tools

COPY . /app
WORKDIR /app/JustTradeIt.Software.API
RUN dotnet dev-certs https --trust
#RUN dotnet tool install -g dotnet-ef
#RUN dotnet ef database update
ENTRYPOINT [ "dotnet", "run" ]
