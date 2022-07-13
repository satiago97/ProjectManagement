using Microsoft.Data.SqlClient;
using ProjectManagement.Models;

namespace ProjectManagement.Repositories;

public class TaskRepository : ITaskRepository
{
    private SqlDataReader dr;


    public List<Tarefa> GetTaskByProject(int? id_projeto)
    {
        
            string connectionString= "Server=DESKTOP-NFP0P2O; Database=ProjectManagementDB; Trusted_Connection=True; MultipleActiveResultSets=true; Encrypt=False";
            SqlConnection connection = new SqlConnection(connectionString);
            
            connection.Open();
            SqlCommand sqlCommand = connection.CreateCommand();

            sqlCommand.CommandText = $"SELECT id_tarefa, descricao, preco_hora, data_hora_ini, data_hora_fim  FROM Tarefa where id_projeto='{id_projeto}' and id_estado = 1";

            dr = sqlCommand.ExecuteReader();
            List<Tarefa> tarefas = new List<Tarefa>();
            while (dr.Read())
            {
                Tarefa t = new Tarefa();
                t.IdTarefa = Convert.ToInt32(dr["id_tarefa"]);
                t.Descricao = dr["descricao"].ToString();
                t.PrecoHora = Convert.ToDouble(dr["preco_hora"]);
                t.DataHoraIni = Convert.ToDateTime(dr["data_hora_ini"]);
                tarefas.Add(t);
            }
            connection.Close();


            return tarefas;
    }


    public void InsertTaskOnProject(int id_projeto, string descricao, DateTime data_ini, string preco_hora)
    {
        string connectionString= "Server=DESKTOP-NFP0P2O; Database=ProjectManagementDB; Trusted_Connection=True; MultipleActiveResultSets=true; Encrypt=False";

        SqlConnection connection = new SqlConnection(connectionString);
            
        connection.Open();
        SqlCommand sqlCommand = connection.CreateCommand();
        
        var data_inicial = Convert.ToDateTime(data_ini);
            
        sqlCommand.CommandText =
            $"INSERT INTO Tarefa(descricao, data_hora_ini, preco_hora, id_estado, id_projeto) values  ('{descricao}', '{data_inicial}', '{preco_hora}', 1, '{id_projeto}')";

        var result = sqlCommand.ExecuteNonQuery();
        
        connection.Close();

    }

    public void DeleteTask(int id_tarefa)
    {
        
        string connectionString= "Server=DESKTOP-NFP0P2O; Database=ProjectManagementDB; Trusted_Connection=True; MultipleActiveResultSets=true; Encrypt=False";
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        SqlCommand sqlCommand = connection.CreateCommand();
        
        sqlCommand.CommandText = $"DELETE FROM Tarefa where id_tarefa='{id_tarefa}'";

        var result = sqlCommand.ExecuteNonQuery();
        connection.Close();
    }

    public void FinishTask(int? id_tarefa)
    {
        string connectionString= "Server=DESKTOP-NFP0P2O; Database=ProjectManagementDB; Trusted_Connection=True; MultipleActiveResultSets=true; Encrypt=False";
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        SqlCommand sqlCommand = connection.CreateCommand();
        SqlCommand sqlCommand2 = connection.CreateCommand();
        string format = "yyyy-MM-dd HH:mm:ss";
     

        sqlCommand2.CommandText = $"SELECT data_hora_ini from Tarefa where id_tarefa='{id_tarefa}'";
        dr = sqlCommand2.ExecuteReader();

        if (dr.Read())
        {
            DateTime initialTime = Convert.ToDateTime(dr["data_hora_ini"]);
            DateTime finishTime = DateTime.UtcNow;
            int result = DateTime.Compare(initialTime, finishTime);
            if (result < 0)
            {
                sqlCommand.CommandText = $"UPDATE Tarefa SET data_hora_fim = '{finishTime.ToString(format)}', id_estado = 2 where id_tarefa= '{id_tarefa}'";
                sqlCommand.ExecuteNonQuery();
            }
        }
        
        connection.Close();
    }

    public List<Tarefa> ListTaskBetweenDates(DateTime start, DateTime end)
    {
        string connectionString= "Server=DESKTOP-NFP0P2O; Database=ProjectManagementDB; Trusted_Connection=True; MultipleActiveResultSets=true; Encrypt=False";
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        SqlCommand sqlCommand = connection.CreateCommand();
        string format = "yyyy-MM-dd HH:mm:ss";



        sqlCommand.CommandText = $"SELECT id_tarefa, descricao, preco_hora, data_hora_ini, data_hora_fim FROM Tarefa where data_hora_fim between '{start.ToString(format)}' and '{end.ToString(format)}' and id_estado=2";
        dr = sqlCommand.ExecuteReader();
        List<Tarefa> tarefas = new List<Tarefa>();
        while (dr.Read())
        {
            Tarefa t = new Tarefa();
            t.IdTarefa = Convert.ToInt32(dr["id_tarefa"]);
            t.Descricao = dr["descricao"].ToString();
            t.PrecoHora = Convert.ToDouble(dr["preco_hora"]);
            t.DataHoraIni = Convert.ToDateTime(dr["data_hora_ini"]);
            t.DataHoraFim = Convert.ToDateTime(dr["data_hora_fim"]);
            tarefas.Add(t);
        }
        
        connection.Close();
        
        return tarefas;
    }

}