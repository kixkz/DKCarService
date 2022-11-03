using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CarService.HealtChecks
{
    public class CustomHealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                //throw new ArgumentException("Failed");
            }
            catch (Exception e)
            {
                return HealthCheckResult.Unhealthy(e.Message);
            }

            return HealthCheckResult.Healthy("Connection is OK");
        }
    }
}
