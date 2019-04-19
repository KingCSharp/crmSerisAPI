﻿using SimpleInjector;
using System.Reflection;
using crmSeries.Core.Mediator.Decorators;
using crmSeries.Core.Mediator.Mediators;

namespace crmSeries.Core.Mediator.Configuration
{
    public static class SimpleInjectorMediatorConfiguration
    {
        public static void ConfigureMediator(this Container container, Assembly[] assemblies)
        {
            ConfigureDynamicMediator(container, assemblies);
        }

        public static void ConfigureDynamicMediator(this Container container, Assembly[] assemblies)
        {
            container.Register<IMediator>(() => new DynamicDispatchMediator(container.GetInstance));

            RegisterHandlers(container, assemblies);
        }

        public static void ConfigureStaticMediator(this Container container, Assembly[] assemblies)
        {
            container.RegisterInstance<IStaticDispatcher>(
                new StaticDispatcher(container.GetInstance, assemblies));

            container.Register<IMediator>(
                () => new StaticDispatchMediator(container.GetInstance<IStaticDispatcher>()));

            RegisterHandlers(container, assemblies);
        }

        private static void RegisterHandlers(Container container, Assembly[] assemblies)
        {
            bool ShouldValidate(DecoratorPredicateContext context) =>
                !context.ImplementationType.RequestHasAttribute(typeof(DoNotValidateAttribute));

            bool IsAdminContext(DecoratorPredicateContext context) =>
                context.ImplementationType.RequestHasAttribute(typeof(AdminContextAttribute));

            bool IsHeavyEquipmentContext(DecoratorPredicateContext context) =>
                context.ImplementationType.RequestHasAttribute(typeof(HeavyEquipmentContextAttribute));
            
            container.Register(typeof(IRequestHandler<>), assemblies);
            container.Register(typeof(IRequestHandler<,>), assemblies);
            container.RegisterDecorator(typeof(IRequestHandler<>), typeof(ValidationHandler<>), ShouldValidate);
            container.RegisterDecorator(typeof(IRequestHandler<,>), typeof(ValidationHandler<,>), ShouldValidate);

            container.RegisterDecorator(typeof(IRequestHandler<>), typeof(AdminTransactionHandler<>), IsAdminContext);
            container.RegisterDecorator(typeof(IRequestHandler<,>), typeof(AdminTransactionHandler<,>), IsAdminContext);
            //container.RegisterDecorator(typeof(IRequestHandler<>), typeof(HeavyEquipmentTransactionHandler<>), IsHeavyEquipmentContext);
            //container.RegisterDecorator(typeof(IRequestHandler<,>), typeof(HeavyEquipmentTransactionHandler<,>), IsHeavyEquipmentContext);
        }
    }
}
