using SubsidiosClientes.Data;

namespace SubsidiosClientes.Services.Interfaces
{
    public interface INewExcelService
    {
        public void CrearExcelCostoMensual(SubsidiosContext _context, string currentDirectory);
    }
}
