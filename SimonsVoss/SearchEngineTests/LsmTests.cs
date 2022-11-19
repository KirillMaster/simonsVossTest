using Newtonsoft.Json.Linq;
using Services;

namespace SearchEngineTests;

public class Tests
{

    private LsmRespository repository;
    [SetUp]
    public void Setup()
    {
        repository = new LsmRespository();
    }

    [Test]
    public void Test1()
    {
        var result = repository.Get();
        Assert.NotNull(result);
    }
}