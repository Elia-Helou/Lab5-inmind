namespace DDDProject.API.Configurations;

public class ApiVersioningConfig
{
    public bool AssumeDefaultVersion { get; set; }
    public int DefaultMajorVersion { get; set; }
    public int DefaultMinorVersion { get; set; }
    public bool ReportApiVersions { get; set; }
}