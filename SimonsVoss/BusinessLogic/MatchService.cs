using Newtonsoft.Json.Linq;

namespace BusinessLogic;

public class MatchService
{
    public IEnumerable<Match> GetMatches(string searchValue, JObject data)
    {
        return data
            .Descendants()
            .OfType<JValue>()
            .Where(p => p.Value != null)
            .Where(p => p.Value.Equals(searchValue)).Select(x => new Match
            {
                Path = x.Path,
                Value = x.Value.ToString()
            });
    }
}