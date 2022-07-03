FROM node:14-alpine

RUN apk add --no-cache vim

WORKDIR /app

COPY . .

RUN yarn install --production

CMD ["node", "index.js"]

EXPOSE 3000