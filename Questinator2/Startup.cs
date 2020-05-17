using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Questinator2.Models;

namespace Questinator2
{
    public class Startup
    {
        readonly string VueCorsPolicy = "_vueCorsPolicy";
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = "Server=(local);Initial Catalog=Questionator;Integrated Security=True";
            services.AddTransient<IUserRepository, UserRepository>(provider => new UserRepository(connectionString));
            services.AddTransient<IAnswerRepository, AnswerRepository>(provider => new AnswerRepository(connectionString));
            services.AddCors(options =>
            {
                options.AddPolicy(name: VueCorsPolicy,
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin()//.WithOrigins("http://localhost:8080")
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                                        //.AllowCredentials(); 
                                      

                                  });
            });
            //сюда будем добавлять остальные сущности из бд
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(VueCorsPolicy);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // подключаем маршрутизацию на контроллеры
            });
        }
    }
}
