using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Enums
{
    public static class EnumExtensions
    {
        // Este es un Extension Method (uso de 'this' en el primer parámetro)
        // Permite llamar al método directamente sobre el enum: MiEnum.MiValor.GetDescription()
        public static string GetDescription(this Enum value)
        {
            // 1. Obtiene el campo (el nombre del valor del enum, ej: PagoUnico)
            FieldInfo fi = value.GetType().GetField(value.ToString());

            if (fi != null)
            {
                // 2. Intenta leer el atributo [Description] de ese campo
                DescriptionAttribute[] attributes =
                    (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute),
                    false);

                // 3. Si encuentra el atributo, devuelve el texto de la descripción
                if (attributes != null && attributes.Length > 0)
                    return attributes[0].Description;
            }

            // Si falla, devuelve el nombre del enum (ej: "PagoUnico")
            return value.ToString();
        }
    }
}
