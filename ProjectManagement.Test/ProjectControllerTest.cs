using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjectManagement.Models;
using ProjectManagement.Repositories;

namespace ProjectManagement.Test;
using Xunit;
using ProjectManagement.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;

public class ProjectControllerTest
{
    private ProjectController _projectController;
    private Mock<IProjectRepository> _mockRepository;


    public ProjectControllerTest()
    {
        _mockRepository = new Mock<IProjectRepository>();
        _projectController = new ProjectController(_mockRepository.Object);
    }
    
    [Fact]
    public void Delete_IsNull_ReturnNotFound()
    {
        var result = _projectController.EliminarProjetoComTarefas(null);
        Assert.IsType<NotFoundResult>(result);
    }
    
    [Fact]
    public void Delete_Without_Tasks_IsNull_ReturnNotFound()
    {
        var result = _projectController.EliminarProjetoSemTarefas(null);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void List_User_IsNull_ReturnNotFound()
    {
        var result = _projectController.ListarUtilizadores(null);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Get_Id_IsNull_ReturnNotFound()
    {
        var result = _projectController.GetIdParaEliminar(null);
        Assert.IsType<NotFoundResult>(result);
    }

    
   
    
    
    
    
}