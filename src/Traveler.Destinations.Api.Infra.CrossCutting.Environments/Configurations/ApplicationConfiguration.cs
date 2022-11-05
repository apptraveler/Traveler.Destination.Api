namespace Traveler.Destinations.Api.Infra.CrossCutting.Environments.Configurations;

public class ApplicationConfiguration
{
    public string Environment { get; }
    public string GlobalErrorCode { get; }
    public string GlobalErrorMessage { get; }
    public string ConnectionString { get; }
}
