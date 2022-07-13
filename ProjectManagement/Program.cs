using Microsoft.Build.Framework;
using ProjectManagement.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "EditarConta",
    pattern: "{controller=Home}/{action=EditarConta}"
);
app.MapControllerRoute(
    name: "NovoProjeto",
    pattern: "{controller=Project}/{action=NovoProjeto}"
);
app.MapControllerRoute(
    name: "NovaTarefa",
    pattern: "{controller=Tarefas}/{action=NovaTarefa}/{id_projeto}");
app.MapControllerRoute(
    name: "GetIdParaEliminar",
    pattern: "{controller=Project}/{action=GetIdParaEliminar}/{id}");
app.MapControllerRoute(
    name: "VerTarefasPorTerminar",
    pattern: "{controller=Tarefas}/{action=VerTarefasPorTerminar}");
app.MapControllerRoute(
    name: "TerminarTarefa",
    pattern: "{controller=Tarefas}/{action=TerminarTarefa}/{id_tarefa}");
app.MapControllerRoute(
    name: "ListarUtilizadores",
    pattern: "{controller=Project}/{action=ListarUtilizadores}/{id_projeto}");
app.MapControllerRoute(
    name: "ConvidarUtilizador",
    pattern: "{controller=Project}/{action=ConvidarUtilizador}/{id_utilizador}");
app.MapControllerRoute(
    name: "AceitarConvite",
    pattern: "{controller=Project}/{action=AceitarConvite}/{id_projeto}");
app.MapControllerRoute(
    name: "RecusarConvite",
    pattern: "{controller=Project}/{action=RecusarConvite}/{id_projeto}");
app.MapControllerRoute(
    name: "ListarUtilizadoresNoProjeto",
    pattern: "{controller=Project}/{action=ListarUtilizadoresNoProjeto}/{id_projeto}");
app.MapControllerRoute(
    name: "EliminarUtilizadorDoProjeto",
    pattern: "{controller=Project}/{action=EliminarUtilizadorDoProjeto}/{id_utilizador}");

app.Run();
