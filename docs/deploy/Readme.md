# Deploy

Este repo está configurado para hacer el despliegue del código automáticamente con Github Actions.

Se despliegan tres servicios:
 - El frontend ([ver el workflow](./frontend/Readme.md)) se despliega a [Firebase Hosting](https://firebase.google.com/docs/hosting?hl=es-419).
   - Link del sitio de PROD: [https://tasty-cliente.web.app/](https://tasty-cliente.web.app/)
   - Link del sitio de STAGING: [https://tasty-cliente-staging.web.app/](https://tasty-cliente-staging.web.app/)
 - El backend se despliega a [Render Web Services](https://docs.render.com/web-services).
   - Link de la API de PROD: [https://tasty-api-v1.onrender.com](https://tasty-api-staging.onrender.com)
   - Link de la API de STAGING: [https://tasty-api-staging.onrender.com](https://tasty-api-staging.onrender.com)
 - La base de datos (workflow todavía no implementado) se despliega a [Aiven MySQL](https://aiven.io/mysql).

> [!TIP]
> Si querés ver y explorar todos los endpoints que tiene la API, usá el link de STAGING.

Cada servicio se encuentra duplicado para cada [entorno de despliegue](#ambientes-o-entornos-de-despliegue).

## Arquitectura de la aplicación

El siguiente diagrama muestra la arquitectura de la aplicación. Es igual tanto para el ambiente de staging como el de producción:

<picture>
  <source media="(prefers-color-scheme: dark)" srcset="./arch-dark.png">
  <img src="./arch-light.png">
</picture>

## Ambientes o entornos de despliegue

El despliegue se realiza a dos entornos distintos:

### **PROD**

**_Es lo que va a usar el usuario final_**

Es el entorno de producción. Contiene la versión final de cada servicio y es públicamente accesible por el usuario.

Se despliega a este entorno cuando se hace un push a la rama `main`.
  
### **STAGING**

**_Es para hacer pruebas_**

Es un entorno idéntico al de producción, pero se encuentra aislado del público. Se usa principalmente para testear la aplicación antes de lanzarla a PROD.

Se despliega a este entorno cuando se hace un push a la rama `develop`.

> [!TIP]
> Más información acerca de las [diferencias entre el ambiente de producción y staging](https://dev.to/flippedcoding/difference-between-development-stage-and-production-d0p).
