using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Turnos.Models;

namespace Turnos.Controllers
{
    public class MedicoController : Controller
    {
        private readonly TurnosContext _context;

        public MedicoController(TurnosContext context)
        {
            _context = context;
        }

        // GET: Medico
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Medico.ToListAsync());
            //en este caso hacemos un select * from con un inner join con las tablas medico , medico especialidad y especialidad
            //var medico guardara en una lista, los datos que resulten de la consulta con inner join
            var medico = await _context.Medico.Include(me => me.MedicoEspecialidad)
                .ThenInclude(e => e.Especialidad).ToListAsync();
            return View(medico);
        }

        // GET: Medico/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //Hacemos un inner join con un where ya que en vez de obtener todos los registros la condicion es que 
            //se obtenga un registro especificando de la id
            var medico = await _context.Medico
                .Where(m => m.idMedico == id).Include(me => me.MedicoEspecialidad)
                .ThenInclude(e => e.Especialidad).FirstOrDefaultAsync();
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // GET: Medico/Create
        public IActionResult Create()
        {
            //con ViewData hacemos una lista que muestre los siguientes registros , y sera llamado por un viewbag
            ViewData["ListaEspecialidades"] = new SelectList(_context.Especialidad,"idEspecialidad","descripcion");//matriz que obtienes los registros de especialidades
            return View();
        }

        // POST: Medico/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //dentro del metodo agregamos los datos del formulario para la tabla medico y la tabla medicoespecialidad.
        public async Task<IActionResult> Create([Bind("idMedico,Nombre,Apellido,Direccion,Telefono,Email,HorarioAtencionDesde,HorarioAtencionHasta")] Medico medico, int idEspecialidad)
        {                                       //Bind obtiene todo del formulario y lo guarda en el objeto MEDICO y tambien un dato extra
            if (ModelState.IsValid)
            {

                //primero insertamos un nuevo registro con los datos que tiene el objeto medico
                _context.Add(medico);
                await _context.SaveChangesAsync();
                //una vez guardados los cambios, inicializamos el objeto
                //a ese objeto llamaremos los metodos set de cada atributo
                //despues haremos un insert a la tabla medicoespecialidad
                var medicoEspecialidad = new MedicoEspecialidad();
                medicoEspecialidad.idMedico = medico.idMedico;
                medicoEspecialidad.idEspecialidad = idEspecialidad;
                _context.Add(medicoEspecialidad);
                await _context.SaveChangesAsync();
                


                return RedirectToAction(nameof(Index));
            }
            return View(medico);
        }

        // GET: Medico/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var medico = await _context.Medico.FindAsync(id);
            var medico = await _context.Medico.Where(m => m.idMedico == id)//hacemos una consulta con inner join
            .Include(me => me.MedicoEspecialidad).FirstOrDefaultAsync();

            if (medico == null)
            {
                return NotFound();
            }
            ViewData["ListaEspecialidades"] = new SelectList(_context.Especialidad,"idEspecialidad","descripcion",medico.MedicoEspecialidad[0].idEspecialidad);//el 0 en el array es por que solo obtenemos un registro
            return View(medico);
        }

        // POST: Medico/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idMedico,Nombre,Apellido,Direccion,Telefono,Email,HorarioAtencionDesde,HorarioAtencionHasta")] Medico medico, int idEspecialidad)
        {
            if (id != medico.idMedico)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medico);
                    await _context.SaveChangesAsync();

                    var medicoEspecialidad = await _context.MedicoEspecialidad.FirstOrDefaultAsync(me => me.idMedico == id);
                    
                    _context.Remove(medicoEspecialidad);
                    await _context.SaveChangesAsync();

                    medicoEspecialidad.idEspecialidad = idEspecialidad;
                    
                    _context.Add(medicoEspecialidad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)//trata concurrencias, cuando dos personas modifican el mismo registro
                {
                    if (!MedicoExists(medico.idMedico))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(medico);
        }

        // GET: Medico/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
                //aqui se mostraran los registros especificos por id
                //var medico = await _context.Medico.FirstOrDefaultAsync(m => m.idMedico == id);
                //Hacemos un inner join con un where ya que en vez de obtener todos los registros la condicion es que 
                //se obtenga un registro especificando de la id
                var medico = await _context.Medico
                .Where(m => m.idMedico == id).Include(me => me.MedicoEspecialidad)
                .ThenInclude(e => e.Especialidad).FirstOrDefaultAsync();
               

            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // POST: Medico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {   
            //en la var medicoEs estara guardado el registro que encuentre apartir de la id que el formulario mande por post
            var medicoEspecialidad = await _context.MedicoEspecialidad
            .FirstAsync(me => me.idMedico == id);
            //despues se accede a la base de datos para que esta haga un DELETE y asi elimine el registro
            _context.MedicoEspecialidad.Remove(medicoEspecialidad);
            await _context.SaveChangesAsync();
            //en la var medico , hara lo mismo hara un select y guardara ese registro encontrado apartir de la id
            var medico = await _context.Medico.FindAsync(id);
            //elimina ese registros
            _context.Medico.Remove(medico);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }

        private bool MedicoExists(int id)
        {
            return _context.Medico.Any(e => e.idMedico == id);//retorna true or false, para validar si el medico existe registro id
        }
    }
}
