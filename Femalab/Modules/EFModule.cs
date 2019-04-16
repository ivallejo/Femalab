using Autofac;
using Femalab.Model.Persistence;
using Femalab.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Femalab.Modules
{
    public class EFModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterType<FemalabContext>().Named<DbContext>("databaseA").InstancePerLifetimeScope();
            builder.RegisterType<PersonaContext>().Named<DbContext>("databaseB").InstancePerLifetimeScope();
            //builder.RegisterType(typeof(FemalabContext)).As(typeof(DbContext)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerRequest();

        }
    }
}