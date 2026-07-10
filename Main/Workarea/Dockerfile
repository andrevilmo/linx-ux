#Dockerfile
FROM nginx

LABEL vendor="Linx Sistemas S.A."

ENV HOME=/usr/share/nginx/html
WORKDIR $HOME
COPY ./dist/www $HOME

RUN touch /etc/nginx/conf.d/gzip.conf
RUN echo "gzip on;" >> /etc/nginx/conf.d/gzip.conf
RUN echo "gzip_disable "msie6";" >> /etc/nginx/conf.d/gzip.conf
RUN echo "" >> /etc/nginx/conf.d/gzip.conf
RUN echo "gzip_vary on;" >> /etc/nginx/conf.d/gzip.conf
RUN echo "gzip_proxied any;" >> /etc/nginx/conf.d/gzip.conf
RUN echo "gzip_comp_level 9;" >> /etc/nginx/conf.d/gzip.conf
RUN echo "gzip_buffers 16 8k;" >> /etc/nginx/conf.d/gzip.conf
RUN echo "gzip_http_version 1.1;" >> /etc/nginx/conf.d/gzip.conf
RUN echo "gzip_types text/html text/plain text/css application/javascript application/json application/x-javascript text/xml application/xml application/xml+rss text/javascript;" >> /etc/nginx/conf.d/gzip.conf

EXPOSE 80


################
### cmds
################

### cria a imagem local
# docker build -t workarea_site .

### roda a imagem
#$ docker run -p 8000:80 -d --name my_workarea_site workarea_site


################
### extras
################

### stop
#$ docker stop my_workarea_site

### remove container
#$ docker rm my_workarea_site -f

### publica no hub
#$ docker push linxinov/workarea_site

### baixa do hub
#$ docker pull linxinov/workarea_site