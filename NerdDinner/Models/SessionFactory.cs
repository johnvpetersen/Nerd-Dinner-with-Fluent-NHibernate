using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace NerdDinner.Models
{
    public class SessionFactory
    {
        public ISessionFactory CreateSessionFactory()
        {

            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2005
                              .ConnectionString(
                              c => c.Is(@"Data Source=.\SQLEXPRESS;AttachDbFilename='<<Your machine-specific path info here.>>\App_Data\NerdDinner.mdf';Integrated Security=True;User Instance=True"))) 
                              .Mappings(m => m.FluentMappings.AddFromAssemblyOf<SessionFactory>())
                              .BuildSessionFactory();
        }
    }
}
