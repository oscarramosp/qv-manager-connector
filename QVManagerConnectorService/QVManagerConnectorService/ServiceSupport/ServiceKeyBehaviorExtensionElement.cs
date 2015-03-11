﻿using System;
using System.ServiceModel.Configuration;
using System.Configuration;

namespace QVManagerConnectorService.ServiceSupport
{
    public class ServiceKeyBehaviorExtensionElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(ServiceKeyEndpointBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new ServiceKeyEndpointBehavior();
        }
    }
}