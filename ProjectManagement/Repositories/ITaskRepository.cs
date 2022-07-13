using ProjectManagement.Models;

namespace ProjectManagement.Repositories;

public interface ITaskRepository
{
    List<Tarefa> GetTaskByProject(int? id_projeto);
    void InsertTaskOnProject(int id_projeto, string descricao, DateTime data_ini, string preco_hora);

    void DeleteTask(int id_tarefa);

    void FinishTask(int? id_tarefa);

    List<Tarefa> ListTaskBetweenDates(DateTime start, DateTime end);

}