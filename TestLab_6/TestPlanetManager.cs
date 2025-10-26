using Lab_6;

namespace TestLab_6
{
    [TestClass]
    public class TestPlanetManager
    {
        private const int TEST_MAX_PLANETS = 3;

        [TestInitialize]
        public void Initialize()
        {
            Program.maxPlanets = TEST_MAX_PLANETS;
            Program.planets = new List<Planet>();
            Program.rnd = new System.Random();

            Planet.PlanetCount = 0;
        }

        [TestCleanup]
        public void Cleanup()
        {
            Program.planets.Clear();

            Planet.PlanetCount = 0;
        }

        [TestMethod]
        public void AddPlanet_WhenNotFull_ShouldAddPlanetAndReturnTrue()
        {
            // Arrange
            var planet = new Planet(PlanetType.Terrestrial, "TestPlanet");

            // Act
            bool result = Program.AddPlanet(planet);
            int count = Program.GetPlanetCount();

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, count);
            Assert.IsTrue(Program.planets.Contains(planet));
        }

        [TestMethod]
        public void AddPlanet_WhenFull_ShouldNotAddPlanetAndReturnFalse()
        {
            // Arrange
            Program.AddPlanet(new Planet(PlanetType.Terrestrial, "Test1"));
            Program.AddPlanet(new Planet(PlanetType.Terrestrial, "Test2"));
            Program.AddPlanet(new Planet(PlanetType.Terrestrial, "Test3"));

            var extraPlanet = new Planet(PlanetType.Terrestrial, "Test4_Extra");

            // Act
            bool result = Program.AddPlanet(extraPlanet);
            int count = Program.GetPlanetCount();

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(TEST_MAX_PLANETS, count);
        }

        [TestMethod]
        public void AddPlanetFromString_WhenFull_ShouldReturnFalse()
        {
            // Arrange
            Program.AddPlanet(new Planet(PlanetType.Terrestrial, "Test1"));
            Program.AddPlanet(new Planet(PlanetType.Terrestrial, "Test2"));
            Program.AddPlanet(new Planet(PlanetType.Terrestrial, "Test3"));

            string validData = "Terrestrial|Mars|6,4e23|3390e3|228e6|false";

            // Act
            bool result = Program.AddPlanetFromString(validData, out Planet parsedPlanet, out string error);
            int count = Program.GetPlanetCount();

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(TEST_MAX_PLANETS, count);
            Assert.IsNull(parsedPlanet);
            Assert.AreEqual("Max planets reached.", error);
        }

        [TestMethod]
        public void AddPlanetFromString_WithValidString_ShouldAddPlanetAndReturnTrue()
        {
            // Arrange
            string validData = "Terrestrial|Mars|6,4e23|3390e3|228e6|false";

            // Act
            bool result = Program.AddPlanetFromString(validData, out Planet parsedPlanet, out string error);
            int count = Program.GetPlanetCount();

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, count);
            Assert.IsNotNull(parsedPlanet);
            Assert.AreEqual("Mars", parsedPlanet.Name);
            Assert.IsTrue(string.IsNullOrEmpty(error));
        }

        [TestMethod]
        public void FindPlanetsByName_WhenPlanetExists_ShouldReturnList()
        {
            // Arrange
            Program.AddPlanet(new Planet(PlanetType.Terrestrial, "Earth"));
            Program.AddPlanet(new Planet(PlanetType.Terrestrial, "Mars"));
            Program.AddPlanet(new Planet(PlanetType.Terrestrial, "earth"));

            // Act
            var result = Program.FindPlanetsByName("Earth");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void FindPlanetsByName_WhenPlanetNotExist_ShouldReturnEmptyList()
        {
            // Arrange
            Program.AddPlanet(new Planet(PlanetType.Terrestrial, "Mars"));

            // Act
            var result = Program.FindPlanetsByName("Earth");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void DeletePlanetsByName_WhenPlanetExists_ShouldRemoveThemAndReturnCount()
        {
            // Arrange
            Program.AddPlanet(new Planet(PlanetType.Terrestrial, "Earth"));
            Program.AddPlanet(new Planet(PlanetType.Terrestrial, "Mars"));
            Program.AddPlanet(new Planet(PlanetType.Terrestrial, "EARTH"));

            // Act
            int removedCount = Program.DeletePlanetsByName("Earth");
            int finalCount = Program.GetPlanetCount();
            int staticPlanetCount = Planet.PlanetCount;

            // Assert
            Assert.AreEqual(2, removedCount);
            Assert.AreEqual(1, finalCount); 
            Assert.AreEqual(1, staticPlanetCount);
            Assert.AreEqual("Mars", Program.planets[0].Name);
        }
    }
}