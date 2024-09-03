# Deploy del Backend

## Deploy manual

> [!NOTE]
> No es necesario hacerlo manualmente. Cuando combines tus cambios con la rama `main` o `develop` se deployea el sitio automágicamente.

Para deployar manualmente a producción se necesita tener instalado [Docker y Docker Desktop](https://www.docker.com/products/docker-desktop/).

Luego, se deben ejecutar estos comandos:

```sh
# Ejecutar los comandos en la siguiente carpeta:
# src/TastysBackend

# Loguearse a la cuenta de Docker Hub
# Te va a pedir una contraseña, pedirla al canal 'backend' del Discord.
docker login --username tastydevops

# Construir imagen (no olvidarse del punto al final)
docker build -t tastydevops/tasty-api:prod .

# Pushear imagen
docker push tastydevops/tasty-api:prod

# Avisarle a Render para que actualice la imagen
# RENDER_DEPLOY_HOOK también es importante que sea secreta, pedirla al canal 'backend' del Discord.
curl RENDER_DEPLOY_HOOK
```

## Deploy automático

Los workflows que se ejecutan para el despliegue del backend son los siguientes:
 - Cuando se hace un push a la rama `main` se ejecuta el workflow "[_Desplegar api de **PROD** a Render_](#desplegar-api-de-prod-a-render)".
 - Cuando se hace un push a la rama `develop` se ejecuta el workflow de [_Desplegar api de **STAGING** a Render_](#desplegar-api-de-staging-a-render).

Estos workflows solo se ejecutan si los commits pusheados hicieron cambios en la carpeta de proyecto del backend (para no hacer un re-deploy de la API cuando solo hay cambios en el front).

Cada uno de ellos requiere ciertas [variables de configuración](https://docs.github.com/es/actions/writing-workflows/choosing-what-your-workflow-does/store-information-in-variables#defining-configuration-variables-for-multiple-workflows) y [secretos de repositorio](https://docs.github.com/es/actions/security-for-github-actions/security-guides/using-secrets-in-github-actions).


## Desplegar api de PROD a Render

Se divide en construcción y despliegue.

#### Variables de configuración:
 - [PROD_BACK_IMAGE_TAG](https://github.com/No-Country-simulation/c20-11-m-csharp-angular/settings/variables/actions): Indica el [tag de la imagen](https://mgallego.gitlab.io/posts/docker-imagenes/#headline-3) de la API de producción.
 - [PROD_DOCKERHUB_USERNAME](https://github.com/No-Country-simulation/c20-11-m-csharp-angular/settings/variables/actions): Indica el nombre de usuario de Docker Hub, donde se encuentra el repositorio para las imágenes de la API de producción.

  
#### Secretos de repositorio:
 - [PROD_DOCKERHUB_TOKEN](https://github.com/No-Country-simulation/c20-11-m-csharp-angular/settings/secrets/actions): Indica el [token de autenticación de Docker Hub](https://docs.docker.com/security/for-developers/access-tokens/) para el repositorio de imágenes.
 - [PROD_RENDER_DEPLOY_HOOK](https://github.com/No-Country-simulation/c20-11-m-csharp-angular/settings/secrets/actions): Indica el [webhook de deploy](https://docs.render.com/deploy-hooks) para avisarle a Render que hay una nueva imagen de la API.

### Construir y pushear imagen

1. Hacer checkout de la rama `main`.
2. Preparar un entorno con Buildx y Docker.
3. Loguearse en Docker.
4. Construir imagen.
5. Subir la imagen al registry de producción.

### Desplegar a Render

1. Hacer checkout de la rama `main` (por si se necesita a futuro).
2. Mandar un GET al webhook de deploy de Render.

El archivo del workflow se encuentra en [.github/workflows/deploy-prod-backend.yml](/.github/workflows/deploy-prod-backend.yml).


## Desplegar api de STAGING a Render

Se divide en construcción y despliegue.

#### Variables de configuración:
 - [STAGING_BACK_IMAGE_TAG](https://github.com/No-Country-simulation/c20-11-m-csharp-angular/settings/variables/actions): Indica el [tag de la imagen](https://mgallego.gitlab.io/posts/docker-imagenes/#headline-3) de la API de staging.
 - [STAGING_DOCKERHUB_USERNAME](https://github.com/No-Country-simulation/c20-11-m-csharp-angular/settings/variables/actions): Indica el nombre de usuario de Docker Hub, donde se encuentra el repositorio para las imágenes de la API de staging.

  
#### Secretos de repositorio:
 - [STAGING_DOCKERHUB_TOKEN](https://github.com/No-Country-simulation/c20-11-m-csharp-angular/settings/secrets/actions): Indica el [token de autenticación de Docker Hub](https://docs.docker.com/security/for-developers/access-tokens/) para el repositorio de imágenes.
 - [STAGING_RENDER_DEPLOY_HOOK](https://github.com/No-Country-simulation/c20-11-m-csharp-angular/settings/secrets/actions): Indica el [webhook de deploy](https://docs.render.com/deploy-hooks) para avisarle a Render que hay una nueva imagen de la API.

### Construir y pushear imagen

1. Hacer checkout de la rama `develop`.
2. Preparar un entorno con Buildx y Docker.
3. Loguearse en Docker.
4. Construir imagen.
5. Subir la imagen al registry de staging.

### Desplegar a Render

1. Hacer checkout de la rama `develop` (por si se necesita a futuro).
2. Mandar un GET al webhook de deploy de Render.

El archivo del workflow se encuentra en [.github/workflows/deploy-prod-backend.yml](/.github/workflows/deploy-prod-backend.yml).
