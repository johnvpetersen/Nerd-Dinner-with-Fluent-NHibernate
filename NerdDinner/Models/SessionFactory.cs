using System.Web;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace NerdDinner.Models
{
 
    public interface ICustomSessionFactory
    {
        ISession CreateSession();
        ISessionFactory CreateSessionFactory();
    }
    
    public class CustomSessionFactory : ICustomSessionFactory
    {
       

        public ISession CreateSession (ISessionFactory sessionFactory)
        {
            return sessionFactory.OpenSession();
        }
        
        public ISession CreateSession()
        {
            var sessionfactory = (ISessionFactory)HttpContext.Current.Application["SessionFactory"];
            return sessionfactory.OpenSession();
        }
        
        
        public ISessionFactory CreateSessionFactory()
        {

            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2005
                              .ConnectionString(
                              c => c.Is(@"Data Source=.\SQLEXPRESS;AttachDbFilename='C:\Users\John Petersen\Projects\Nerd-Dinner-with-Fluent-NHibernate\NerdDinner\App_Data\NerdDinner.mdf';Integrated Security=True;User Instance=True"))) 
                              .Mappings(m => m.FluentMappings.AddFromAssemblyOf<CustomSessionFactory>())
                              .BuildSessionFactory();
        }
    }
}
