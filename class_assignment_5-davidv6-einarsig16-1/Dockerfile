
# Set base image
FROM node:latest

LABEL AUTHOR "Davíð Valdimar Arnalds davidv16@ru.is"
LABEL AUTHOR "Einar Sigurðsson einarsig16@ru.is"

# Set environmental variables
ENV DONUT_DBHOST "cluster0.fbapu.mongodb.net"
ENV DONUT_DBNAME "donuts_web"
ENV DONUT_DBUSER "donuts_web"
ENV DONUT_DBPWD "refrain-moonwalk-easily-deniable"

# COPY app to image
ADD . /app

WORKDIR /app

RUN npm install

CMD ["npm", "start"]