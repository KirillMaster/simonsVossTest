using Newtonsoft.Json.Linq;

namespace BusinessLogic;

public class Match
{
    public string Path { get; set; }
    public string Value { get; set; }
    public int Weight { get; set; }
    public bool IsFullMatch { get; set; }
    public string MatchedObject { get; set; }
}