using System.Data;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Repositories;
using ProjectManagement.Views.Home;


namespace ProjectManagement.Controllers
{
    public class TarefasController : Controller
    {
        private readonly IConfiguration Configuration;
        private SqlDataReader dr;

        private List<Tarefa> tarefas = new List<Tarefa>();
        
        private ITaskRepository _taskRepository;
        
        
        public TarefasController( ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        
  
        
        public IActionResult NovaTarefa(int? id_projeto)
        {
            if (id_projeto == null)
            {
                return NotFound();
            }

            TempData["id"] = id_projeto;
        

            return View();
        }


        public IActionResult RegistoTarefa(string descricao, DateTime data_ini, string preco_hora)
        {
            int id_projeto = Convert.ToInt32(TempData["id"]);
            
            _taskRepository.InsertTaskOnProject(id_projeto, descricao, data_ini, preco_hora);
            
            return View("Menu");
        }
        
        
        public IActionResult VerTarefasPorTerminar(int? id_projeto)
        {
            if (id_projeto == null)
            {
                return NotFound();
            }
            var tarefas = _taskRepository.GetTaskByProject(id_projeto);

            return View(tarefas);
        }

        

        public IActionResult EliminarTarefa(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            int id_tarefa = Convert.ToInt32(id);
            
            _taskRepository.DeleteTask(id_tarefa);
            
            
            return Redirect(HttpContext.Request.Headers["Referer"]);
        }

        public IActionResult TerminarTarefa(int? id_tarefa)
        {
            if (id_tarefa == null)
            {
                return NotFound();
            }
            _taskRepository.FinishTask(id_tarefa);

            return View("Menu");
        }

        public IActionResult VerTarefasEntreDatas()
        {
            return View();
        }


        public IActionResult TarefaEntreDatas(DateTime start, DateTime end)
        {
            
           
            var resultado = _taskRepository.ListTaskBetweenDates(start, end);

            return View(resultado);
        }

    }
}