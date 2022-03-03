using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Turnos.Models;

namespace Turnos.Controllers
{
    public class especialidadController : Controller //hereda de controller 
    {   


        private readonly TurnosContext _context;
        //Iniciamos el contrstuctor con la clase "TurnosContext" para que haya conexión con la BBDD
        public especialidadController(TurnosContext context)//ESTE ES UN CONSTRUCTOR , EL CONSTRUCTOR INICIALIZA VARIABLES
        {
            _context = context;
        }
        
        /* EJEMPLO DE UN METODO QUE NO ESTA ASINCRONO
            public IActionResult Index(){//la vista retornara con el mismo nombre del metodo
            
            return View(_context.Especialidad.ToList());//le pasamos como parametro, va a acceder a la tabla y con "ToList" devuelve todos los registros de especialidad
            //osea que cuando se ejecute , hara una consulta de SQL

        }    //Clase/metodo que muestra el resultado en la vista
        
        */


        //ESTOS METODOS ESTAN ASINCRONOS

        public async Task<IActionResult> Index(){//la vista retornara con el mismo nombre del metodo
            
            return View(await _context.Especialidad.ToListAsync());//le pasamos como parametro, va a acceder a la tabla y con "ToList" devuelve todos los registros de especialidad
            //osea que cuando se ejecute , hara una consulta de SQL

        }    //Clase/metodo que muestra el resultado en la vista

        //Este metodo es para editar una fila de la tabla
        public async Task<IActionResult> Edit(int? id){//se agrega ? por se puede recibir en null, permite valores nulos

            if(id == null)
            {
                return NotFound();//devuelve el error 404
            }

            var especialidad = await _context.Especialidad.FindAsync(id); //Es similar al un Select from "where" id.
            
            if(especialidad == null){

                return NotFound();
            }

            return  View(especialidad);

        }
        [HttpPost]//este editar es para guardar cambios nuevos a la BBDD
        public async Task<IActionResult> Edit(int id, [Bind("idEspecialidad,descripcion")] Especialidad especialidad )
        {
            if(id != especialidad.idEspecialidad){

                    return NotFound();
            }
            if(ModelState.IsValid){

                _context.Update(especialidad);//estamos pasando el objeto para que modifique?
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); //si todo se realiza correctamente , entonces que se dirija al inicio
            }
            return View(especialidad);//puede mostrar el error?
        }
        /*
        public async Task<IActionResult> Delete(int? id){

            if(id==null){

                return NotFound();

            }
            //realiza una busqueda
            var especialidad =await _context.Especialidad.FirstOrDefaultAsync(e => e.idEspecialidad == id); //va a comparar el id mandado por el metodo a la BBDD y si no encuentra manda null o default
            
            if(especialidad == null){

                return NotFound();

            }

            return View(especialidad);//si lo quito nunca me va a mostrar lo que contiene la columna

        }*/
        //[HttpPost] Se quita el post porque ya no es necesario, quiero que el boton eliminar lo elimine sin tener que ir a otra vista
        public async Task<IActionResult> Delete(int id){

            var especialidad =await _context.Especialidad.FindAsync(id);
            _context.Especialidad.Remove(especialidad);//delete from table where id
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create(){

            return View();
        }

        [HttpPost]
        //Bind es para especificar que datos se guardaran en la BBDD cuando el formulario le de submit
        [ValidateAntiForgeryToken]//previene ataques  malintencionados, y solo se valida cuando se presiona el boton del formulario

        public async Task<IActionResult> Create([Bind("idEspecialidad,descripcion")] Especialidad especialidad){
            
            if(ModelState.IsValid){//si todo esta correcto

                _context.Add(especialidad);//insert into tabla valores.
                await _context.SaveChangesAsync();//guarda cambios
                return RedirectToAction(nameof(Index));//regresa al index , todo correcto

            }
            return View();
        }

    }
}