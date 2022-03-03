using Microsoft.EntityFrameworkCore;
using Turnos.Models;
//Conexion a la BBDD . etc-...
//en appsettings.json se crea el vinculo para la BD
//ver connectionstring , ahi se define el nombre del context osea en este caso TurnosContext. Y Tambien se escribe la cadena para la conexion a SQL
namespace Turnos.Models
{
        public class TurnosContext : DbContext
        {

            public TurnosContext(DbContextOptions<TurnosContext> opciones) 
            : base(opciones) //se pone "base" para indicar que la variable opciones , se inicia con valores por default 
            {
                    
            }

            //se genera la tabla en SQLSERVER codigo
            public DbSet<Especialidad> Especialidad { get; set; } //DbSet es una tabla o identidad, el nombre del metodo "DbSet" sera el nombre de la tabla en SQL
            public DbSet<Pacientes> Pacientes {get; set;}
            public DbSet<Medico> Medico { get; set; }
            public DbSet<MedicoEspecialidad> MedicoEspecialidad {get; set;}

            protected override void OnModelCreating(ModelBuilder modelBiuilder){//no se puede acceder esta protegido el metodo
                 //Propiedades de las tablas en la BBDD
                    modelBiuilder.Entity<Especialidad>(entidad =>
                    {
                            entidad.ToTable("Especialidad");
                            entidad.HasKey(e => e.idEspecialidad);//especificamos que sera el PK
                            entidad.Property(e => e.descripcion)//especificamos las propiedades de "descripcion
                            .IsRequired()//no aceptan nulos
                            .HasMaxLength(100)//tamaño 100
                            .IsUnicode(false);//no es unicodes

                    }
                    );
                    modelBiuilder.Entity<Pacientes>(entidad =>{
                            entidad.ToTable("Pacientes");
                            entidad.HasKey(p => p.idPaciente);//especificamos que sera el PK
                            entidad.Property(p => p.Nombre)//especificamos las propiedades de "descripcion
                            .IsRequired()//no aceptan nulos
                            .HasMaxLength(100)//tamaño 100
                            .IsUnicode(false);//no es unicodes
                            entidad.Property(p => p.Apellido)
                            .IsRequired()
                            .HasMaxLength(50)
                            .IsUnicode(false);
                            entidad.Property(p => p.Direccion)
                            .IsRequired()
                            .HasMaxLength(50)
                            .IsUnicode(false);
                            entidad.Property(p => p.Telefono)
                            .IsRequired()
                            .HasMaxLength(10)
                            .IsUnicode(false);
                            entidad.Property(p => p.Email)
                            .IsRequired()
                            .HasMaxLength(50)
                            .IsUnicode(false);


                    });
                    modelBiuilder.Entity<Medico>(entidad =>{
                            entidad.ToTable("Medico");
                            entidad.HasKey(m => m.idMedico);//especificamos que sera el PK
                            entidad.Property(m => m.Nombre)//especificamos las propiedades de "descripcion
                            .IsRequired()//no aceptan nulos
                            .HasMaxLength(100)//tamaño 100
                            .IsUnicode(false);//no es unicodes
                            entidad.Property(m => m.Apellido)
                            .IsRequired()
                            .HasMaxLength(50)
                            .IsUnicode(false);
                            entidad.Property(m => m.Direccion)
                            .IsRequired()
                            .HasMaxLength(50)
                            .IsUnicode(false);
                            entidad.Property(m => m.Telefono)
                            .IsRequired()
                            .HasMaxLength(10)
                            .IsUnicode(false);
                            entidad.Property(m => m.Email)
                            .IsRequired()
                            .HasMaxLength(50)
                            .IsUnicode(false);
                            entidad.Property(m => m.HorarioAtencionDesde)
                            .IsRequired()
                            .IsUnicode(false);
                            entidad.Property(m => m.HorarioAtencionHasta)
                            .IsRequired()
                            .IsUnicode(false);



                    });

                    //sE DEFINEN RESTRICCIONES DE FK
                    modelBiuilder.Entity<MedicoEspecialidad>().HasKey(x => new{ x.idMedico, x.idEspecialidad});//Define PK compuesta
                   
                    modelBiuilder.Entity<MedicoEspecialidad>().HasOne(x => x.Medico)//ESTABLECEMOS LA RELACION DE UNO A MUCHOS
                    .WithMany(p => p.MedicoEspecialidad)
                    .HasForeignKey(p => p.idMedico);//se define que campo sera FK
                   
                    modelBiuilder.Entity<MedicoEspecialidad>().HasOne(x => x.Especialidad)//ESTABLECEMOS LA RELACION DE UNO A MUCHOS
                    .WithMany(p => p.MedicoEspecialidad)
                    .HasForeignKey(p => p.idEspecialidad);

                    
            }
        }
}