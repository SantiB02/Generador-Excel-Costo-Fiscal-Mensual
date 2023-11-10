# Generador-Excel-Costo-Fiscal-Mensual
Aplicación de consola programada en .NET 6 y EF Core para extraer datos de muchos Cronogramas de préstamos en formato Excel dentro de una carpeta para generar su consecuente archivo de Costo Fiscal Mensual de todos los préstamos. Es una aplicación muy simple para automatizar un proceso de extracción de datos de cientos de archivos Excel y generación de uno nuevo.

# ¿Qué hace este programa?
Recorre múltiples archivos Excel alojados en una carpeta "archivos_excel_fuente" con el siguiente formato de tabla:
| **Cliente**                      | nombre              |                           |           |             |                             |                         |                 |                      |                   |
|----------------------------------|---------------------|---------------------------|-----------|-------------|-----------------------------|-------------------------|-----------------|----------------------|-------------------|
| **Fecha comunicación del banco** | fecha               |                           |           |             |                             |                         |                 |                      |                   |
| **Monto otorgado**               | monto               |                           |           |             |                             |                         |                 |                      |                   |
| **Cantidad de cuotas**           | cantidad            |                           |           |             |                             |                         |                 |                      |                   |
| TNA                              | tasa                |                           |           |             |                             |                         |                 |                      |                   |
| TEM                              | tasa                |                           |           |             |                             |                         |                 |                      |                   |
|                                  |                     |                           |           |             |                             |                         |                 |                      |                   |
| **Monto cuota**                  | monto               |                           |           |             |                             |                         |                 |                      |                   |
| **Número de préstamo**           | número              |                           |           |             |                             |                         |                 |                      |                   |
|                                  |                     |                           |           |             |                             |                         |                 |                      |                   |
| **Fecha vencimiento cuota**      | **Número de cuota** | **Saldo Inicial Capital** | **Cuota** | **Interés** | **Amortización de capital** | **Saldo Final Capital** | **Interés TNA** | **Interés Subsidio** | **Cantidad Días** |

El programa extrae los valores necesarios a través de la librería EPPlus, los guarda en una base de datos relacional y finalmente genera un archivo Excel (dentro de una carpeta "resultados") del Costo Fiscal Mensual de todos los préstamos (con todos sus años y meses), mostrando el valor del Interés Subsidio.

# ¿Por qué hice este proyecto?
Debido a la necesidad de un familiar cercano de generar informes a partir de cientos de archivos Excel distintos. Mi objetivo fue automatizar ese proceso y generar un informe con todos los préstamos en cuestión de segundos. Además, realicé este proyecto como un desafío personal y como parte de mi camino de aprendizaje en el mundo de la programación back-end en C#. En este proyecto personal pude implementar lo aprendido en mi cursado universitario así como también incorporé conocimientos nuevos mediante documentación oficial de Microsoft, foros y guías en Internet.

# ¿Qué tecnologías usé?
.NET 6.0, Entity Framework Core, SQLite, EPPlus (para la manipulación de archivos Excel) y ciertos paquetes NuGet para poder realizar migraciones y demás.
