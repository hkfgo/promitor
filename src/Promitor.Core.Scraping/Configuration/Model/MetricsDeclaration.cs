﻿using System.Collections.Generic;
using Promitor.Core.Scraping.Configuration.Model.Metrics;

namespace Promitor.Core.Scraping.Configuration.Model
{
    public class MetricsDeclaration
    {
        public bool UseAzureMonitorSdk { get; set; } = true;
        public AzureMetadata AzureMetadata { get; set; }
        public MetricBatchScrapeConfig? MetricBatchConfig { get; set; } 
        public MetricDefaults MetricDefaults { get; set; } = new MetricDefaults();
        public List<MetricDefinition> Metrics { get; set; } = new List<MetricDefinition>();
    }
}