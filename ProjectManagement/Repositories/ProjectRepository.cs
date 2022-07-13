using System.Configuration;
using Microsoft.Data.SqlClient;
using ProjectManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.Repositories;

public class ProjectRepository : IProjectRepository
{
    private SqlDataReader dr;

    public List<Projeto> GetAllProjectsByUser(int idUtilizador)
    {
        string connectionString =
            "Server=DESKTOP-NFP0P2O; Database=ProjectManagementDB; Trusted_Connection=True; MultipleActiveResultSets=true; Encrypt=False";
        SqlConnection connection = new SqlConnection(connectionString);

        connection.Open();
        SqlCommand sqlCommand = connection.CreateCommand();
        sqlCommand.CommandText =
            $"SELECT Projeto.id_projeto, Projeto.nome, Projeto.nome_cliente, Projeto.preco FROM Projeto Inner Join UtilizadorProj ON Projeto.id_projeto = UtilizadorProj.id_projeto WHERE UtilizadorProj.id_utilizador = '{idUtilizador}'";

        dr = sqlCommand.ExecuteReader();
        List<Projeto> projetos = new List<Projeto>();

        while (dr.Read())
        {
            Projeto p = new Projeto();
            p.IdProjeto = Convert.ToInt32(dr["id_projeto"]);
            p.Nome = dr["nome"].ToString();
            p.NomeCliente = dr["nome_cliente"].ToString();
            p.Preco = dr["preco"].ToString();
            projetos.Add(p);

        }

        connection.Close();

        return projetos;
    }
    public List<Utilizador> ListAllUsers()
    {
        string connectionString =
            "Server=DESKTOP-NFP0P2O; Database=ProjectManagementDB; Trusted_Connection=True; MultipleActiveResultSets=true; Encrypt=False";
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();

        SqlCommand sqlCommand = connection.CreateCommand();
        sqlCommand.CommandText = $"SELECT id_utilizador, nome, username FROM Utilizador";
        dr = sqlCommand.ExecuteReader();
        List<Utilizador> utilizadores = new List<Utilizador>();

        while (dr.Read())
        {
            Utilizador u = new Utilizador();
            u.IdUtilizador = Convert.ToInt32(dr["id_utilizador"]);
            u.Nome = dr["nome"].ToString();
            u.Username = dr["username"].ToString();
            utilizadores.Add(u);
        }
        connection.Close();

        return utilizadores;

    }
    
    public List<Projeto> ListInvitedProjects(int id_utilizador)
    {
        string connectionString =
            "Server=DESKTOP-NFP0P2O; Database=ProjectManagementDB; Trusted_Connection=True; MultipleActiveResultSets=true; Encrypt=False";
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        SqlCommand sqlCommand = connection.CreateCommand();

        sqlCommand.CommandText =
            $"SELECT p.id_projeto, p.nome_cliente, p.nome, p.preco from Projeto p, UtilizadorProj u where p.id_projeto = u.id_projeto and tipo = 2 and u.id_utilizador = '{id_utilizador}'";
        dr = sqlCommand.ExecuteReader();
        List<Projeto> projetosConvidados = new List<Projeto>();

        while (dr.Read())
        {
            Projeto p = new Projeto();
            p.IdProjeto = Convert.ToInt32(dr["id_projeto"]);
            p.NomeCliente = dr["nome_cliente"].ToString();
            p.Nome = dr["nome"].ToString();
            p.Preco = dr["preco"].ToString();
            projetosConvidados.Add(p);
        }
        connection.Close();
        return projetosConvidados;
    }

    public void InsertProject(string nome, string nome_cliente, string preco)
    {
        string connectionString =
            "Server=DESKTOP-NFP0P2O; Database=ProjectManagementDB; Trusted_Connection=True; MultipleActiveResultSets=true; Encrypt=False";
        SqlConnection connection = new SqlConnection(connectionString);

        connection.Open();
        SqlCommand sqlCommand = connection.CreateCommand();
        sqlCommand.CommandText =
            $"INSERT INTO Projeto(nome_cliente, nome, preco) values ('{nome_cliente}', '{nome}', '{preco}')";

        var result = sqlCommand.ExecuteNonQuery();

        connection.Close();

    }

    public void InsertUtilizadorProj(int id_utilizador)
    {
        string connectionString =
            "Server=DESKTOP-NFP0P2O; Database=ProjectManagementDB; Trusted_Connection=True; MultipleActiveResultSets=true; Encrypt=False";
        SqlConnection connection = new SqlConnection(connectionString);
       
        connection.Open();
        SqlCommand sqlCommand = connection.CreateCommand();
        sqlCommand.CommandText =
            $"INSERT INTO UtilizadorProj (id_projeto, id_utilizador, tipo) select max(Projeto.id_projeto), '{id_utilizador}', 0 FROM  Projeto";

        sqlCommand.ExecuteNonQuery();
        
        connection.Close();
    }

    public void DeleteProjectWithTasks(int? id_projeto)
    {
  
        string connectionString =
            "Server=DESKTOP-NFP0P2O; Database=ProjectManagementDB; Trusted_Connection=True; MultipleActiveResultSets=true; Encrypt=False";
        SqlConnection connection = new SqlConnection(connectionString);
            
        connection.Open();
        SqlCommand sqlCommand = connection.CreateCommand();
        SqlCommand sqlCommand2 = connection.CreateCommand();
        SqlCommand sqlCommand3 = connection.CreateCommand();
        SqlCommand sqlCommand4 = connection.CreateCommand();
        
        sqlCommand4.CommandText =
            $"DELETE FROM UtilizadorProj WHERE id_projeto='{id_projeto}'";

        sqlCommand4.ExecuteNonQuery();

        sqlCommand3.CommandText = $"SELECT * FROM Tarefa where  id_projeto='{id_projeto}'";

        var result3 = sqlCommand3.ExecuteNonQuery();

        if (result3 != 0)
        {
            sqlCommand.CommandText = $"DELETE FROM Tarefa where id_projeto='{id_projeto}'";
            sqlCommand.ExecuteNonQuery();
               
        }
            
        sqlCommand2.CommandText = $"DELETE FROM Projeto where id_projeto='{id_projeto}'";
        sqlCommand2.ExecuteNonQuery();
        
        connection.Close();
    }


    public void DeleteProjectDisassociateTasks(int id_projeto)
    {
        string connectionString =
            "Server=DESKTOP-NFP0P2O; Database=ProjectManagementDB; Trusted_Connection=True; MultipleActiveResultSets=true; Encrypt=False";
        
        SqlConnection connection = new SqlConnection(connectionString);
            
            
        connection.Open();
        SqlCommand sqlCommand = connection.CreateCommand();
        SqlCommand sqlCommand2 = connection.CreateCommand();
        SqlCommand sqlCommand3 = connection.CreateCommand();
        SqlCommand sqlCommand4 = connection.CreateCommand();

        sqlCommand.CommandText = $"SELECT * FROM Tarefa where id_projeto='{id_projeto}'";
        
        sqlCommand2.CommandText =
            $"DELETE FROM UtilizadorProj WHERE id_projeto='{id_projeto}'";
        
        sqlCommand2.ExecuteNonQuery();

        var result = sqlCommand.ExecuteNonQuery();
        
        if (result != 0)
        {
            sqlCommand3.CommandText= $"UPDATE Tarefa set id_projeto = null where id_projeto = '{id_projeto}'";
            sqlCommand3.ExecuteNonQuery();
        }

        sqlCommand4.CommandText = $"DELETE FROM Projeto WHERE id_projeto='{id_projeto}'";
         sqlCommand4.ExecuteNonQuery();
         
         connection.Close();
    }

    public void InviteUser(int id_projeto, int id_utilizadorConvidado)
    {
        string connectionString =
            "Server=DESKTOP-NFP0P2O; Database=ProjectManagementDB; Trusted_Connection=True; MultipleActiveResultSets=true; Encrypt=False";

        SqlConnection connection = new SqlConnection(connectionString);
        
        connection.Open();
        SqlCommand sqlCommand = connection.CreateCommand();

        sqlCommand.CommandText =
            $"INSERT INTO UtilizadorProj(id_utilizador, id_projeto, tipo) values('{id_utilizadorConvidado}','{id_projeto}', 2)";

        sqlCommand.ExecuteNonQuery();
        
        
        connection.Close();
    }

    public void AcceptInvite(int id_projeto, int id_utilizadorConvidado)
    {
        string connectionString =
            "Server=DESKTOP-NFP0P2O; Database=ProjectManagementDB; Trusted_Connection=True; MultipleActiveResultSets=true; Encrypt=False";

        SqlConnection connection = new SqlConnection(connectionString);
        
        connection.Open();
        SqlCommand sqlCommand = connection.CreateCommand();

        sqlCommand.CommandText =
            $"UPDATE  UtilizadorProj SET tipo = 1 where id_utilizador='{id_utilizadorConvidado}' and id_projeto = '{id_projeto}'";

        sqlCommand.ExecuteNonQuery();
        
        connection.Close();

    }

    public void RefuseInvite(int id_projeto, int id_utilizadorConvidado)
    {
        string connectionString =
            "Server=DESKTOP-NFP0P2O; Database=ProjectManagementDB; Trusted_Connection=True; MultipleActiveResultSets=true; Encrypt=False";

        SqlConnection connection = new SqlConnection(connectionString);
        
        connection.Open();
        SqlCommand sqlCommand = connection.CreateCommand();

        sqlCommand.CommandText =
            $"DELETE FROM UtilizadorProj where id_utilizador='{id_utilizadorConvidado}' and id_projeto = '{id_projeto}'";

        sqlCommand.ExecuteNonQuery();
        
        connection.Close();

    }

    public List<Utilizador> ListUsersInProject(int id_projeto)
    {
        string connectionString =
            "Server=DESKTOP-NFP0P2O; Database=ProjectManagementDB; Trusted_Connection=True; MultipleActiveResultSets=true; Encrypt=False";

        SqlConnection connection = new SqlConnection(connectionString);
        
        connection.Open();
        SqlCommand sqlCommand = connection.CreateCommand();

        sqlCommand.CommandText =
            $"SELECT u.id_utilizador, u.nome, u.username from Utilizador u, UtilizadorProj up WHERE u.id_utilizador = up.id_utilizador and up.id_projeto = '{id_projeto}' and (tipo= 1 or tipo = 0)";

        dr = sqlCommand.ExecuteReader();
        List<Utilizador> utilizadores = new List<Utilizador>();
        while (dr.Read())
        {
            Utilizador u = new Utilizador();
            u.IdUtilizador = Convert.ToInt32(dr["id_utilizador"]);
            u.Nome = dr["nome"].ToString();
            u.Username = dr["username"].ToString();
            utilizadores.Add(u);
        }
        
        connection.Close();

        return utilizadores;
    }

    public void DeleteUserFromProject(int id_projeto, int id_utilizador)
    {
        string connectionString =
            "Server=DESKTOP-NFP0P2O; Database=ProjectManagementDB; Trusted_Connection=True; MultipleActiveResultSets=true; Encrypt=False";

        SqlConnection connection = new SqlConnection(connectionString);
        
        connection.Open();
        SqlCommand sqlCommand = connection.CreateCommand();

        sqlCommand.CommandText =
            $"DELETE FROM UtilizadorProj WHERE id_projeto='{id_projeto}' and id_utilizador='{id_utilizador}'";

        sqlCommand.ExecuteNonQuery();
        
        connection.Close();


    }






}

   