using BusinessLogic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Services;

namespace SearchEngineTests;

public class Tests
{

    private LsmRespository repository;
    private MatchService matchService;
    [SetUp]
    public void Setup()
    {
        repository = new LsmRespository();
        matchService = new MatchService();
    }

    [Test]
    public void TestThatDatabaseFileCouldBeReadedSuccessfully()
    {
        var result = repository.Get();
        Assert.NotNull(result);
    }
    
    [Test]
    public void TestThatTreePathForMatchIsCorrect()
    {
        var result = repository.Get();
        var value = "Head Office";
        var token = matchService.GetMatches(value, result);
        
        Assert.AreEqual("buildings.name", string.Join(",", token.Select(x => x.Path)));
    }

    [Test]
    public void TestThatWeightsConfigParsesCorrectly()
    {
        var weights = matchService.GetWeightsConfig();
        Assert.NotNull(weights);
    }
}