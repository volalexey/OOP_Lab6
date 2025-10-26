namespace Lab_6
{
    public class Planet
    {
        private static int _planetCount = 0;
        public static int PlanetCount { get { return _planetCount; } set { _planetCount = value; } }

        public static string Galaxy { get; set; } = "Milky Way";

        private PlanetType _type;
        private string _name;
        private double _mass;
        private double _radius;
        private double _distanceFromSun;
        private bool _hasLife;
        private int _age;

        public double OrbitalSpeed { get; private set; } = 29.78;

        public PlanetType Type
        {
            get => _type;
            set
            {
                if (!Enum.IsDefined(typeof(PlanetType), value))
                    throw new ArgumentException("Invalid planet type. Must be 0-3.");
                _type = value;
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrEmpty(value) || value.Length > 30)
                    throw new ArgumentException("Name must be 1–30 characters.");
                _name = value;
            }
        }

        public double Mass
        {
            get => _mass;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Mass must be positive.");
                _mass = value;
            }
        }

        public double Radius
        {
            get => _radius;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Radius must be positive.");
                _radius = value;
            }
        }

        public int Age
        {
            get => _age;
            private set => _age = value;
        }

        public bool HasLife
        {
            get => _hasLife;
            set => _hasLife = value;
        }

        public double DistanceFromSun
        {
            get => _distanceFromSun;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Distance must be >= 0.");
                _distanceFromSun = value;
            }
        }

        public double DistanceTraveled => OrbitalSpeed * Age * 60 * 60 * 24 * 365;
        public Planet(PlanetType type, string name, double mass, double radius, double distanceFromSun, bool hasLife)
        {
            Type = type;
            Name = name;
            Mass = mass;
            Radius = radius;
            DistanceFromSun = distanceFromSun;
            HasLife = hasLife;
            Age = 0;
            _planetCount++;
        }

        public Planet()
        {
            Type = PlanetType.Terrestrial;
            Name = ".";
            Mass = 1;
            Radius = 1;
            DistanceFromSun = 0;
            HasLife = false;
            Age = 0;
            _planetCount++;
        }

        public Planet(PlanetType type, string name)
            : this(type, name, 5.97e24, 6.371e6, 150e6, false) { }

        public Planet(Random rnd)
        {
            string[] names = { "Xenon", "Astra", "Orbis", "Nova", "Kronos", "Zephyr" };
            Type = (PlanetType)rnd.Next(0, 4);
            Name = names[rnd.Next(names.Length)] + rnd.Next(100, 999);
            Mass = rnd.NextDouble() * 1e25 + 1e22;
            Radius = rnd.NextDouble() * 7e7 + 1e6;
            DistanceFromSun = rnd.NextDouble() * 5000;
            HasLife = rnd.Next(0, 2) == 1;
            Age = 0;
            _planetCount++;
        }
        public static string ShowUniverseInfo()
        {
            return $"In galaxy {Galaxy} there are currently {PlanetCount} planets.";
        }

        public static Planet Parse(string data)
        {
            string[] parts = data.Split('|');
            if (parts.Length != 6)
                throw new FormatException("Invalid format: must contain 6 parts (type|name|mass|radius|distance|hasLife).");

            Planet planet = new Planet();

            try
            {
                planet.Type = (PlanetType)Enum.Parse(typeof(PlanetType), parts[0]);
                planet.Name = parts[1];
                planet.Mass = double.Parse(parts[2]);
                planet.Radius = double.Parse(parts[3]);
                planet.DistanceFromSun = double.Parse(parts[4]);
                planet.HasLife = bool.Parse(parts[5]);
            }
            catch (Exception ex)
            {
                throw new FormatException($"Error parsing planet data: {ex.Message}");
            }

            return planet;
        }

        public static bool TryParse(string data, out Planet result, out string error)
        {
            try
            {
                result = Parse(data);
                error = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                result = null!;
                error = ex.Message;
                return false;
            }
        }

        public static void PlanetDestroyed()
        {
            _planetCount--;
        }

        public double GetGravity() => CalculateGravity();

        private double CalculateGravity()
        {
            const double G = 6.67430e-11;
            return G * Mass / (Radius * Radius);
        }

        public string MakeYearPass()
        {
            Age++;
            return $"{Name} made a full circle around the Sun. Age is now {Age} years.";
        }

        public string MakeYearPass(int years)
        {
            Age += years;
            return $"{Name} made {years} circles around the Sun. Age is now {Age}.";
        }

        public string BirthLife()
        {
            if (HasLife) return $"Error: life already exists on {Name}!";
            HasLife = true;
            return $"Life has begun on {Name}!";
        }

        public string DestroyLife()
        {
            if (!HasLife) return $"Error: {Name} already has no life!";
            HasLife = false;
            return $"Life on {Name} has been destroyed...";
        }
        public override string ToString()
        {
            return $"{Type}|{Name}|{Mass}|{Radius}|{DistanceFromSun}|{HasLife}";
        }

        public string ToTableString()
        {
            return string.Format(
                "{0,-12} | {1,-12} | {2,12:E2} | {3,12:E2} | {4,12:E2} | {5,-3} | {6,5} | {7,12:F2} | {8,12:E2}",
                Name,
                Type,
                Mass,
                Radius,
                DistanceFromSun,
                HasLife ? "Yes" : "No",
                Age,
                OrbitalSpeed,
                DistanceTraveled
            );
        }
    }
}
