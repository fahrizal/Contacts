using Contacts.WebApi;
using Contacts.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(option =>
    option.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

//app.MapGet("/api/contacts", async (ApplicationDbContext dbContext) =>
//{
//    var contacts = await dbContext.Contacts.ToListAsync();
//    return Results.Ok(contacts);
//});

app.MapGet("/api/contacts", async ([FromQuery] string? s, ApplicationDbContext db) =>
{
    List<Contact> contacts;

    if (string.IsNullOrWhiteSpace(s))
        contacts = await db.Contacts.ToListAsync();
    else
        contacts = await db.Contacts.Where(x =>
            !string.IsNullOrWhiteSpace(x.Name) && x.Name.ToLower().IndexOf(s.ToLower()) >= 0 ||
            !string.IsNullOrWhiteSpace(x.Email) && x.Email.ToLower().IndexOf(s.ToLower()) >= 0 ||
            !string.IsNullOrWhiteSpace(x.Address) && x.Address.ToLower().IndexOf(s.ToLower()) >= 0 ||
            !string.IsNullOrWhiteSpace(x.Phone) && x.Phone.ToLower().IndexOf(s.ToLower()) >= 0).ToListAsync();

    return Results.Ok(contacts);
});

app.MapGet("/api/contacts/{id}", async (ApplicationDbContext dbContext, int id) =>
{
    var contact = await dbContext.Contacts.FindAsync(id);

    if (contact == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(contact);
});

app.MapPost("/api/contacts", async (ApplicationDbContext dbContext, Contact contact) =>
{
    await dbContext.Contacts.AddAsync(contact);
    await dbContext.SaveChangesAsync();
    return Results.Ok(contact);
});

app.MapPut("/api/contacts/{id}", async (ApplicationDbContext dbContext, int id, Contact contact) =>
{
    // This is the original code from the copilot
    //if (id != contact.ContactId)
    //{
    //    return Results.BadRequest();
    //}

    //dbContext.Entry(contact).State = EntityState.Modified;
    //await dbContext.SaveChangesAsync();

    //return Results.Ok(contact);

    var contactToUpdate = await dbContext.Contacts.FindAsync(id);

    if (contactToUpdate == null)
    {
        return Results.NotFound();
    }

    contactToUpdate.Name = contact.Name;
    contactToUpdate.Email = contact.Email;
    contactToUpdate.Phone = contact.Phone;
    contactToUpdate.Address = contact.Address;

    await dbContext.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/api/contacts/{id}", async (ApplicationDbContext dbContext, int id) =>
{
    var contactToDelete = await dbContext.Contacts.FindAsync(id);

    if (contactToDelete == null)
    {
        return Results.NotFound();
    }

    dbContext.Contacts.Remove(contactToDelete);
    await dbContext.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();
