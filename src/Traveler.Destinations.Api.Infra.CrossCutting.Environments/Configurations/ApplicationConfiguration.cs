namespace Traveler.Destinations.Api.Infra.CrossCutting.Environments.Configurations;

public class ApplicationConfiguration
{
    public string Environment { get; set; }
    public string GlobalErrorCode { get; set; }
    public string GlobalErrorMessage { get; set; }
    public string ConnectionString { get; set; }
}
