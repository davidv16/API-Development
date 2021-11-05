FROM python:latest

LABEL maintainer="Davíð V. Arnalds"

ENV PYTHON_ENV=production
ENV RMQ_HOST=message-broker
ENV RMQ_USER=user
ENV RMQ_PASS=pass

COPY . /app
WORKDIR /app
RUN pip install -r requirements.txt
ENTRYPOINT [ "python", "service.py" ]
