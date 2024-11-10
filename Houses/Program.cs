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
            new Property { Type = typeof(Resident), Value = Resident.England },
            new Property { Type = typeof(Resident), Value = Resident.Japan },
            new Property { Type = typeof(Resident), Value = Resident.Norway },
            new Property { Type = typeof(Resident), Value = Resident.Spain },
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
        var waterDrinker = candidates.SelectMany(h => h).FirstOrDefault(h => h.Drink == Drink.Water).Resident;
        Console.WriteLine($"The water drinker is from {waterDrinker}");
        var zebraOwner = candidates.SelectMany(h => h).FirstOrDefault(h => h.Pet == Pet.Zebra).Resident;
        Console.WriteLine($"The zebra owner is from {zebraOwner}");
    }

    private static void Loop(Property[] properties, House[] houses, List<House[]> candidates)
    {
        if (DateTime.Now.Second == _second)
        {
            Console.WriteLine(candidates.Count);
            _second = (_second + 1) % 60;
        }

        if (IsCandidate(houses))
        {
            if (properties.Length == 0)
            {
                candidates.Add(houses);
                return;
            }
        }
        else
        {
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
        for (var i = 0; i < houses.Length; i++)
        {
            if (houses[i].Resident is Resident.England && houses[i].Color is not (Color.Red or Color.Default))
                return false;
            if (houses[i].Color is Color.Red && houses[i].Resident is not (Resident.England or Resident.Default))
                return false;
            if (houses[i].Resident is Resident.Spain && houses[i].Pet is not (Pet.Dog or Pet.Default))
                return false;
            if (houses[i].Pet is Pet.Dog && houses[i].Resident is not (Resident.Spain or Resident.Default))
                return false;
            if (houses[i].Color is Color.Green && houses[i].Drink is not (Drink.Coffee or Drink.Default))
                return false;
            if (houses[i].Drink is Drink.Coffee && houses[i].Color is not (Color.Green or Color.Default))
                return false;
            if (houses[i].Resident is Resident.Ukraine && houses[i].Drink is not (Drink.The or Drink.Default))
                return false;
            if (houses[i].Drink is Drink.The && houses[i].Resident is not (Resident.Ukraine or Resident.Default))
                return false;
            if (i > 0 && houses[i].Color is Color.Green && houses[i - 1].Color is not (Color.White or Color.Default))
                return false;
            if (i == 0 && houses[i].Color is Color.Green)
                return false;
            if (i < 4 && houses[i].Color is Color.White && houses[i + 1].Color is not (Color.Green or Color.Default))
                return false;
            if (i == 4 && houses[i].Color is Color.White)
                return false;
            if (houses[i].Smoke is Smoke.OldGold && houses[i].Pet is not (Pet.Snail or Pet.Default))
                return false;
            if (houses[i].Pet is Pet.Snail && houses[i].Smoke is not (Smoke.OldGold or Smoke.Default))
                return false;
            if (houses[i].Smoke is Smoke.Kool && houses[i].Color is not (Color.Yellow or Color.Default))
                return false;
            if (houses[i].Color is Color.Yellow && houses[i].Smoke is not (Smoke.Kool or Smoke.Default))
                return false;
            if (i == 2 && houses[i].Drink is not (Drink.Milk or Drink.Default))
                return false;
            if (i != 2 && houses[i].Drink is Drink.Milk)
                return false;
            if (i == 0 && houses[i].Resident is not (Resident.Norway or Resident.Default))
                return false;
            if (i != 0 && houses[i].Resident is Resident.Norway)
                return false;
            if (houses[i].Smoke is Smoke.Chesterfield 
                && !(i > 0 && houses[i - 1].Pet is Pet.Fox or Pet.Default || i < 4 && houses[i + 1].Pet is Pet.Fox or Pet.Default))
                return false;
            if (houses[i].Pet is Pet.Fox 
                && !(i > 0 && houses[i - 1].Smoke is Smoke.Chesterfield or Smoke.Default || i < 4 && houses[i + 1].Smoke is Smoke.Chesterfield or Smoke.Default))
                return false;
            if (houses[i].Smoke is Smoke.Kool 
                && !(i > 0 && houses[i - 1].Pet is Pet.Horse or Pet.Default || i < 4 && houses[i + 1].Pet is Pet.Horse or Pet.Default))
                return false;
            if (houses[i].Pet is Pet.Horse 
                && !(i > 0 && houses[i - 1].Smoke is Smoke.Kool or Smoke.Default || i < 4 && houses[i + 1].Smoke is Smoke.Kool or Smoke.Default))
                return false;
            if (houses[i].Smoke is Smoke.LuckyStrike && houses[i].Drink is not (Drink.OrangeJuice or Drink.Default))
                return false;
            if (houses[i].Drink is Drink.OrangeJuice && houses[i].Smoke is not (Smoke.LuckyStrike or Smoke.Default))
                return false;
            if (houses[i].Smoke is Smoke.Kent && houses[i].Resident is not (Resident.Japan or Resident.Default))
                return false;
            if (houses[i].Resident is Resident.Japan && houses[i].Smoke is not (Smoke.Kent or Smoke.Default))
                return false;
            if (houses[i].Resident is Resident.Norway
                && !(i > 0 && houses[i - 1].Color is Color.Blue or Color.Default || i < 4 && houses[i + 1].Color is Color.Blue or Color.Default))
                return false;
            if (houses[i].Color is Color.Blue
                && !(i > 0 && houses[i - 1].Resident is Resident.Norway or Resident.Default || i < 4 && houses[i + 1].Resident is Resident.Norway or Resident.Default))
                return false;
        }
        
        return true;
    }
}