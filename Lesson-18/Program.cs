using College_DB.Data;
using College_DB.Models;

var builder = WebApplication.CreateBuilder(args);

string connection_string = "Data Source=COMP11A1\\SQLEXPRESS;Initial Catalog=College_DB;Integrated Security=True;TrustServerCertificate=True;";
StudentRepository stud_repo = new StudentRepository(connection_string);
GroupRepository group_repo = new GroupRepository(connection_string);
builder.Services.AddRazorPages();
builder.Services.AddSingleton(stud_repo);
builder.Services.AddSingleton(group_repo);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.MapGet("students/all", () => {
    var students = stud_repo.GetAllStudents();
    return Results.Ok(students);
});

app.MapGet("groups/all", () => {
    var groups = group_repo.GetAllGroups();
    return Results.Ok(groups);
});

app.MapGet("group/{id}", (string id) => {
    var group = group_repo.GetGroupById(id);
    return Results.Ok(group);
});

app.MapPost("/students/add", (Student student) =>
{
    int id = stud_repo.CreateStudent(student);
    return Results.Ok(id);
});

app.MapPut("/students/{id:int}", (int id, Student student) =>
{ 
    Console.WriteLine(id);
    student.StudentId = id;
    Console.WriteLine(student.StudentId);
    stud_repo.UpdateStudent(id, student);
    return Results.NoContent();
});

app.MapDelete("/students/{id:int}", (int id) =>
{
    stud_repo.DeleteStudent(id);
    return Results.NoContent();
});



app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
