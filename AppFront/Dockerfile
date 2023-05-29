FROM node:18-alpine as build
RUN mkdir -p /app
WORKDIR /app
COPY package.json /app
RUN npm install
COPY . /app
RUN npm run build --prod

FROM nginx:1.25.0-alpine
COPY --from=build /app/dist/app-front /usr/share/nginx/html

# FROM node:18.14.0-alpine as build
# WORKDIR /app
# COPY package*.json ./
# RUN npm ci
# COPY . .
# RUN npm run build

# FROM nginx:alpine
# #ADD ./config/default.conf /etc/nginx/conf.d/default.conf
# COPY nginx.conf /etc/nginx/conf.d/default.conf
# COPY --from=build /app/dist/AppFront /usr/share/nginx/html
# EXPOSE 4200
# #CMD ["npm", "run", "start"]
# CMD ["nginx", "-g", "daemon off;"]

# FROM node:18.14.0

# WORKDIR /app

# COPY package*.json ./

# RUN npm install

# COPY . .

# EXPOSE 4200

# CMD ["npm", "run", "start"]



# FROM node:18.14.0

# WORKDIR /app

# COPY ./dist /app

# RUN npm install -g http-server

# EXPOSE 4200

# CMD ["http-server", "./wep-app-front"]
