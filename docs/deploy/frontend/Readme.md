# Deploy del frontend

Los workflows que se ejecutan para el despliegue del frontend son los siguientes:
 - Cuando se hace un push a la rama `main` se ejecuta el workflow "[_Desplegar front de **PROD** a Firebase_](#desplegar-front-de-prod-a-firebase)".
 - Cuando se hace un push a la rama `develop` se ejecuta el workflow de [_Desplegar front de **STAGING** a Firebase_](#desplegar-front-de-staging-a-firebase).

Estos workflows solo se ejecutan si los commits pusheados hicieron cambios en la carpeta de proyecto del frontend (para no hacer un re-deploy del front cuando solo hay cambios en el back).

Cada uno de ellos requiere ciertas [variables de configuración](https://docs.github.com/es/actions/writing-workflows/choosing-what-your-workflow-does/store-information-in-variables#defining-configuration-variables-for-multiple-workflows) y [secretos de repositorio](https://docs.github.com/es/actions/security-for-github-actions/security-guides/using-secrets-in-github-actions).


## Desplegar front de PROD a Firebase

Se divide en compilación y despliegue.

#### Variables de configuración:
 - [PROD_FRONT_FIREBASE_ID](https://github.com/No-Country-simulation/c20-11-m-csharp-angular/settings/variables/actions): Indica el [ID de Firebase](https://firebase.google.com/docs/projects/learn-more?hl=es#project-identifiers) del proyecto de producción.
  
#### Secretos de repositorio:
 - [PROD_FRONT_FIREBASE_TOKEN](https://github.com/No-Country-simulation/c20-11-m-csharp-angular/settings/secrets/actions): Indica el [token de CI de Firebase](https://firebase.google.com/docs/cli?hl=es-419#cli-ci-systems) para el proyecto de producción.

### Compilar y crear artifact de prod

1. Hacer checkout de la rama `main`.
2. Preparar un entorno con Node 20.x.
3. Instalar las dependencias del projecto.
4. Compilar con `npm build`.
5. Archivar el artifact de compilación para el paso siguiente.

### Desplegar a Firebase

1. Hacer checkout de la rama `main`.
2. Descargar el artifact de compilación.
3. Desplegar el proyecto usando [w9jds/firebase-action@master](https://github.com/marketplace/actions/github-action-for-firebase).

El archivo del workflow se encuentra en [.github/workflows/deploy-prod-frontend.yml](/.github/workflows/deploy-prod-frontend.yml).


## Desplegar front de STAGING a Firebase

Se divide en compilación y despliegue.

#### Variables de configuración:
 - [STAGING_FRONT_FIREBASE_ID](https://github.com/No-Country-simulation/c20-11-m-csharp-angular/settings/variables/actions): Indica el [ID de Firebase](https://firebase.google.com/docs/projects/learn-more?hl=es#project-identifiers) del proyecto de staging.
  
#### Secretos de repositorio:
 - [STAGING_FRONT_FIREBASE_TOKEN](https://github.com/No-Country-simulation/c20-11-m-csharp-angular/settings/secrets/actions): Indica el [token de CI de Firebase](https://firebase.google.com/docs/cli?hl=es-419#cli-ci-systems) para el proyecto de staging.

### Compilar y crear artifact de prod

1. Hacer checkout de la rama `develop`.
2. Preparar un entorno con Node 20.x.
3. Instalar las dependencias del projecto.
4. Compilar con `npm build`.
5. Archivar el artifact de compilación para el paso siguiente.

### Desplegar a Firebase

1. Hacer checkout de la rama `develop`.
2. Descargar el artifact de compilación.
3. Desplegar el proyecto usando [w9jds/firebase-action@master](https://github.com/marketplace/actions/github-action-for-firebase).

El archivo del workflow se encuentra en [.github/workflows/deploy-staging-frontend.yml](/.github/workflows/deploy-staging-frontend.yml).