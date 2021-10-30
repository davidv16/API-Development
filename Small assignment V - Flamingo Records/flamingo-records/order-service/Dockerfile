FROM node:latest

LABEL maintainer="Davíð V. Arnalds"

ENV NODE_ENV=production
ENV PORT=80

COPY . /app
WORKDIR /app

RUN npm install

ENTRYPOINT [ "node", "index.js" ]