FROM node:18-alpine as build
RUN mkdir -p /app
WORKDIR /app
COPY package.json /app
RUN npm install
COPY . /app
RUN npm run build --prod --output-path=./dist/app-front

FROM nginx:1.25.0-alpine
COPY --from=build /app/dist/app-front /usr/share/nginx/html
