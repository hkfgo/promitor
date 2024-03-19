using System;
using System.Collections.Generic;
using GuardNet;
using Promitor.Core.Contracts;

namespace Promitor.Core.Scraping.Configuration.Model.Metrics
{
    /// <summary>
    /// Defines a batch of ScrapeDefinitions to be executed in a single request 
    /// Scrape definitions within a batch should share
    /// 1. The same resource type 
    /// 2. The same Azure metric scrape target with identical dimensions
    /// 3. The same time granularity 
    /// </summary>
    public class BatchScrapeDefinition<TResourceDefinition> where TResourceDefinition : class, IAzureResourceDefinition
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ScrapeDefinition{TResourceDefinition}"/> class.
        /// </summary>
        /// <param name="azureMetricConfiguration">Configuration about the Azure Monitor metric to scrape</param>
        /// <param name="azureMetricConfiguration">Configuration about the Azure Monitor metric to scrape</param>
        /// <param name="scraping">The scraping model.</param>
        /// <param name="subscriptionId">Specify a subscription to scrape that defers from the default subscription.</param>
        /// <param name="resourceGroupName">
        /// The name of the resource group containing the resource to scrape. This should contain the global
        /// resource group name if none is overridden at the resource level.
        /// </param>
        public BatchScrapeDefinition(
            List<ScrapeDefinition<TResourceDefinition>> groupedScrapeDefinitions,
            AzureMetricConfiguration azureMetricConfiguration,
            Scraping scraping,
            TimeSpan aggregationInterval,
            string subscriptionId,
            string resourceGroupName)
        {
            Guard.NotNull(groupedScrapeDefinitions, nameof(groupedScrapeDefinitions));
            Guard.NotNull(azureMetricConfiguration, nameof(azureMetricConfiguration));
            Guard.NotNull(scraping, nameof(scraping));
            Guard.NotNull(subscriptionId, nameof(subscriptionId));
            Guard.NotNull(resourceGroupName, nameof(resourceGroupName));
            ScrapeDefinitions = groupedScrapeDefinitions;
            AggregationInterval = aggregationInterval;
            Scraping = scraping;
            SubscriptionId = subscriptionId;
            ResourceGroupName = resourceGroupName;
        }

        /// <summary>
        /// A batch of scrape job definitions to be executed as a single request 
        /// </summary>
        public List<ScrapeDefinition<TResourceDefinition>> ScrapeDefinitions { get; set; } = new List<ScrapeDefinition<TResourceDefinition>>();

        /// <summary>
        /// Configuration about the Azure Monitor metric to scrape
        /// </summary>
        public AzureMetricConfiguration AzureMetricConfiguration { get; }


        /// <summary>
        /// The scraping model.
        /// </summary>
        public Scraping Scraping { get; }

        /// <summary>
        /// The Azure subscription to get the metric from. This should be used instead of using
        /// the SubscriptionId from <see cref="Resource"/> because this property will contain
        /// the global subscription id if none is overridden at the resource level.
        /// </summary>
        public string SubscriptionId { get; }

        /// <summary>
        /// The Azure resource group to get the metric from. This should be used instead of using
        /// the ResourceGroupName from <see cref="Resource"/> because this property will contain
        /// the global resource group name if none is overridden at the resource level.
        /// </summary>
        public string ResourceGroupName { get; }

        public TimeSpan AggregationInterval{ get; }
    }
}