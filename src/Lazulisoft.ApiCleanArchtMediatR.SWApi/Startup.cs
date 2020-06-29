using Lazulisoft.ApiCleanArchtMediatR.Application;
using Lazulisoft.ApiCleanArchtMediatR.Domain.Data;
using Lazulisoft.ApiCleanArchtMediatR.Domain.Entities;
using Lazulisoft.ApiCleanArchtMediatR.Infrastructure;
using Lazulisoft.ApiCleanArchtMediatR.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lazulisoft.ApiCleanArchtMediatR.SWApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                // Use SQL Server Database
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            // DI (Dependency Injection) extensions
            services.AddApplication();
            services.AddInfrastructure(Configuration);

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "SW Characters API documentation",
                    Contact = new OpenApiContact()
                    {
                        Name = "Leandro Dos Anjos",
                        Email = "laamaker@gmail.com",
                        Url = new Uri("https://lazulisoft.com")
                    }
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "SW Characters API");
            });

            app.UseMvc();

            // Create the database for test (REMOVE when publish to PROD)
            InitializeDatabaseAsync(app.ApplicationServices).GetAwaiter().GetResult();
        }

        private async Task InitializeDatabaseAsync(IServiceProvider services)
        {
            // Create a new service scope to ensure the database context is correctly disposed when this methods returns.
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await context.Database.EnsureCreatedAsync();

                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var characterRepo = unitOfWork.CharacterRepository;

                if (!characterRepo.All().Any())
                {
                    var characters = new List<Character>
                    {
                        new Character
                        {
                            Name = "Darth Vader",
                            FullName = "Anakin Skywalker",
                            Description = "Uma das figuras centrais da saga, Anakin é um dos mais lendários entre os cavaleiros Jedi. Ele serviu a República Galáctica, mas acabou tornando-se o lorde negro Sith conhecido como Darth Vader. Teve uma vida sofrida como escravo em Tatooine, mas foi libertado pelo Jedi Qui-Gon Jinn, que o fez ingressar na ordem Jedi. Mais tarde, passou para o Lado Negro da Força como aprendiz de Darth Sidious.",
                            Homeworld = "Tatooine",
                            Species = "Humano",
                            Gender = Domain.Enums.Gender.Male,
                            Occupation = "Escravo, Padawan, Cavaleiro Jedi, Lord Sith"
                        },
                        new Character
                        {
                            Name = "Padmé Amidala",
                            FullName = "Padmé Amidala Naberrie",
                            Description = "Eleita aos 14 anos Rainha de Naboo, posteriormente representou seu planeta no Senado Galáctico. É uma das principais personagens da trilogia mais recente. Por segurança, frequentemente se disfarçava como uma de suas damas de companhia enquanto uma sósia assumia seu lugar; deste modo escapou de ao menos um atentado contra sua vida. Conheceu Anakin Skywalker quando este ainda era uma criança e, anos mais tarde, casou-se em segredo com o jovem Jedi. De coração partido ao se dar conta de que Anakin havia se rendido ao Lado Negro da Força, Padmé deu à luz um casal de gêmeos, Luke Skywalker e Leia Organa, e morreu pouco após o parto.",
                            Homeworld = "Naboo",
                            Species = "Humana",
                            Gender = Domain.Enums.Gender.Female,
                            Occupation = "Rainha de Naboo, Senadora de Naboo"
                        },
                    };

                    characters.ForEach(x => characterRepo.Add(x));
                    await unitOfWork.SaveChangesAsync();
                }
            }
        }
    }
}
