using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Turnos.Models{

        public class Medico{

            [Key]
            public int idMedico { get; set; }

            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Direccion { get; set; }
            public string Telefono { get; set; }
            public string Email { get; set; }
            public DateTime HorarioAtencionDesde { get; set; }
            public DateTime HorarioAtencionHasta { get; set; }
            public List<MedicoEspecialidad> MedicoEspecialidad { get; set; } //vincula el modelo medico y obtiene los datos de especialidad


        }
}