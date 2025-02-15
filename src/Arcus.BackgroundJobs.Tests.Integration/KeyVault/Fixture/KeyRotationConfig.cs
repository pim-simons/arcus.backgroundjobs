﻿using System;
using Arcus.BackgroundJobs.Tests.Integration.Fixture;
using Arcus.BackgroundJobs.Tests.Integration.Fixture.ServiceBus;
using GuardNet;

namespace Arcus.BackgroundJobs.Tests.Integration.KeyVault.Fixture
{
    /// <summary>
    /// Represents all the configuration values related to testing key rotation.
    /// </summary>
    public class KeyRotationConfig
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyRotationConfig" /> class.
        /// </summary>
        /// <param name="keyVault">The config to represent a Azure Key Vault secret.</param>
        /// <param name="servicePrincipal">The config to authenticate to Azure resources.</param>
        /// <param name="serviceBusNamespace">The config to represent a Azure Service Bus namespace.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when the <paramref name="keyVault"/>, <paramref name="servicePrincipal"/>, or <paramref name="serviceBusNamespace"/> is <c>null</c>.
        /// </exception>
        public KeyRotationConfig(KeyVaultConfig keyVault, ServicePrincipal servicePrincipal, ServiceBusNamespace serviceBusNamespace)
        {
            Guard.NotNull(keyVault, nameof(keyVault));
            Guard.NotNull(servicePrincipal, nameof(servicePrincipal));
            Guard.NotNull(serviceBusNamespace, nameof(serviceBusNamespace));

            KeyVault = keyVault;
            ServicePrincipal = servicePrincipal;
            ServiceBusNamespace = serviceBusNamespace;
        }

        /// <summary>
        /// Gets the config representing a Azure Key Vault secret.
        /// </summary>
        public KeyVaultConfig KeyVault { get; }

        /// <summary>
        /// Gets the config to authenticate to Azure resources.
        /// </summary>
        public ServicePrincipal ServicePrincipal { get; }

        /// <summary>
        /// Gets the config to represent a Azure Service Bus Queue.
        /// </summary>
        public ServiceBusNamespace ServiceBusNamespace { get; }
    }
}
