FROM python:latest

LABEL maintainer="Davíð V. Arnalds"

ENV PYTHON_ENV=production

COPY . /app
WORKDIR /app

RUN pip install -r requirements.txt

ENTRYPOINT [ "python", "app.py" ]