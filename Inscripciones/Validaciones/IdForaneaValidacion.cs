using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Inscripciones.Validaciones
{
    public class IdForaneaValidacion : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                int id = Convert.ToInt32(value);

                if (id >= 1)
                    return ValidationResult.Success;
                else
                    return new ValidationResult("El ID debe ser mayor o igual a uno");
            }
            return new ValidationResult("Debes poner un ID");
        }
    }
}
