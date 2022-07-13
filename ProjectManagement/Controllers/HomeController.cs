using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagement.Controllers;

public class HomeController : Controller
{
  
    private readonly IConfiguration Configuration;
    private SqlDataReader dr;
    private static int _id_utilizadorAtual;
    private List<Utilizador> userAtual = new List<Utilizador>();
    



    public HomeController(IConfiguration _configuration)
    {
        Configuration = _configuration;
    }
    

    public IActionResult Index()
    {
        
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Logar(string username, string password)
    {
        string connectionstring = Configuration.GetConnectionString("DefaultConnectionString");
        SqlConnection connection = new SqlConnection(connectionstring);

        await connection.OpenAsync();

        SqlCommand sqlCommand = connection.CreateCommand();
        sqlCommand.CommandText = $"SELECT id_utilizador, username, nome FROM Utilizador where username='{username}' and password = '{password}'";
        dr = sqlCommand.ExecuteReader();
        if (dr.Read())
        {
            _id_utilizadorAtual = Convert.ToInt32(dr["id_utilizador"]);
            TempData["id_user"] = _id_utilizadorAtual;
            ViewData["id_utilizador"] = _id_utilizadorAtual;
            connection.Close();
            return View("Menu");
            
        }
        connection.Close();
        return Json(new { Msg="Login sem sucesso" });
    }

    public IActionResult Registo()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Registo(string username, string nome, string password)
    {
        string connectionstring = Configuration.GetConnectionString("DefaultConnectionString");
        SqlConnection connection = new SqlConnection(connectionstring);

        await connection.OpenAsync();
        SqlCommand sqlCommand = connection.CreateCommand();
        sqlCommand.CommandText =
            $"INSERT INTO Utilizador(username, nome, password, id_horario) values ('{username}', '{nome}', '{password}', 1)";

       var result = sqlCommand.ExecuteNonQuery();

       if (result == 0)
       {
           return Json(new { Msg="Registo sem sucesso" }); 
       }
       
       connection.Close();

        return View("Index"); 
    }


    private void FetchData()
    {
        string connectionString = Configuration.GetConnectionString("DefaultConnectionString");
        SqlConnection connection = new SqlConnection(connectionString);

        connection.Open();
        SqlCommand sqlCommand = connection.CreateCommand();
        sqlCommand.CommandText = $"SELECT nome, username, password from Utilizador where id_utilizador='{_id_utilizadorAtual}'";
        dr = sqlCommand.ExecuteReader();

        while (dr.Read())
        {
           userAtual.Add(new Utilizador()
               {Nome = dr["nome"].ToString(), Username = dr["username"].ToString(),
                   Password = dr["password"].ToString() });
        }
        
        connection.Close();
    }

    public IActionResult EditarConta()
    {
        
        FetchData();

        return View(userAtual);
    }

    public IActionResult Editar(string username, string nome, string password)
    {
        string connectionString = Configuration.GetConnectionString("DefaultConnectionString");
        SqlConnection connection = new SqlConnection(connectionString);
        
        connection.Open();
        SqlCommand sqlCommand = connection.CreateCommand();
        sqlCommand.CommandText =
            $"UPDATE Utilizador SET nome ='{nome}', username ='{username}', password='{password}' WHERE id_utilizador='{_id_utilizadorAtual}'";

        var result = sqlCommand.ExecuteNonQuery();

        if (result == 0)
        {
            return Json(new { Msg = "Edit sem sucesso" });
        }

        return View("Menu");
    }
    
  
    
    
    
    
    public IActionResult Privacy()
    {

        return View();
    }

    public IActionResult Menu()
    {
        return View();
    }

 
}