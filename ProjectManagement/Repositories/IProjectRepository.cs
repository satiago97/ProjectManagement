using ProjectManagement.Models;

namespace ProjectManagement.Repositories;

public interface IProjectRepository
{
     List<Projeto> GetAllProjectsByUser(int idUtilizador);

     void InsertProject(string nome, string nome_cliente, string preco);

     void InsertUtilizadorProj(int id_utilizador);

     void DeleteProjectWithTasks(int? id_projeto);

     void DeleteProjectDisassociateTasks(int id_projeto);

     List<Utilizador> ListAllUsers();

     void InviteUser(int id_projeto, int utilizadorConvidado);

     List<Projeto> ListInvitedProjects(int id_utilizador);

     void AcceptInvite(int id_projeto, int id_utilizadorConvidado);

     void RefuseInvite(int id_projeto, int id_utilizadorConvidado);

     List<Utilizador> ListUsersInProject(int id_projeto);

     void DeleteUserFromProject(int id_projeto, int id_utilizador);
}