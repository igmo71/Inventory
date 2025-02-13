using Inventory.Data;
using Inventory.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
namespace Inventory.WebApi;

public static class EquipmentEndpoints
{
    public static void MapEquipmentEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Equipment").WithTags(nameof(Equipment));

        group.MapGet("/", async (ApplicationDbContext db) =>
        {
            var result = await db.Equipment
            //.AsNoTracking()
            //.Select(e => new { e.Id, e.Name, e.IsFolder, e.ParentId })
            .ToListAsync();

            var entries = db.ChangeTracker.Entries();

            return result;
        })
        .WithName("GetAllEquipment")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Equipment>, NotFound>> (string id, ApplicationDbContext db) =>
        {
            return await db.Equipment.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Equipment model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetEquipmentById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (string id, Equipment equipment, ApplicationDbContext db) =>
        {
            var affected = await db.Equipment
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, equipment.Id)
                    .SetProperty(m => m.Name, equipment.Name)
                    .SetProperty(m => m.IsFolder, equipment.IsFolder)
                    .SetProperty(m => m.ParentId, equipment.ParentId)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateEquipment")
        .WithOpenApi();

        group.MapPost("/", async (Equipment equipment, ApplicationDbContext db) =>
        {
            db.Equipment.Add(equipment);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Equipment/{equipment.Id}",equipment);
        })
        .WithName("CreateEquipment")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (string id, ApplicationDbContext db) =>
        {
            var affected = await db.Equipment
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteEquipment")
        .WithOpenApi();
    }
}
