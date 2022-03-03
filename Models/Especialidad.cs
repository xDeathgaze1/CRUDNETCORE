using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Turnos.Models{

    public class Especialidad{

            [Key] //con esto especificamos al "ENTITY FRAMEWORK" que esta variable sera un PK y le dara la propiedad Identity
            public int idEspecialidad{ get; set; } //primary key

            public string descripcion{ set; get; }
            public List<MedicoEspecialidad> MedicoEspecialidad { get; set; }
            

    }

}