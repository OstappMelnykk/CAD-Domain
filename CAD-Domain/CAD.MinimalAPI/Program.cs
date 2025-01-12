using System.Diagnostics;
using CAD.Domain.ComplexFigures;
using Microsoft.AspNetCore.Mvc;

namespace CAD.MinimalAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();
            });
        });
        
        builder.Services.AddAuthorization();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSingleton<SuperCubeService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
    
            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/")
                {
                    context.Response.Redirect("/swagger/index.html");
                    return;
                }
                await next();
            });
        }

        app.UseHttpsRedirection();
        
        app.UseCors();
        
        app.UseAuthorization();


        app.MapGet("/", () => "Hello World!");
        
        app.MapPost("/Devide", ([FromBody] DevideDTO devideDto, [FromServices] SuperCubeService scs) =>
        {
            var stopwatch = Stopwatch.StartNew();
            scs.DivideCubeByAxes(devideDto.x, devideDto.y, devideDto.z);
            stopwatch.Stop();

            return Results.Ok(new 
            { 
                devideDto.x, 
                devideDto.y, 
                devideDto.z, 
                executionTime = stopwatch.Elapsed.TotalSeconds  
            });
        });

        app.MapGet("/GetPoints", (SuperCubeService scs) => 
        {
            return Results.Ok(new 
            { 
                Count = scs.GetPoints().Count, 
                Points = scs.GetPoints()
            });
        });
        
        app.MapGet("/GetPairsOfIndices", (SuperCubeService scs) =>
        {
           List<int[]> indixes = 
                           scs.PairsToConnect_asListIndices.Select(pair => 
                                           new int[2] { pair.Item1, pair.Item2 }).ToList();
            return Results.Ok(new 
            { 
                Count = indixes.Count, 
                Indices = indixes 
            });
        });
        
        
        app.MapGet("/GetPolygons", (SuperCubeService scs) => 
        {
            List<int[]> poligons =
                            scs.Polygons_asListIndices.Select(pair =>
                                            new int[3] { pair.Item1, pair.Item2, pair.Item3 }).ToList();
            return Results.Ok(new 
            { 
                Count = poligons.Count, 
                Poligons = poligons
            });
        });
        
        
        app.Run();
    }
}

public record DevideDTO(int x, int y, int z);