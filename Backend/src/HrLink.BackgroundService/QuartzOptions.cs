namespace HrLink.BackgroundService;

public class QuartzOptions
{
    public string ConnectionString { get; set; } = string.Empty;
    public string TablePrefix { get; set; } = string.Empty;
    public Dictionary<string, string> Properties = new Dictionary<string, string>();
}