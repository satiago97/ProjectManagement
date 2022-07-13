using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Repositories;

namespace ProjectManagement.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IConfiguration Configuration;
        private SqlDataReader dr;

        private IProjectRepository _projectRepository;
        public ProjectController( IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        
        public IActionResult NovoProjeto()
        {
           
            var idUtilizadorAtual = Convert.ToInt32(TempData["id_user"]);
            TempData.Keep("id_user");
            ViewData["id_userAtual"] = idUtilizadorAtual;
            
            var projects = _projectRepository.GetAllProjectsByUser(idUtilizadorAtual);
            
            return View(projects);
        }


        
        public IActionResult RegistoProjeto(string nome, string nome_cliente, string preco)
        {
            var id_utilizador = Convert.ToInt32(TempData["id_user"]);
            TempData.Keep("id_user");
            _projectRepository.InsertProject(nome, nome_cliente, preco);
            _projectRepository.InsertUtilizadorProj(id_utilizador);

            return Redirect(HttpContext.Request.Headers["Referer"]);
        }
        
        public IActionResult GetIdParaEliminar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            TempData["id_projeto"] = id;

            return View();
        }

        public IActionResult EliminarProjetoComTarefas(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            int id_projeto = Convert.ToInt32(TempData["id_projeto"]);
            
     
            
            _projectRepository.DeleteProjectWithTasks(id_projeto);


            return View("Menu");
        }

        public IActionResult EliminarProjetoSemTarefas(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            int id_projeto = Convert.ToInt32(TempData["id_projeto"]);

            _projectRepository.DeleteProjectDisassociateTasks(id_projeto);

            return View("Menu");
        }

        public IActionResult ListarUtilizadores(int? id_projeto)
        {
            if (id_projeto == null)
            {
                return NotFound();
            }
            TempData["idProjetoAdicionarUser"] = id_projeto;
            TempData.Keep("idProjetoAdicionarUser");
            
            var utilizadores = _projectRepository.ListAllUsers();
            
            return View(utilizadores);
        }

        public IActionResult ConvidarUtilizador(int id_utilizador)
        {
            int id_projeto = Convert.ToInt32(TempData["idProjetoAdicionarUser"]);
            
            _projectRepository.InviteUser(id_projeto, id_utilizador);
            
            return View("Menu");
        }

        public IActionResult ListarConvites()
        {
            var id_utilizador = Convert.ToInt32(TempData["id_user"]);
            TempData.Keep("id_user");

            var projetosConvidados = _projectRepository.ListInvitedProjects(id_utilizador);

            return View(projetosConvidados);

        }

        public IActionResult AceitarConvite(int id_projeto)
        {
            var id_utilizador = Convert.ToInt32(TempData["id_user"]);
            TempData.Keep("id_user");
            
            _projectRepository.AcceptInvite(id_projeto, id_utilizador);

            return View("Menu");
        }

        public IActionResult RecusarConvite(int id_projeto)
        {
            var id_utilizador = Convert.ToInt32(TempData["id_user"]);
            TempData.Keep("id_user");
            
            _projectRepository.RefuseInvite(id_projeto, id_utilizador);

            return View("Menu");

        }

        public IActionResult ListarUtilizadoresNoProjeto(int id_projeto)
        {
            TempData["id_projetoParaRemover"] = id_projeto;
            var utilizadores = _projectRepository.ListUsersInProject(id_projeto);

            return View(utilizadores);
        }

        public IActionResult EliminarUtilizadorDoProjeto(int id_utilizador)
        {
            var id_projeto = Convert.ToInt32(TempData["id_projetoParaRemover"]);
            
            _projectRepository.DeleteUserFromProject(id_projeto, id_utilizador);

            return View("Menu");
        }
        
        
        
        
    }
}