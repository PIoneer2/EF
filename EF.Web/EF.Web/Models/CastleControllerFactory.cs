using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Web.Mvc;
using System.Web.Routing;
using EF.Core.Data;
using EF.Data;
using EF.Core;

namespace EF.Web.Models
{
    public class CastleControllerFactory : DefaultControllerFactory
    {
        
        public IWindsorContainer Container { get; protected set; }

        public CastleControllerFactory(IWindsorContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.Container = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                return null;
            }
            return Container.Resolve(controllerType) as IController;
        }

        public override void ReleaseController(IController controller)
        {
            var disposableController = controller as IDisposable;
            if (disposableController != null)
            {
                disposableController.Dispose();
            }

            Container.Release(controller);
        }
    }
}