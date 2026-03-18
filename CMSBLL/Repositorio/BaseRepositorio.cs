using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using Castle.Windsor.Installer;
using Castle.MicroKernel.Registration;
using CMSXEF;
using System.Data.Entity;

namespace CMSBLL.Repositorio
{
    public abstract class BaseRepositorio
    {
        protected IWindsorContainer container;
        protected dynamic lprop;
        protected cmsxDBEntities db;

        protected BaseRepositorio()
        {
            BootstrapContainer();
        }

        private void BootstrapContainer()
        {
            container = new WindsorContainer();//new XmlInterpreter(@"C:\Users\Cesar\Documents\Visual Studio 2010\Projects\POC\IoCPOCvCastle\IoCPOC.WebApp\ConfigCastle.xml"));
            //container.Register(AllTypes.FromAssemblyNamed("CMSDAL").Pick().WithServiceAllInterfaces());
            container.Register(Classes.FromAssemblyNamed("CMSDAL").Pick().WithServiceAllInterfaces());
            container.Register(Classes.FromAssemblyNamed("CMSBLL").Pick().WithServiceAllInterfaces());
        }
    }
}