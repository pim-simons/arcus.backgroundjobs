﻿using System;
using System.Text;
using Arcus.Messaging.Abstractions.ServiceBus.MessageHandling;
using CloudNative.CloudEvents;
using GuardNet;
using Microsoft.Extensions.Logging;

namespace Arcus.BackgroundJobs.CloudEvents
{
    /// <summary>
    /// Represents a custom <see cref="IAzureServiceBusMessageRouter"/> that can deserialize incoming CloudEvents messages to valid JSON objects.
    /// </summary>
    public class CloudEventMessageRouter : AzureServiceBusMessageRouter
    {
        private static readonly JsonEventFormatter JsonEventFormatter = new JsonEventFormatter();
        
        /// <inheritdoc />
        public CloudEventMessageRouter(
            IServiceProvider serviceProvider, 
            AzureServiceBusMessageRouterOptions options, 
            ILogger<AzureServiceBusMessageRouter> logger) 
            : base(serviceProvider, options, logger)
        {
        }

        /// <inheritdoc />
        protected override bool TryDeserializeToMessageFormat(string message, Type messageType, out object result)
        {
            Guard.NotNullOrWhitespace(message, nameof (message), "Requires a non-blank raw message to determine whether or not it can be parsed as a CloudEvent message");
            
            try
            {
                if (messageType == typeof(CloudEvent))
                {
                    CloudEvent cloudEvent = JsonEventFormatter.DecodeStructuredEvent(Encoding.UTF8.GetBytes(message));
                    
                    result = cloudEvent; 
                    return true;
                }
            }
            catch (Exception exception)
            {
                Logger.LogWarning(exception, "Unable to deserialize the CloudEvent");
            }
            
            return base.TryDeserializeToMessageFormat(message, messageType, out result);
        }
    }
}
