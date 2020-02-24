namespace Persistencia.Migrations
{
    using Persistencia.Entity;
    using Persistencia.Repositorios;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Persistencia.Entity.CatafexEntities>
    {
        EntityFramework ef;
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Persistencia.Entity.CatafexEntities context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //ef = new EntityFramework(context);


        }
        
    }
}
