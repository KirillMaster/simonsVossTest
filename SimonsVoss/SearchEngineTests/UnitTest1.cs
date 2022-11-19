using Newtonsoft.Json.Linq;

namespace SearchEngineTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
       var result= JObject.Parse(File.ReadAllText("Database.json"));
       Assert.NotNull(result);
    }
}