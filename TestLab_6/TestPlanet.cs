using Lab_6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TestLab_6
{
    [TestClass]
    public class TestPlanet
    {
        private Planet _planet;

        [TestInitialize]
        public void Setup()
        {
            _planet = new Planet(PlanetType.Terrestrial, "Earth", 5.97e24, 6.371e6, 150e6, true);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _planet = null!;
        }

        // constructors

        [TestMethod]
        public void DefaultConstructor_ShouldInitializeWithDefaultValues()
        {
            // Act
            var planet = new Planet();

            // Assert
            Assert.AreEqual(PlanetType.Terrestrial, planet.Type);
            Assert.AreEqual(".", planet.Name);
            Assert.AreEqual(1, planet.Mass);
            Assert.AreEqual(1, planet.Radius);
            Assert.AreEqual(0, planet.DistanceFromSun);
            Assert.IsFalse(planet.HasLife);
            Assert.AreEqual(0, planet.Age);
        }

        [TestMethod]
        public void FullConstructor_ShouldAssignAllValuesCorrectly()
        {
            // Arrange + Act
            var planet = new Planet(PlanetType.GasGiant, "Jupiter", 1.9e27, 69911e3, 778e6, false);

            // Assert
            Assert.AreEqual(PlanetType.GasGiant, planet.Type);
            Assert.AreEqual("Jupiter", planet.Name);
            Assert.AreEqual(1.9e27, planet.Mass);
            Assert.AreEqual(69911e3, planet.Radius);
            Assert.AreEqual(778e6, planet.DistanceFromSun);
            Assert.IsFalse(planet.HasLife);
        }

        [TestMethod]
        public void Constructor_WithTypeAndName_ShouldUseThisConstructor()
        {
            // Act
            var planet = new Planet(PlanetType.IceGiant, "Neptune");

            // Assert
            Assert.AreEqual(PlanetType.IceGiant, planet.Type);
            Assert.AreEqual("Neptune", planet.Name);
            Assert.AreEqual(5.97e24, planet.Mass);
            Assert.AreEqual(6.371e6, planet.Radius);
            Assert.AreEqual(150e6, planet.DistanceFromSun);
        }

        // properties

        [TestMethod]
        [DataRow("Mars")]
        [DataRow("Venus")]
        public void Name_SetValidValue_ShouldAssignCorrectly(string validName)
        {
            // Act
            _planet.Name = validName;

            // Assert
            Assert.AreEqual(validName, _planet.Name);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(null)]
        [DataRow("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA")]
        [ExpectedException(typeof(ArgumentException))]
        public void Name_SetInvalidValue_ShouldThrowException(string invalidName)
        {
            // Act
            _planet.Name = invalidName;
        }

        [TestMethod]
        public void Mass_SetValidValue_ShouldAssignCorrectly()
        {
            // Act
            _planet.Mass = 2.97e22;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Mass_SetZero_ShouldThrowException()
        {
            // Act
            _planet.Mass = 0;
        }

        [TestMethod]
        [DataRow(100)]
        [DataRow(3.55e11)]
        public void Radius_SetValidValue_ShouldAssignCorrectly(double validRadius)
        {
            // Act
            _planet.Radius = validRadius;
        }

        [TestMethod]
        [DataRow(-10)]
        [DataRow(-1)]
        [ExpectedException(typeof(ArgumentException))]
        public void Radius_SetNegative_ShouldThrowException(double invalidRadius)
        {
            // Act
            _planet.Radius = invalidRadius;
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(3333)]
        public void DistanceFromSun_SetValidValue_ShouldAssignCorrectly(double validDistance)
        {
            // Act
            _planet.DistanceFromSun = validDistance;
        }

        [TestMethod]
        [DataRow(-100)]
        [ExpectedException(typeof(ArgumentException))]
        public void DistanceFromSun_Negative_ShouldThrowException(double invalidValue)
        {
            // Act
            _planet.DistanceFromSun = invalidValue;
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(2)]
        public void Type_SetValidValue_ShouldAssignCorrectly(double validType)
        {
            // Act
            _planet.Type = (PlanetType)validType;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Type_SetInvalidEnum_ShouldThrowException()
        {
            // Act
            _planet.Type = (PlanetType)999;
        }

        // methods

        [TestMethod]
        public void GetGravity_ShouldReturnPositiveValue()
        {
            // Act
            double gravity = _planet.GetGravity();

            // Assert
            Assert.IsTrue(gravity > 0);
        }

        [TestMethod]
        public void MakeYearPass_ShouldIncreaseAgeBy1()
        {
            // Arrange
            int initialAge = _planet.Age;

            // Act
            _planet.MakeYearPass();

            // Assert
            Assert.AreEqual(initialAge + 1, _planet.Age);
        }

        [TestMethod]
        [DataRow(3)]
        [DataRow(10)]
        public void MakeYearPass_WithYears_ShouldIncreaseAgeByGivenAmount(int years)
        {
            // Arrange
            int initialAge = _planet.Age;

            // Act
            _planet.MakeYearPass(years);

            // Assert
            Assert.AreEqual(initialAge + years, _planet.Age);
        }

        [TestMethod]
        public void BirthLife_WhenNoLife_ShouldEnableLife()
        {
            // Arrange
            _planet.HasLife = false;

            // Act
            string result = _planet.BirthLife();

            // Assert
            Assert.IsTrue(_planet.HasLife);
            StringAssert.Contains(result, "Life has begun");
        }

        [TestMethod]
        public void BirthLife_WhenLifeExists_ShouldReturnErrorMessage()
        {
            // Arrange
            _planet.HasLife = true;

            // Act
            string result = _planet.BirthLife();

            // Assert
            Assert.AreEqual("Error: life already exists on Earth!", result);
        }

        [TestMethod]
        public void DestroyLife_WhenLifeExists_ShouldDisableLife()
        {
            // Arrange
            _planet.HasLife = true;

            // Act
            string result = _planet.DestroyLife();

            // Assert
            Assert.IsFalse(_planet.HasLife);
            StringAssert.Contains(result, "destroyed");
        }

        [TestMethod]
        public void DestroyLife_WhenNoLife_ShouldReturnErrorMessage()
        {
            // Arrange
            _planet.HasLife = false;

            // Act
            string result = _planet.DestroyLife();

            // Assert
            StringAssert.Contains(result, "already has no life");
        }

        // Static methods

        [TestMethod]
        public void StaticShowUniverseInfo_ShouldContainGalaxyAndCount()
        {
            // Act
            string result = Planet.ShowUniverseInfo();

            // Assert
            StringAssert.Contains(result, Planet.Galaxy);
            StringAssert.Contains(result, Planet.PlanetCount.ToString());
        }

        [TestMethod]
        public void PlanetDestroyed_ShouldDecreasePlanetCount()
        {
            // Arrange
            int before = Planet.PlanetCount;

            // Act
            Planet.PlanetDestroyed();

            // Assert
            Assert.AreEqual(before - 1, Planet.PlanetCount);
        }

        [TestMethod]
        [DataRow("Terrestrial|Mars|6,4e23|3390e3|228e6|false")]
        [DataRow("GasGiant|Jupiter|1,9e27|69911e3|778e6|false")]
        public void Parse_ValidData_ShouldReturnPlanet(string data)
        {
            // Act
            Planet parsed = Planet.Parse(data);

            // Assert
            Assert.IsNotNull(parsed);
            StringAssert.Contains(parsed.ToString(), "|");
        }

        [TestMethod]
        [DataRow("Invalid|Data|123")]
        [DataRow("Terrestrial|Earth|badMass|radius|distance|true")]
        [ExpectedException(typeof(FormatException))]
        public void Parse_InvalidData_ShouldThrowFormatException(string data)
        {
            // Act
            Planet.Parse(data);
        }

        [TestMethod]
        public void TryParse_ValidData_ShouldReturnTrue()
        {
            // Arrange
            string data = "GasGiant|Jupiter|1,9e27|69911e3|778e6|false";

            // Act
            bool success = Planet.TryParse(data, out Planet result, out string error);

            // Assert
            Assert.IsTrue(success);
            Assert.AreEqual(string.Empty, error);
            Assert.AreEqual("Jupiter", result.Name);
        }

        [TestMethod]
        public void TryParse_InvalidData_ShouldReturnFalse()
        {
            // Arrange
            string data = "Bad|Data";

            // Act
            bool success = Planet.TryParse(data, out Planet result, out string error);

            // Assert
            Assert.IsFalse(success);
            Assert.IsNotNull(error);
        }

        // ToString methods

        [TestMethod]
        public void ToString_ShouldReturnPipeSeparatedValues()
        {
            // Act
            string result = _planet.ToString();

            // Assert
            StringAssert.Contains(result, "|");
        }

        [TestMethod]
        public void ToTableString_ShouldContainFormattedValues()
        {
            // Act
            string result = _planet.ToTableString();

            // Assert
            StringAssert.Contains(result, "|");
            StringAssert.Contains(result, _planet.Age.ToString());
        }
    }
}
