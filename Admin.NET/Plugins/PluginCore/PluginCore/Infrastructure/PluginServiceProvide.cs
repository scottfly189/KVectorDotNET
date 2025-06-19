// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！



﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

//namespace PluginCore.Infrastructure
//{
//    /// <summary>
//    /// The default IServiceProvider.
//    /// </summary>
//    public class PluginServiceProvide : IServiceProvider, IDisposable, IServiceProviderEngineCallback, IAsyncDisposable
//    {
//        private readonly IServiceProviderEngine _engine;

//        private readonly CallSiteValidator _callSiteValidator;

//        internal ServiceProvider(IEnumerable<ServiceDescriptor> serviceDescriptors, IServiceProviderEngine engine, ServiceProviderOptions options)
//        {
//            _engine = engine;

//            if (options.ValidateScopes)
//            {
//                _engine.InitializeCallback(this);
//                _callSiteValidator = new CallSiteValidator();
//            }

//            if (options.ValidateOnBuild)
//            {
//                List<Exception> exceptions = null;
//                foreach (ServiceDescriptor serviceDescriptor in serviceDescriptors)
//                {
//                    try
//                    {
//                        _engine.ValidateService(serviceDescriptor);
//                    }
//                    catch (Exception e)
//                    {
//                        exceptions = exceptions ?? new List<Exception>();
//                        exceptions.Add(e);
//                    }
//                }

//                if (exceptions != null)
//                {
//                    throw new AggregateException("Some services are not able to be constructed", exceptions.ToArray());
//                }
//            }
//        }

//        /// <summary>
//        /// Gets the service object of the specified type.
//        /// </summary>
//        /// <param name="serviceType">The type of the service to get.</param>
//        /// <returns>The service that was produced.</returns>
//        public object GetService(Type serviceType) => _engine.GetService(serviceType);

//        /// <inheritdoc />
//        public void Dispose()
//        {
//            _engine.Dispose();
//        }

//        void IServiceProviderEngineCallback.OnCreate(ServiceCallSite callSite)
//        {
//            _callSiteValidator.ValidateCallSite(callSite);
//        }

//        void IServiceProviderEngineCallback.OnResolve(Type serviceType, IServiceScope scope)
//        {
//            _callSiteValidator.ValidateResolution(serviceType, scope, _engine.RootScope);
//        }

//        /// <inheritdoc/>
//        public ValueTask DisposeAsync()
//        {
//            return _engine.DisposeAsync();
//        }
//    }

//    internal interface IServiceProviderEngineCallback
//    {
//        void OnCreate(ServiceCallSite callSite);
//        void OnResolve(Type serviceType, IServiceScope scope);
//    }

//    /// <summary>
//    /// Summary description for ServiceCallSite
//    /// </summary>
//    internal abstract class ServiceCallSite
//    {
//        protected ServiceCallSite(ResultCache cache)
//        {
//            Cache = cache;
//        }

//        public abstract Type ServiceType { get; }
//        public abstract Type ImplementationType { get; }
//        public abstract CallSiteKind Kind { get; }
//        public ResultCache Cache { get; }

//        public bool CaptureDisposable =>
//            ImplementationType == null ||
//            typeof(IDisposable).IsAssignableFrom(ImplementationType) ||
//            typeof(IAsyncDisposable).IsAssignableFrom(ImplementationType);
//    }


//    internal struct ResultCache
//    {
//        public static ResultCache None { get; } = new ResultCache(CallSiteResultCacheLocation.None, ServiceCacheKey.Empty);

//        internal ResultCache(CallSiteResultCacheLocation lifetime, ServiceCacheKey cacheKey)
//        {
//            Location = lifetime;
//            Key = cacheKey;
//        }

//        public ResultCache(ServiceLifetime lifetime, Type type, int slot)
//        {
//            Debug.Assert(lifetime == ServiceLifetime.Transient || type != null);

//            switch (lifetime)
//            {
//                case ServiceLifetime.Singleton:
//                    Location = CallSiteResultCacheLocation.Root;
//                    break;
//                case ServiceLifetime.Scoped:
//                    Location = CallSiteResultCacheLocation.Scope;
//                    break;
//                case ServiceLifetime.Transient:
//                    Location = CallSiteResultCacheLocation.Dispose;
//                    break;
//                default:
//                    Location = CallSiteResultCacheLocation.None;
//                    break;
//            }
//            Key = new ServiceCacheKey(type, slot);
//        }

//        public CallSiteResultCacheLocation Location { get; set; }

//        public ServiceCacheKey Key { get; set; }
//    }


//    internal enum CallSiteResultCacheLocation
//    {
//        Root,
//        Scope,
//        Dispose,
//        None
//    }

//    internal interface IServiceProviderEngine : IServiceProvider, IDisposable, IAsyncDisposable
//    {
//        IServiceScope RootScope { get; }
//        void InitializeCallback(IServiceProviderEngineCallback callback);
//        void ValidateService(ServiceDescriptor descriptor);
//    }

//}


