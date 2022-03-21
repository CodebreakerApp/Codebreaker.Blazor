using BlazorMM.Server.Services;

using Xunit;

namespace ServerTests;

public class RandomGameGeneratorTests
{

    [Fact]
    public void StartGameReturnsRandomEnoughValues()
    {
        RandomGameGenerator generator = new();

        string[] colorList = { "white", "red", "green", "blue", "black", "yellow" };
        bool[] colorMatches = new bool[colorList.Length];
        bool allColors = false;
        int iterations = 0;
        do
        {
            iterations++;
            string[] colors = generator.GetColors(4);
            for (int i = 0; i < colorList.Length; i++)
            {
                if (colors.Contains(colorList[i]))
                {
                    colorMatches[i] = true;
                    allColors = colorMatches.All(c => c);
                }
            }
        } while (!allColors);
        Assert.True(iterations < 100);
    }
}
