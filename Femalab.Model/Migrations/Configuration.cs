namespace Femalab.Model.Migrations
{
    using Femalab.Model.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Femalab.Model.Persistence.FemalabContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Femalab.Model.FemalabContext";
        }

        protected override void Seed(Femalab.Model.Persistence.FemalabContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //var separador = '|';
            var carpeta = @"C:\GitHub\Femalab\Femalab.Model\Data\";

            if (!context.Ubigeo.Any())
            {
                var ubigeos = File.ReadAllLines($"{carpeta}Ubigeo.txt");
                context.Ubigeo.AddOrUpdate(ubigeos.Select(linea => linea)
                    .Select(valores => new Ubigeo
                    {
                        Code = valores.Substring(0, 6),
                        Description = valores.Substring(7).Trim()
                    }).ToArray());
            }

            if (!context.Doctor.Any())
            {
                context.Doctor.AddOrUpdate(
                     p => p.FullName,
                     new Doctor { Code = "01", FullName = "MEDICO", State = true }
                   );
            }
            if (!context.AttentionCategory.Any())
            {
                context.AttentionCategory.AddOrUpdate(
                     p => p.Category,
                     new AttentionCategory { Code = "01", Category = "Consulta", Tag = "success", Action = "Attention", State = true },
                     new AttentionCategory { Code = "02", Category = "Laboratorio", Tag = "info", Action = "Laboratory", State = true },
                     new AttentionCategory { Code = "03", Category = "Farmacia", Tag = "warning", Action = "Pharmacy", State = true }
                   );
            }
            if (!context.AttentionType.Any())
            {
                context.AttentionType.AddOrUpdate(
                     p => p.Type,
                     new AttentionType { Code = "01", Type = "MEDICINA GENERAL", Tag = "info", State = true },
                     new AttentionType { Code = "02", Type = "PEDIATRÍA", Tag = "success", State = true },
                     new AttentionType { Code = "03", Type = "GINECOLOGÍA", Tag = "success", State = true  },
                     new AttentionType { Code = "04", Type = "NUTRICIÓN", Tag = "warning", State = true },
                     new AttentionType { Code = "05", Type = "OFTALMOLOGÍA", Tag = "primary", State = true },
                     new AttentionType { Code = "06", Type = "IMÁGENES", Tag = "danger", State = true }
                   );
            }
            if (!context.Category.Any())
            {
                context.Category.AddOrUpdate(
                    p => p.Description,
                    new Category { Code = "01", Description = "CONSULTA", Observation = "", State = true },
                    new Category { Code = "02", Description = "LABORATORIO", Observation = "", State = true },
                    new Category { Code = "03", Description = "FARMACIA", Observation = "", State = true }

                );
            }
            if (!context.Specialty.Any())
            {
                context.Specialty.AddOrUpdate(
                    p => p.Description,
                    new Specialty { Code = "00", Description = "CONSULTA", Observation = "", State = true },
                    new Specialty { Code = "01", Description = "NINGUNO", Observation = "", State = true },
                    new Specialty { Code = "02", Description = "BACTERIOLOGIA", Observation = "", State = true },
                    new Specialty { Code = "03", Description = "BIOLOGÍA MOLECULAR", Observation = "", State = true },
                    new Specialty { Code = "04", Description = "BIOQUIMICA", Observation = "", State = true },
                    new Specialty { Code = "05", Description = "HEMATOLOGIA", Observation = "", State = true },
                    new Specialty { Code = "06", Description = "INMUNOLOGIA", Observation = "", State = true },
                    new Specialty { Code = "07", Description = "BIOQUIMICA", Observation = "", State = true },
                    new Specialty { Code = "08", Description = "MANUALES", Observation = "", State = true },
                    new Specialty { Code = "09", Description = "OTROS", Observation = "", State = true }

                );
            }
            if (!context.Product.Any())
            {
                context.Product.AddOrUpdate(
                    p => p.Category,
                    new Product { Code = "00001", Description = "MEDICINA GENERAL", SpecialtyId = 1, CategoryId = 1, State = true, Price = 10.00m },
                    new Product { Code = "00002", Description = "PEDIATRÍA", SpecialtyId = 1, CategoryId = 1, State = true, Price = 10.00m },
                    new Product { Code = "00003", Description = "GINECOLOGÍA", SpecialtyId = 1, CategoryId = 1, State = true, Price = 10.00m },
                    new Product { Code = "00004", Description = "NUTRICIÓN", SpecialtyId = 1, CategoryId = 1, State = true, Price = 10.00m },
                    new Product { Code = "00005", Description = "OFTALMOLOGÍA", SpecialtyId = 1, CategoryId = 1, State = true, Price = 10.00m },
                    new Product { Code = "00006", Description = "IMÁGENES", SpecialtyId = 1, CategoryId = 1, State = true, Price = 10.00m }

                    //new Product { Code = "A0001", Description = "ACIDO FOLICO", SpecialtyId = 10, CategoryId = 2, State = true, Price = 47.00m },
                    //new Product { Code = "A0002", Description = "ACIDO URICO", SpecialtyId = 8, CategoryId = 2, State = true, Price = 8.00m },
                    //new Product { Code = "A0003", Description = "ACIDO URICO EN ORINA DE 12 HORAS", SpecialtyId = 8, CategoryId = 2, State = true, Price = 6.00m },
                    //new Product { Code = "A0004", Description = "ACIDO URICO EN ORINA DE 24 HORAS", SpecialtyId = 8, CategoryId = 2, State = true, Price = 11.00m },
                    //new Product { Code = "A0005", Description = "ACIDO VALPROICO", SpecialtyId = 10, CategoryId = 2, State = true, Price = 61.00m },
                    //new Product { Code = "A0006", Description = "ACIDO VANIL MANDELICO EN ORINA DE 24 HORAS", SpecialtyId = 10, CategoryId = 2, State = true, Price = 88.00m },
                    //new Product { Code = "H0007", Description = "HORMONA ADRENOCORTICOTROFICA (ACTH)", SpecialtyId = 7, CategoryId = 2, State = true, Price = 103.00m }

                );
            }
        }
    }
}
