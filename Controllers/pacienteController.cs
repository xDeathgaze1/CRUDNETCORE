using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Turnos.Models;
using Microsoft.EntityFrameworkCore;

namespace Turnos.Controllers{


    public class pacienteController : Controller{

            //Conexion a la BBDD
            private readonly TurnosContext _context;

            public pacienteController(TurnosContext context){
                _context = context;
            }

            public async Task<IActionResult> Index(){

                return View(await _context.Pacientes.ToListAsync()); 
               
            }

            public async Task<IActionResult> Delete(int id){

                var paciente =await _context.Pacientes.FindAsync(id);
                _context.Pacientes.Remove(paciente);//delete from table where id
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            public IActionResult Create(){

                    return View();
            }
           
            [HttpPost]
                //Bind es para especificar que datos se guardaran en la BBDD cuando el formulario le de submit
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("idPaciente,Nombre,Apellido,Direccion,Telefono,Email")] Pacientes pacientes){
            
                if(ModelState.IsValid){//si todo esta correcto

                        _context.Add(pacientes);//insert into tabla valores.
                        await _context.SaveChangesAsync();//guarda cambios
                        return RedirectToAction(nameof(Index));//regresa al index , todo correcto

                }
                return View(pacientes);
            }
            public async Task<IActionResult> Edit(int? id){//se agrega ? por se puede recibir en null, permite valores nulos

                if(id == null)
                {
                        return NotFound();//devuelve el error 404
                }

                var pacientes = await _context.Pacientes.FindAsync(id); //Es similar al un Select from "where" id.
            
                if(pacientes == null){

                   return NotFound();
                }

                return  View(pacientes);

            }
            [HttpPost]//este editar es para guardar cambios nuevos a la BBDD
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("idPaciente,Nombre,Apellido,Direccion,Telefono,Email")]  Pacientes pacientes )
            {
                if(id != pacientes.idPaciente){

                    return NotFound();
                }
                if(ModelState.IsValid){

                    _context.Update(pacientes);//estamos pasando el objeto para que modifique?
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index)); //si todo se realiza correctamente , entonces que se dirija al inicio
                }
                return View(pacientes);//puede mostrar el error?
            }   


    }
}