namespace BusinessLogic;

public class Weight
{
    public string Path { get; set; }
    public int Value { get; set; }
    public List<Weight> TransitiveWeights { get; set; }
}