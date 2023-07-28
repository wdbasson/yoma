# Yoma API Core CICD

## Running local development environment

### Local development stack (BE)

Run:
```
docker-compose up
```

Tear down:
```
docker-compose down
```

Tear down and delete persisted volumes:
```
docker-compose down -v
```

(re)build all containers & run
```
docker-compose up --build
```

(re)build a specific container:
```
docker-compose build yoma-api-core
```

### Local development stack (BE +FE)
#### Start BE stack:
```
docker-compose up
```

#### Clone the FE repo and run the FE stack:
```
git clone git@github.com:didx-xyz/yoma-web-app-reboot.git
cd yoma-web-app-reboot
docker-compose up
```

### Useful docker-compose commands

#### Tear down and delete persisted DB volumes and containers:
```
docker-compose down -v --rmi all
```

#### Build:
```
docker-compose up --build
```

#### Build with no caching:
```
docker-compose up --build --no-cache
```

#### Build a specific image with no caching:
Change <image-name> to e.g., `sqlserver-init`
```
docker-compose build --no-cache <image-name>
```

# Yoma API Core Keycloak CaC

How to prepare user profile and realm exports for Keycloak CaC.

Sort json:
```
jq -S '.' realm-export.json > realm-export.json
```

Convert to yaml
```
yq eval -o=yaml '.' realm-export.json > realm-export.yaml
``````
