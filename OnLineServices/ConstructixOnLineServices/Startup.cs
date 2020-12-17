using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ConstructixOnLineServices.Controllers;
using ConstructixOnLineServices.Models;
using ConstructixOnLineServices.Repository;
using ConstructixOnLineServices.Services;

namespace ConstructixOnLineServices
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ConstructixOnLineServices", Version = "v1" });
            });

            var _suppliers = SetupSuppliers();
            var _categories = SetupCategories();

            services.AddScoped<IRepository<Supplier, string>>(c => new SupplierRepository(_suppliers));
            services.AddScoped<IRepository<Category, string>>(c => new CategoryRepository(_categories));

            services.AddScoped<ISupplierService, SupplierService>();

        }

        private List<Category> SetupCategories()
        {
            object testObject = new object();
            List<Category> _categories;
            lock (testObject)
            {
                if (!System.IO.File.Exists("categories.json"))
                {
                    _categories = InitialiseCategories();
                    WriteCategories(_categories);
                }
                else
                {
                    var categoriesJSON = File.ReadAllText("categories.json");
                    _categories = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Category>>(categoriesJSON);
                }
            }

            return _categories;
        }

        private List<Supplier> SetupSuppliers()
        {

            object testObject = new object();
            List<Supplier> _suppliers;
            lock (testObject)
            {
             
                if (!System.IO.File.Exists("suppliers.json"))
                {
                    _suppliers = IntialiseSuppliers();
                    WriteSuppliers(_suppliers);
                }
                else
                {
                    var supplierJson = File.ReadAllText("suppliers.json");
                    _suppliers = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Supplier>>(supplierJson);
                }
            }

            return _suppliers;
        }


        private void WriteSuppliers(List<Supplier> suppliers)
        {
            System.IO.File.WriteAllText("suppliers.json", Newtonsoft.Json.JsonConvert.SerializeObject(suppliers));
        }

        private List<Supplier> IntialiseSuppliers()
        {
            var _suppliers = new List<Supplier>();

            // creating of data should be in startup rather than in controllers.
            // should be in the services side.

            _suppliers.Add(new Supplier
            {
                Name = "Nerangba Timbers",
                EffectiveFrom = DateTime.Parse("01/01/2020"),
                Categories = new List<Category>
                {
                    new Category
                    {
                        Id = Guid.NewGuid().ToString(), Name = "Construction",
                        EffectiveFrom = DateTime.Parse("01/01/2020")
                    }
                }
            });

            _suppliers.FirstOrDefault(x => x.Name.Equals("Nerangba Timbers")).Categories
                .FirstOrDefault(x => x.Name.Equals("Construction")).SubCategories.Add(new Category
                {
                    Id = Guid.NewGuid().ToString(), Name = "Building Supplies",
                    EffectiveFrom = DateTime.Parse("01/01/2020")
                });

            WriteSuppliers(_suppliers);

            return _suppliers;

        }
        private List<Category> InitialiseCategories()
        {
            var _categories = new List<Category>();
            _categories = new List<Category>();
            _categories.Add(new Category
                { Id = Guid.NewGuid().ToString(), Name = "Construction", EffectiveFrom = DateTime.Parse("01/01/2020") });
            _categories.Add(new Category
                { Id = Guid.NewGuid().ToString(), Name = "Restaurant", EffectiveFrom = DateTime.Parse("01/01/2020") });


            var buildingSupplyCategory = new Category
                { Id = Guid.NewGuid().ToString(), Name = "Building Supplies", EffectiveFrom = DateTime.Parse("01/01/2020") };
            _categories.FirstOrDefault(x => x.Name.Equals("Construction")).SubCategories.Add(buildingSupplyCategory);


            var concreting = new Category
                { Id = Guid.NewGuid().ToString(), Name = "Concreting", EffectiveFrom = DateTime.Parse("01/01/2020") };
            _categories.FirstOrDefault(x => x.Name.Equals("Construction")).SubCategories.Add(concreting);

            return _categories;
        }

        private void WriteCategories(List<Category> categories)
        {
            System.IO.File.WriteAllText("categories.json", Newtonsoft.Json.JsonConvert.SerializeObject(categories));
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ConstructixOnLineServices v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
