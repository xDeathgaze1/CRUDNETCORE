namespace Turnos.Models
{
    //ESTA CLASE O MODELO ES PARA RELACIONAR A MEDICO CON ESPECIALIDAD
    public class MedicoEspecialidad{

            
            public int idMedico { get; set; }
            public int idEspecialidad { get; set; }
            public Medico Medico { get; set; }
            public Especialidad Especialidad { get; set; }
    }
}