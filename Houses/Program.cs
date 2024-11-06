namespace Houses;

public class Program
{
    private static int _second = 0;
    
    public static void Main(string[] args)
    {
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
            new Property { Type = typeof(Pet), Value = Pet.Dog },
            new Property { Type = typeof(Pet), Value = Pet.Fox },
            new Property { Type = typeof(Pet), Value = Pet.Horse },
            new Property { Type = typeof(Pet), Value = Pet.Snail },
            new Property { Type = typeof(Pet), Value = Pet.Zebra },
            new Property { Type = typeof(Drink), Value = Drink.Coffee },
            new Property { Type = typeof(Drink), Value = Drink.Milk },
            new Property { Type = typeof(Drink), Value = Drink.The },
            new Property { Type = typeof(Drink), Value = Drink.Water },
            new Property { Type = typeof(Drink), Value = Drink.OrangeJuice },
            new Property { Type = typeof(Smoke), Value = Smoke.Chesterfield },
            new Property { Type = typeof(Smoke), Value = Smoke.Kent },
            new Property { Type = typeof(Smoke), Value = Smoke.Kool },
            new Property { Type = typeof(Smoke), Value = Smoke.LuckyStrike },
            new Property { Type = typeof(Smoke), Value = Smoke.OldGold }
        };
        
        var candidates = new List<House[]>();
        Loop(properties, houses, candidates);
        Console.WriteLine(candidates.Count);
    }

    private static void Loop(Property[] properties, House[] houses, List<House[]> candidates)
    {
        if (DateTime.Now.Second == _second)
        {
            Console.WriteLine(candidates.Count);
            _second = (_second + 1) % 60;
        }
        
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
            else if (property.Type == typeof(Resident) && newHouses[i].Resident == Resident.Default)
            {
                newHouses[i].Resident = (Resident)(property.Value ?? throw new InvalidOperationException());
                combinations.Add(newHouses);
            }
            else if (property.Type == typeof(Pet) && newHouses[i].Pet == Pet.Default)
            {
                newHouses[i].Pet = (Pet)(property.Value ?? throw new InvalidOperationException());
                combinations.Add(newHouses);
            }
            else if (property.Type == typeof(Drink) && newHouses[i].Drink == Drink.Default)
            {
                newHouses[i].Drink = (Drink)(property.Value ?? throw new InvalidOperationException());
                combinations.Add(newHouses);
            }
            else if (property.Type == typeof(Smoke) && newHouses[i].Smoke == Smoke.Default)
            {
                newHouses[i].Smoke = (Smoke)(property.Value ?? throw new InvalidOperationException());
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