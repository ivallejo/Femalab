using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;

namespace Femalab.Modules
{
    public class RepositoryModule :Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("Femalab.Repository"))
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .WithParameter((pi, c) => pi.Name == "dbContext",
                            (pi, c) => c.ResolveNamed<DbContext>("databaseA"))
                .InstancePerLifetimeScope();
                
        }
    }
}