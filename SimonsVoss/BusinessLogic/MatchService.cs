using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BusinessLogic;

public class MatchService
{
    public IEnumerable<Match> GetMatches(string searchValue, JObject data)
    {
        //TODO:: Add support for partial matches
        return data
            .Descendants()
            .OfType<JValue>()
            .Where(p => p.Value != null)
            .Where(p => p.Value.Equals(searchValue)).Select(x => new Match
            {
                Path = ConvertPath(x.Path),
                Value = x.Value.ToString()
            });
    }

    /// <summary>
    /// Converts jObjectPath with indexes to abstract path.
    /// Example: buildings[0].name -> buildings.name
    /// </summary>
    /// <param name="jObjectPath"></param>
    /// <returns>Converted path</returns>
    private string ConvertPath(string jObjectPath)
    {
        return string.Join(".",
            jObjectPath.Split(".").Select(field => field.Split("[")[0]));
    }

    public WeightsConfig GetWeightsConfig()
    {
        return JsonConvert.DeserializeObject<WeightsConfig>(File.ReadAllText("weights_config.json"));
    }
}