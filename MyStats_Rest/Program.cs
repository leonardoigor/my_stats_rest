using MayStats_Infra.Contexts;
using MayStats_Infra.Controller;
using MayStats_Infra.Interfaces.Repositories.Base;
using MayStats_Infra.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateSlimBuilder(args);

        builder.Services.ConfigureHttpJsonOptions(options =>
        {
            // options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
        });
        builder.Services.AddControllers();

        // Adicione o Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(); 
        builder.Services.AddScoped<AppDbContext>();
        var connectionString = "server=localhost;port=33600;database=myStats;user=root;password=root;";

        builder.Services.AddScoped<DbContext, AppDbContext>();



        builder.Services.AddScoped(typeof(IEFRepository<,>), typeof(EFRepository<,>));
        //builder.Services.AddScoped(typeof(GenericController<,>));

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API v1");
            });
        }

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            app.Run();
        });
    }
}