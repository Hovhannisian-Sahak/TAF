namespace TAF.Core.Timeouts;

public class Timeouts
{
    public int Default { get; set; } = 10;
    public int Short { get; set; } = 3;
    public int Long { get; set; } = 20;
    public int Retry { get; set; } = 3;
}
