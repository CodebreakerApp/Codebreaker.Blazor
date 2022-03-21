namespace BlazorMM.Client.Models;

public record struct CodePeg(string Color)
{
    public override string ToString() => Color;
}

public record struct KeyPeg(string Color)
{
    public override string ToString() => Color;
}
