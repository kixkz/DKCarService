﻿namespace CarService.Models.Models.HealthChecks
{
    public class HealthCheckResponse
    {
        public string Status { get; init; }

        public IEnumerable<IndividualHealthCheckResponse> HealtChecks { get; init; }

        public TimeSpan HealthCheckDuration { get; init; }
    }
}
