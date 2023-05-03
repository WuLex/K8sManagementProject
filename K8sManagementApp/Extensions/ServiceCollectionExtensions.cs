//namespace K8sManagementApp.Extensions
//{
//    /// <summary>
//    /// Extensions for <see cref="IServiceCollection"/>.
//    /// </summary>
//    public static class ServiceCollectionExtensions
//    {

//        //https://github.com/buehler/dotnet-operator-sdk/blob/6da41bd408ea2baebda8d9b55a65e7e0fdee424f/src/KubeOps/Operator/Builder/OperatorBuilder.cs
//        /// <summary>
//        /// Add the kubernetes operator to the dependency injection
//        /// and configure the operator.
//        /// </summary>
//        /// <param name="services"><see cref="IServiceCollection"/>.</param>
//        /// <param name="configure">An optional configure action for adjusting settings in the operator.</param>
//        /// <returns>An <see cref="IOperatorBuilder"/> for further configuration and chaining.</returns>
//        public static IOperatorBuilder AddKubernetesOperator(
//            this IServiceCollection services,
//            Action<OperatorSettings>? configure = null)
//        {
//            var settings = new OperatorSettings();
//            configure?.Invoke(settings);
//            return AddKubernetesOperator(services, settings);
//        }

//        /// <summary>
//        /// Add the kubernetes operator to the dependency injection
//        /// and configure the operator.
//        /// </summary>
//        /// <param name="services"><see cref="IServiceCollection"/>.</param>
//        /// <param name="settings">An instance of the operator settings to use.</param>
//        /// <returns>An <see cref="IOperatorBuilder"/> for further configuration and chaining.</returns>
//        public static IOperatorBuilder AddKubernetesOperator(
//            this IServiceCollection services,
//            OperatorSettings settings) => new OperatorBuilder(services).AddOperatorBase(settings);
//    }
//}
