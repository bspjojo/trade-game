### STAGE 1: Build ###

# We label our stage as ‘scoring-builder’
FROM node:10-alpine as scoring-builder

COPY package.json package-lock.json ./

## Storing node modules on a separate layer will prevent unnecessary npm installs at each build

RUN npm ci && mkdir /ng-app && mv ./node_modules ./ng-app

WORKDIR /ng-app

COPY . .

## Build the angular app in production mode and store the artifacts in dist folder

RUN npm run ng build -- --prod --output-path=dist


### STAGE 2: Setup ###

FROM nginx:1.14.1-alpine

## Copy our default nginx config
COPY nginx/default.conf /etc/nginx/conf.d/

## Remove default nginx website
RUN rm -rf /usr/share/nginx/html/*

## From ‘scoring-builder’ stage copy over the artifacts in dist folder to default nginx public folder
COPY --from=scoring-builder /ng-app/dist /usr/share/nginx/html

EXPOSE 4301

CMD ["nginx", "-g", "daemon off;"]