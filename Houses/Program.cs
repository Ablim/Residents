namespace Houses;

public class Program
{
    public static void Main(string[] args)
    {
        var colors = new[] { Color.Blue, Color.Green, Color.Red, Color.White, Color.Yellow };
        var residents = new[] { Resident.Englishman, Resident.Japanese, Resident.Norwegian, Resident.Spaniard, Resident.Ukraine };
        var pets = new[] { Pet.Dog, Pet.Fox, Pet.Horse, Pet.Snail, Pet.Zebra };
        var drinks = new[] { Drink.Coffee, Drink.Milk, Drink.The, Drink.Water, Drink.OrangeJuice };
        var smokes = new[] { Smoke.Chesterfield, Smoke.Kent, Smoke.Kool, Smoke.LuckyStrike, Smoke.OldGold };
        var houses = new[] { new House(), new House(), new House(), new House(), new House() };
        Loop(colors, houses);
    }

    private static void Loop(Color[] colors, House[] houses)
    {
        if (colors.Length == 0)
        {
            Console.WriteLine($"{houses[0].Color} {houses[1].Color} {houses[2].Color} {houses[3].Color} {houses[4].Color}");
            return;
        }

        var color = colors[0];
        var combinations = new List<House[]>();

        for (var i = 0; i < 5; i++)
        {
            var newHouses = new House[5];
            Array.Copy(houses, newHouses, houses.Length);
            
            if (newHouses[i].Color == Color.Default)
            {
                newHouses[i].Color = color;
                combinations.Add(newHouses);
            }
        }

        foreach (var combo in combinations)
        {
            Loop(colors[1..], combo);
        }
    }
    
    private bool Validate(House[] houses)
    {
        return false;
    }
}

