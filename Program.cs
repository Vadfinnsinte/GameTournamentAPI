using GameTournamentAPI.Converters;
using GameTournamentAPI.Data;
using GameTournamentAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

namespace GameTournamentAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            builder.Services.AddOpenApi(options =>
            {
                // fix date format in Swagger
                options.AddSchemaTransformer((schema, context, cancellationToken) =>
                {

                    if (context.JsonTypeInfo.Type == typeof(DateTime))
                    {
                        schema.Type = JsonSchemaType.String;
                        schema.Format = "yyyy-MM-dd HH:mm";
                        schema.Example = "2026-08-23 15:42";
                    }

                    return Task.CompletedTask;
                });
            });

            builder.Services.AddControllers();



            builder.Services.AddScoped<TournamentService>();
            builder.Services.AddScoped<GameService>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/openapi/v1.json", "My API v1");
                    options.RoutePrefix = "swagger";
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
