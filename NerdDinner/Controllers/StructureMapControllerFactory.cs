using System;
using System.Web.Mvc;
using StructureMap;

namespace NerdDinner.Controllers
{
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(Type controllerType)
        {

            if (controllerType == null) return null;
            try
            {
                return ObjectFactory.GetInstance(controllerType) as Controller;
            }

            catch (StructureMapException)
            {
                System.Diagnostics.Debug.WriteLine(ObjectFactory.WhatDoIHave());
                throw;
            }
        }
    }
}
