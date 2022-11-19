using Newtonsoft.Json.Linq;

namespace Services;

public class LsmRespository
{
    public JObject Get()
    {
        return JObject.Parse(File.ReadAllText("Database/Database.json"));
    }
}