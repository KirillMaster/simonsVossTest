using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BusinessLogic;

public class MatchService
{
    public WeightsConfig WeightsConfig { get; }

    public MatchService()
    {
        WeightsConfig = JsonConvert.DeserializeObject<WeightsConfig>(File.ReadAllText("weights_config.json"));
    }

    public IEnumerable<Match> GetMatches(string searchValue, JObject data)
    {
        return data
            .Descendants()
            .OfType<JValue>()
            .Where(p => p.Value != null)
            .Select(p =>
            {
                var value = p.Value?.ToString() ?? "";
                return new
                {
                    JValue = p,
                    MatchedValue = value,
                    IsFullMatch = IsFullMatch(value, searchValue),
                    IsPartialMatch = IsPartialMatch(value, searchValue)
                };
            })
            .Where(x => x.IsFullMatch || x.IsPartialMatch)
            .Select(x =>
            {
                var convertedPath = ConvertPath(x.JValue.Path);
                return new Match
                {
                    Path = convertedPath,
                    Value = x.MatchedValue,
                    MatchedObject = GetRootObject(x.JValue.Path, data),
                    Weight = GetWeight(convertedPath, x.IsFullMatch)
                };
            });
    }

    /// <summary>
    /// Converts jObjectPath with indexes to abstract path.
    /// Example: buildings[0].name -> buildings.name
    /// </summary>
    /// <param name="jValuePath"></param>
    /// <returns>Converted path</returns>
    private string ConvertPath(string jValuePath)
    {
        return string.Join(".",
            jValuePath.Split(".").Select(field => field.Split("[")[0]));
    }

    private string GetRootObject(string jValuePath, JObject data)
    {
        var rootObjectPath = jValuePath.Split(".")[0];
        return JsonConvert.SerializeObject(data.SelectToken(rootObjectPath));;
    }

    private bool IsFullMatch(string s1, string s2)
    {
        return String.Equals(s1.Trim(), s2.Trim(), StringComparison.CurrentCultureIgnoreCase);
    }

    private bool IsPartialMatch(string s1, string s2)
    {
        return s2.Contains(s1);
    }

    private int GetWeight(string matchPath, bool isFullMatch)
    {
        var defaultWeight = WeightsConfig.Weights.FirstOrDefault(x => x.Path == matchPath)?.Value ?? 0;
        return isFullMatch ? defaultWeight * 10 : defaultWeight;
    }
}