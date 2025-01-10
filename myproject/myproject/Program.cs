using Microsoft.EntityFrameworkCore;
using myproject.Data;
using myproject.Models;

var builder = WebApplication.CreateBuilder(args);

// Adicionando o serviço do DbContext com a configuração do PostgreSQL
builder.Services.AddDbContext<Context>(op =>
op.UseNpgsql(builder.Configuration.GetConnectionString("myprojectDatabase")));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Endpoints da API
app.MapGet("/persons", async (Context db) => await db.persons.ToListAsync());

app.MapGet("/persons/{id}", async (int id, Context db) =>
    await db.persons.FindAsync(id)
    is Person person
    ? Results.Ok(person)
    : Results.NotFound());

app.MapPost("/persons/{id}", async (Person person, Context db) =>
{
    db.persons.Add(person);
    await db.SaveChangesAsync();
    return Results.Created($"/persons/{person.Id}", person);
});

app.MapPut("/persons/{id}", async (int id, Person personAtualizado, Context db) =>
{
    var person = await db.persons.FindAsync(id);
    if (person is null)
    {
        return Results.NotFound();
    }

    person.Id = personAtualizado.Id; 
    person.Name = personAtualizado.Name;
    person.Lastname = personAtualizado.Lastname;
    person.Mail = personAtualizado.Mail;
    person.Phone = personAtualizado.Phone;  

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/persons/{id}", async (int id, Context db) =>
{
    var person = await db.persons.FindAsync(id);
    if (person is not null)
    {
        db.persons.Remove(person);
        await db.SaveChangesAsync();
        return Results.Ok();
    }

    return Results.NotFound();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
