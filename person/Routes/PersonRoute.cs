using Microsoft.EntityFrameworkCore;
using person.Models;
using Person.Data;
using Person.Models;

namespace person.Routes
{
    public static class PersonRoute
    {
        public static void PersonRoutes(this WebApplication app)
        {
            //app.MapGet(pattern: "person", () => new PersonModel ("João Pedro"));

            // aqui foi criado o prefixo para nao preccisar repetir "person" todas as vezes.
            var route = app.MapGroup(prefix: "person");

            route.MapPost(pattern: "", async (PersonRequest req, PersonContext context) =>
            {
                var person = new PersonModel(req.name);
                await context.AddAsync(person);

                // aqui é ko "commit" para salvar no banco de dados
                await context.SaveChangesAsync();

            });


            // "PersonContext context" é o banco de dados
            route.MapGet("", async (PersonContext context) =>
            {
                var peaple = await context.Peaple.ToListAsync();
                return Results.Ok(peaple);
            });

            route.MapPut("{id:guid}",
                async (Guid id, PersonRequest req, PersonContext context) =>
            {
                var person = await context.Peaple.FindAsync(id);
                if (person == null)

                    return Results.NotFound();


                person.ChangeName(req.name);
                await context.SaveChangesAsync();

                return Results.Ok(person);
            });

            app.MapDelete(pattern:"{id:guid}", async (Guid id, PersonContext context) =>
            {
                var person = await context.Peaple.FindAsync(id);

                if (person == null)
                    return Results.NotFound();

                person.InactiveName();
                await context.SaveChangesAsync();
                return Results.Ok(person);


            });



        }
    }
}


// quando nao se precisa instanciar uma classe, ela deve ser estática **conferir