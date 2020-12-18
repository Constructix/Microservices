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

            var _categories = SetupCategories();
            var _suppliers = SetupSuppliers(_categories);
           

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

        private List<Supplier> SetupSuppliers(List<Category> categories)
        {

            object testObject = new object();
            List<Supplier> _suppliers;
            lock (testObject)
            {
             
                if (!System.IO.File.Exists("suppliers.json"))
                {
                    _suppliers = IntialiseSuppliers(categories);
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

        private List<Supplier> IntialiseSuppliers(List<Category> categories)
        {
            var _suppliers = new List<Supplier>();

            _suppliers.Add(new Supplier
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Narangba Timbers",
                EffectiveFrom = DateTime.Parse("01/01/2020"),
                webAddress = "www.narangbaTimbers.com.au",

                Locations = new List<Location>() { new Location
                {
                    Address = new Address { StreetLine1 = "728 Old Gympie Road", Suburb = "Narangba", Postcode = "4504", State = "QLD"},
                    Phone = "07 3888 1293"
                }},

                    
                Categories = new List<Category>
                {
                    new Category
                    {
                        Id = Guid.NewGuid().ToString(), Name = "Construction",
                        EffectiveFrom = DateTime.Parse("01/01/2020")
                    }
                }
            });

            _suppliers.Add(new Supplier()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "A Wood Shed",
                EffectiveFrom = DateTime.Parse("01/01/2020"),

                Locations = new List<Location>() { new Location
                {
                    Address = new Address { StreetLine1 = "1/217 Pine Mountain Rd", Suburb = "Brassall", Postcode = "4305", State = "QLD"},
                    Phone = "07 3813 0644"
                },
                    new Location
                    {
                        Address = new Address { StreetLine1 = "46 Queensland Road", Suburb = "Darra", Postcode = "4076", State = "QLD"},
                        Phone = "07 3375 1726"
                    }
                },
                Categories = new List<Category> 
                { 
                    categories.FirstOrDefault(x=>x.Name.Equals("Constructixion"))
                }
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
