using College_Web.Data;
using College_DB.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CollegeDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    
builder.Services.AddRazorPages();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CollegeDBContext>();
    db.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.MapGet("students/all", async (CollegeDBContext db) => {
    return Results.Ok(await db.Students.AsNoTracking().ToListAsync());
});

app.MapGet("groups/all", async (CollegeDBContext db) => {
    return Results.Ok(await db.Groups.AsNoTracking().ToListAsync());
});

app.MapGet("group/{id}", (string id) => {
    return Results.Ok();
});

app.MapPost("/students/add", async(Student student, CollegeDBContext db) =>
{
    db.Students.Add(student);
    await db.SaveChangesAsync();
    return Results.Created($"/students/add",
                           new { studentId = student.StudentId });
});

app.MapPost("/groups/add", async (Group group, CollegeDBContext db) =>
{
    db.Groups.Add(group);
    await db.SaveChangesAsync();
    return Results.Created($"/groups/add",
                           new { studentId = group.GroupId });
});

app.MapPut("/students/{id:int}", (int id, Student student) =>
{ 
    student.StudentId = id;

    return Results.NoContent();
});

app.MapDelete("/students/{id:int}", (int id) =>
{
    return Results.NoContent();
});



app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
