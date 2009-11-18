using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using NerdDinner.Controllers;
using NerdDinner.Models;
using NHibernate;
using StructureMap;

namespace NerdDinner
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new {controller = "Home", action = "Index", id = ""} // Parameter defaults
                );


            routes.MapRoute(
                "UpcomingDinners",
                "Dinners/Page/{page}",
                new {controller = "Dinners", action = "Index"}
                );
        }

        protected void Application_Start()
        {
            RegisterClasses();

            
            ControllerBuilder.Current.SetControllerFactory(
                new StructureMapControllerFactory());

            Application["SessionFactory"] = ObjectFactory.GetInstance<ICustomSessionFactory>().CreateSessionFactory();

            //Phil Haack's route debugger.
            //RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);
            RegisterRoutes(RouteTable.Routes);
        }

        private void RegisterClasses()
        {
            ObjectFactory.Configure(
                x => x.ForRequestedType<IDinnerRepository>().TheDefaultIsConcreteType<DinnerRepository>());

            ObjectFactory.Configure(
                x => x.ForRequestedType<ICustomSessionFactory>().TheDefaultIsConcreteType<CustomSessionFactory>());

            ObjectFactory.Configure(
                x => x.ForRequestedType<IFormsAuthentication>().TheDefaultIsConcreteType<FormsAuthenticationService>());

            ObjectFactory.Configure(
                x => x.ForRequestedType<IMembershipService>().TheDefaultIsConcreteType<AccountMembershipService>());

            ObjectFactory.Configure(
                x => x.ForRequestedType<IMembershipService>().TheDefaultIsConcreteType<AccountMembershipService>());

            ObjectFactory.Configure(
                x => x.ForRequestedType<ISession>()
                         .TheDefault.Is.ConstructedBy(
                         () => ObjectFactory.GetInstance<ICustomSessionFactory>().CreateSession()));

            ObjectFactory.Configure(
                x => x.ForRequestedType<MembershipProvider>()
                         .TheDefault.Is.ConstructedBy(
                         () => ObjectFactory.GetInstance<MembershipProviderFactory>().GetProvider()));
        }
    }
}