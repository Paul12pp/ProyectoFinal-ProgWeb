using ProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Interface
{
    public interface IPago
    {
        int AddPago(Pago model);
        int EditPago(int idpago, Pago model);
        int DeletePago(int idpago);
        IEnumerable<Pago> GetPagos();
    }
}
