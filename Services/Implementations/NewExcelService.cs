using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using SubsidiosClientes.Data;
using SubsidiosClientes.Data.Entities;
using SubsidiosClientes.Services.Interfaces;

namespace SubsidiosClientes.Services.Implementations
{
    public class NewExcelService : INewExcelService
    {
        public void CrearExcelCostoMensual(SubsidiosContext _context, string currentDirectory)
        {
            using (ExcelPackage excel = new ExcelPackage())
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nIngrese el nombre del documento Excel de costo fiscal mensual a crear:");
                string fileName = Console.ReadLine();
                Console.WriteLine("Creando el archivo Excel de costo fiscal mensual. Por favor espere...");
                Console.ForegroundColor = ConsoleColor.White;
                excel.Workbook.Worksheets.Add("Costo Fiscal Mensual");

                var excelWorksheet = excel.Workbook.Worksheets["Costo Fiscal Mensual"];

                List<string[]> headerRow = new List<string[]>()
                {
                    new string[] {"Razón Social", "Nro Préstamo", "Año", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"}
                };

                string headerRange = "A1:" + Char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";
                excelWorksheet.Cells[headerRange].LoadFromArrays(headerRow);

                int row = 2;
                List<Prestamo> prestamos = _context.Prestamos.Include(p => p.Cuotas).ToList(); //me traigo todos los préstamos de la BDD
                foreach (Prestamo prestamo in prestamos)
                {
                    excelWorksheet.Cells[row, 1].Value = prestamo.NombreCliente;
                    excelWorksheet.Cells[row, 2].Value = prestamo.NroPrestamo;
                    List<int> anios = new();
                    foreach (Cuota cuota in prestamo.Cuotas)
                    {
                        int anioCuota = cuota.FechaVencimiento.Year;
                        if (!anios.Any(a => a == anioCuota))
                        {
                            anios.Add(anioCuota);
                        }
                    }
                    foreach (int anio in  anios)
                    {
                        excelWorksheet.Cells[row, 3].Value = anio;
                        foreach (Cuota cuota in prestamo.Cuotas)
                        {     
                            for (int monthColumn = 4; monthColumn <= 15; monthColumn++)
                            {
                                if (cuota.FechaVencimiento.Month == (monthColumn - 3) && cuota.FechaVencimiento.Year == anio)
                                {
                                    excelWorksheet.Cells[row, monthColumn].Value = cuota.InteresControlSubsidio;
                                }
                            }
                        }
                        row++;
                    }
                }
                FileInfo excelFile = new FileInfo(Path.Combine(currentDirectory, "resultados", $"{fileName}.xlsx"));
                excel.SaveAs(excelFile);
            }
        }
    }
}
