using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjectManagement.Controllers;
using ProjectManagement.Repositories;

namespace ProjectManagement.Test;

public class TaskControllerTest
{
    private TarefasController _tarefasController;
    private Mock<ITaskRepository> _mockRepository;

    public TaskControllerTest()
    {
        _mockRepository = new Mock<ITaskRepository>();
        _tarefasController = new TarefasController(_mockRepository.Object);
    }
    
    [Fact]
    public void NewTask_IsNull_ReturnNotFound()
    {
        var result = _tarefasController.NovaTarefa(null);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void ToFinishTasks_IsNull_ReturnNotFound()
    {
        var result = _tarefasController.VerTarefasPorTerminar(null);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void DeleteTask_IsNull_ReturnNotFound()
    {
        var result = _tarefasController.EliminarTarefa(null);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void FinishTask_IsNull_ReturnNotFound()
    {
        var result = _tarefasController.TerminarTarefa(null);
        Assert.IsType<NotFoundResult>(result);
    }

  

}