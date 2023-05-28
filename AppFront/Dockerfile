
FROM node:18.14.0

WORKDIR /app

COPY package*.json ./

RUN npm install

COPY . .

EXPOSE 4200

CMD ["npm", "run", "start"]



# FROM node:18.14.0

# WORKDIR /app

# COPY ./dist /app

# RUN npm install -g http-server

# EXPOSE 4200

# CMD ["http-server", "./wep-app-front"]
