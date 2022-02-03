# Fravega
Este repositorio cuenta con la solucion al challenge planteado por Fravega es una Api creada con .net core 3.1. Se deben seguir los siguientes pasos para levantarla.

Pasos:

1 - Clonar el repo y abrirlo con visual studio.

2 - apuntar desde el appsettings a un servidor sql server para crear la base de datos en base a las migraciones.

3 - correr el comando Update-Database o levantar la api para correr todas las migraciones y crear la Base de datos con las especificaciones solicitadas.

4 - Ejecutar la aplicacion y probarla para comprobar si cumple o no los requisitos solicitados.

Requisitos :

1- servidor sql server



Sobre los endpoints:

La api cuenta con 4 endpoints los cuales son:

1-ObtenerTodasLasSucursales: utilizado para listar todas las sucursales y poder ver facilmente el id de las mismas
para luego ver el detalle con el endpoint ObtenerSucursalPorId

2-CrearSucursal: utilizado para crear sucursales

3-ObtenerSucursalPorId : utilizado para consultar el detalle de una sucursal por medio de su id

4-ObtenerSucursalMasCercana: utilizado para obtener la sucursal mas cercana a la longitud y latitud enviadas en el request

Nota: la api cuenta con swagger por lo que al levantarla se podran visualizar los enpoints descritos previmanete y asu vez swagger indicara que
informacion es requerida para hacer la peticion a la api.
