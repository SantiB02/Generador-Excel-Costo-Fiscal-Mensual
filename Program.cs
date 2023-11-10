using SubsidiosClientes.Data;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using SubsidiosClientes.Data.Entities;
using Microsoft.Extensions.DependencyInjection;
using SubsidiosClientes.Services.Implementations;
using System.Reflection;

Console.WriteLine("Programa de creación de base de datos de subsidios de clientes!");

Console.WriteLine("Actualizando base de datos...");

string currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
string archiveFolder = Path.Combine(currentDirectory, "archivos_excel_fuente");
string[] archivos = Directory.GetFiles(archiveFolder, "*.xlsx");

var services = new ServiceCollection();
services.AddDbContext<SubsidiosContext>(options => options.UseSqlite("Data Source=SubsidiosClientes.db"));

var serviceProvider = services.BuildServiceProvider();

SubsidiosContext _context = serviceProvider.GetService<SubsidiosContext>(); //instancio el context
_context.Database.Migrate(); //si no existe, crea la BDD y aplica migraciones

NewExcelService newExcelService = new();

ExcelPackage.LicenseContext = LicenseContext.NonCommercial; //defino el tipo de licencia para poder usar la librería

List<FaultyFile> faultyFiles = new();
int faultyCounter = 0;
bool newFiles = false;
List<string> numerosPrestamos = new();
int newFilesCounter = 0; //guardo los nuevos préstamos
List<Cuota> cuotas = new();

foreach (var archivo in archivos)
{
    using (var package = new ExcelPackage(new FileInfo(archivo)))
    {
        bool isFaulty = false;
        var worksheet = package.Workbook.Worksheets[0]; //elijo la primera hoja del Excel

        string? nombreCliente = worksheet.Cells["B1"].Text; //guardo el nombre del cliente
        string? fechaComunicacionBNA = worksheet.Cells["B2"].Text;
        decimal? montoCredito = worksheet.Cells["B3"].GetValue<decimal>();
        int? cantidadCuotas = worksheet.Cells["B4"].GetValue<int>();
        decimal? TNA = worksheet.Cells["B5"].GetValue<decimal>();
        decimal? TEM = worksheet.Cells["B6"].GetValue<decimal>();
        decimal? montoCuota = worksheet.Cells["B8"].GetValue<decimal>();
        string? nroPrestamo = worksheet.Cells["B9"].GetValue<string>();

        bool isInExcelDirectory = numerosPrestamos.Any(np => np == nroPrestamo);
        bool isInDataBase = _context.Prestamos.Any(p => p.NroPrestamo == nroPrestamo);

        Prestamo prestamo = new Prestamo
        {
            NombreCliente = nombreCliente,
            FechaComunicacionBNA = fechaComunicacionBNA,
            MontoCredito = montoCredito,
            CantidadCuotas = cantidadCuotas,
            TNA = TNA,
            TEM = TEM,
            MontoCuota = montoCuota,
            NroPrestamo = nroPrestamo
        };

        if (!isInExcelDirectory && !isInDataBase) //si el préstamo no fue ingresado previamente, lo agrego
        {
            numerosPrestamos.Add(nroPrestamo);
            newFiles = true;
            Console.WriteLine(archivo.ToString()); //imprimo el archivo con el que estoy trabajando

            int validRow;

            for (validRow = 1; validRow <= worksheet.Dimension.End.Row; validRow++)
            {
                if (DateTime.TryParse(worksheet.Cells[validRow, 1].Text, out _)) break; //cuando encuentra la fila válida frena
            }

            for (int row = validRow; row <= worksheet.Dimension.End.Row; row++)
            {
                if (worksheet.Cells[row, 1].Text == "") break; //Si terminó la tabla, termino el bucle.

                string? dateToValidate = worksheet.Cells[row, 1].Text;

                if (DateTime.TryParse(dateToValidate, out DateTime dateValue)) //intenta convertir el vencimiento de la cuota a DateTime
                {
                    DateTime fechaVencimiento = dateValue;
                    int? nroCuota = worksheet.Cells[row, 2].GetValue<int>();
                    decimal? saldoInicialCapital = worksheet.Cells[row, 3].GetValue<decimal>();
                    decimal? valorCuota = worksheet.Cells[row, 4].GetValue<decimal>();
                    decimal? interes = worksheet.Cells[row, 5].GetValue<decimal>();
                    decimal? amortizacionCapital = worksheet.Cells[row, 6].GetValue<decimal>();
                    decimal? saldoFinalCapital = worksheet.Cells[row, 7].GetValue<decimal>();
                    decimal? interesControlTNA = worksheet.Cells[row, 8].GetValue<decimal>();
                    decimal? interesControlSubsidio = worksheet.Cells[row, 9].GetValue<decimal>();
                    int? cantidadDias = worksheet.Cells[row, 10].GetValue<int>();

                    Cuota cuota = new Cuota
                    {
                        Prestamo = prestamo,
                        FechaVencimiento = fechaVencimiento,
                        NroCuota = nroCuota,
                        SaldoInicialCapital = saldoInicialCapital,
                        ValorCuota = valorCuota,
                        Interes = interes,
                        AmortizacionCapital = amortizacionCapital,
                        SaldoFinalCapital = saldoFinalCapital,
                        InteresControlTNA = interesControlTNA,
                        InteresControlSubsidio = interesControlSubsidio,
                        CantidadDias = cantidadDias,
                    };
                    cuotas.Add(cuota);
                    _context.Add(cuota); //agrego cada cuota a la tabla Cuotas
                }
                else
                {

                    isFaulty = true;
                }
                
            }
            if (isFaulty) //si el archivo tiene al menos una fecha de cuota mal, lo borro del contexto
            {
                faultyCounter++;
                faultyFiles.Add(
                        new FaultyFile { Path = archivo.ToString(), ErrorDescription = "Este archivo tiene fechas de vencimiento de cuotas con formato erróneo" });
                _context.Remove(prestamo);
            } else
            {
                _context.Add(prestamo); //agrego el préstamo a la tabla Prestamos
                newFilesCounter++;
            }
        } else
        {
            if (!isInExcelDirectory && isInDataBase)
            {
                numerosPrestamos.Add(nroPrestamo);
            }
            if (isInExcelDirectory)
            {
                faultyCounter++;
                FaultyFile faultyFile = new FaultyFile
                {
                    Path = archivo.ToString(),
                    ErrorDescription = $"Este archivo tiene un número de préstamo ya existente en la base de datos ({nroPrestamo})"
                };
                faultyFiles.Add(faultyFile);
            }
        }
    }
}

List<Prestamo> prestamosBaseDeDatos = _context.Prestamos.Include(p => p.Cuotas).ToList();
List<string> infoPrestamosBorrados = new();

foreach (Prestamo prestamo in prestamosBaseDeDatos)
{
    if (!numerosPrestamos.Any(np => np == prestamo.NroPrestamo))
    {
        _context.Remove(prestamo); //si el Excel no existe pero en la base está, entonces lo elimino de la BDD
        infoPrestamosBorrados.Add($"Préstamo eliminado: {prestamo.NombreCliente}, número de préstamo: {prestamo.NroPrestamo}");
    }
}
_context.SaveChanges(); //Al terminar de trabajar con la BDD, guardo todo
if (newFiles)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"\nSe encuentran cargados {_context.Prestamos.Count()} préstamos en la base de datos");
    Console.WriteLine($"Se han agregado {newFilesCounter} nuevos préstamos a la base de datos");
    Console.ForegroundColor = ConsoleColor.White;
    if (faultyCounter == 0)
    {
        Console.WriteLine("No hay archivos con errores");
    } else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\n{faultyCounter} archivos con errores:");
        Console.ForegroundColor = ConsoleColor.White;
        foreach (FaultyFile faultyFile in faultyFiles)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("--------------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Ruta del archivo: {faultyFile.Path}");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {faultyFile.ErrorDescription}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

} else
{
    Console.WriteLine("Su base de datos ya se encuentra actualizada");
}

if (infoPrestamosBorrados.Any())
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("\nSe han eliminado los siguientes préstamos de la base de datos:");
    foreach (string infoPrestamoBorrado in infoPrestamosBorrados)
    {
        Console.WriteLine(infoPrestamoBorrado);
    }
    Console.ForegroundColor = ConsoleColor.White;
}

if (_context.Prestamos.Any())
{
    newExcelService.CrearExcelCostoMensual(_context, currentDirectory);
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("\nArchivo Excel creado con éxito dentro de la carpeta 'resultados'");
    Console.ForegroundColor = ConsoleColor.White;
}
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("@Copyright - Santiago Brasca");
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("\n Presione una tecla para cerrar el programa...");
Console.ReadKey();