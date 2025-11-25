using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Enums
{
   
    public enum ModalidadPagoEnum
    {        
        [Description("Pago Único")]
        PagoUnico = 1,

        [Description("Gratuito")]
        Gratuito = 2
    }
}
