namespace Houses;

public class Program
{
    public static void Main(string[] args)
    {
        // var colors = new[] { Color.Blue, Color.Green, Color.Red, Color.White, Color.Yellow };
        // var residents = new[] { Resident.Englishman, Resident.Japanese, Resident.Norwegian, Resident.Spaniard, Resident.Ukraine };
        var pets = new[] { Pet.Dog, Pet.Fox, Pet.Horse, Pet.Snail, Pet.Zebra };
        var drinks = new[] { Drink.Coffee, Drink.Milk, Drink.The, Drink.Water, Drink.OrangeJuice };
        var smokes = new[] { Smoke.Chesterfield, Smoke.Kent, Smoke.Kool, Smoke.LuckyStrike, Smoke.OldGold };
        var houses = new[] { new House(), new House(), new House(), new House(), new House() };

        var properties = new[]
        {
            new Property { Type = typeof(Color), Value = Color.Blue },
            new Property { Type = typeof(Color), Value = Color.Green },
            new Property { Type = typeof(Color), Value = Color.Red },
            new Property { Type = typeof(Color), Value = Color.White },
            new Property { Type = typeof(Color), Value = Color.Yellow },
            new Property { Type = typeof(Resident), Value = Resident.Englishman },
            new Property { Type = typeof(Resident), Value = Resident.Japanese },
            new Property { Type = typeof(Resident), Value = Resident.Norwegian },
            new Property { Type = typeof(Resident), Value = Resident.Spaniard },
            new Property { Type = typeof(Resident), Value = Resident.Ukraine },
        };
        
        var candidates = new List<House[]>();
        Loop(properties, houses, candidates);
        Console.WriteLine(candidates.Count);
    }

    private static void Loop(Property[] properties, House[] houses, List<House[]> candidates)
    {
        if (properties.Length == 0)
        {
            if (IsCandidate(houses))
                candidates.Add(houses);
            
            return;
        }

        var combinations = new List<House[]>();
        var property = properties[0];

        for (var i = 0; i < 5; i++)
        {
            var newHouses = new House[5];
            Array.Copy(houses, newHouses, houses.Length);

            if (property.Type == typeof(Color) && newHouses[i].Color == Color.Default)
            {
                newHouses[i].Color = (Color)(property.Value ?? throw new InvalidOperationException());
                combinations.Add(newHouses);
            }
            
            if (property.Type == typeof(Resident) && newHouses[i].Resident == Resident.Default)
            {
                newHouses[i].Resident = (Resident)(property.Value ?? throw new InvalidOperationException());
                combinations.Add(newHouses);
            }
        }

        foreach (var combo in combinations)
        {
            Loop(properties[1..], combo, candidates);
        }
    }
    
    private static bool IsCandidate(House[] houses)
    {
        return true;
    }
}