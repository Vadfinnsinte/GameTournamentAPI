using GameTournamentAPI.Services;
using Microsoft.OpenApi;
using GameTournamentAPI.Converters;

namespace GameTournamentAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

       
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

            builder.Services.AddControllers()
                 .AddJsonOptions(options =>
                 {
                     //always run converter on dateTime in controllers
                     options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                 });



            builder.Services.AddSingleton<TournamentService>();
            builder.Services.AddSingleton<GameService>();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi


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
