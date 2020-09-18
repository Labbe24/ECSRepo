using ECS;
using NUnit.Framework;

namespace ECS.Test.Unit
{
    [TestFixture]
    public class ECSTests
    {

        [Test]
        public void Regulates_UnderThreshold_ReturnTurnOnCalledTrue()
        {
            // Arrange
            FakeHeater fakeHeater = new FakeHeater();
            FakeTempSensor fakeTempSensor = new FakeTempSensor();
            fakeTempSensor.SetTemp(20);
            ECSClass ecs = new ECSClass(fakeTempSensor, fakeHeater, 26);

            // Act
            ecs.Regulate();
            bool result = fakeHeater.TurnOnCalled;

            // Assert
            Assert.AreEqual(true, result);

        }

        [Test]
        public void Regulate_OverThreshold_ReturnTurnOnCalledFalse()
        {
            // Arrange
            FakeHeater fakeHeater = new FakeHeater();
            FakeTempSensor fakeTempSensor = new FakeTempSensor();
            fakeTempSensor.SetTemp(30);
            ECSClass ecs = new ECSClass(fakeTempSensor, fakeHeater, 25);

            // Act
            ecs.Regulate();
            bool result = fakeHeater.TurnOnCalled;

            // Assert
            Assert.AreEqual(false, result);

        }

        [Test]
        public void Regulate_OverThreshold_ReturnTurnOffCalledTrue()
        {
            // Arrange
            FakeHeater fakeHeater = new FakeHeater();
            FakeTempSensor fakeTempSensor = new FakeTempSensor();
            fakeTempSensor.SetTemp(30);
            ECSClass ecs = new ECSClass(fakeTempSensor, fakeHeater, 25);

            // Act
            ecs.Regulate();
            bool result = fakeHeater.TurnOffCalled;

            // Assert
            Assert.AreEqual(true, result);

        }

        [Test]
        public void Regulate_UnderThreshold_ReturnTurnOffCalledFalse()
        {
            // Arrange
            FakeHeater fakeHeater = new FakeHeater();
            FakeTempSensor fakeTempSensor = new FakeTempSensor();
            fakeTempSensor.SetTemp(20);
            ECSClass ecs = new ECSClass(fakeTempSensor, fakeHeater, 25);

            // Act
            ecs.Regulate();
            bool result = fakeHeater.TurnOffCalled;

            // Assert
            Assert.AreEqual(false, result);

        }

        [Test]
        public void RunSelfTest_HeaterSelfTestTureTempSensorSelfTestTrue_ReturnTrue()
        {
            // Arrange
            FakeHeater fakeHeater = new FakeHeater();
            FakeTempSensor fakeTempSensor = new FakeTempSensor();
            fakeTempSensor.RunSelfTestBool = true;
            fakeHeater.RunSelfTestBool = true;
            ECSClass ecs = new ECSClass(fakeTempSensor, fakeHeater, 20);

            // Act
            bool result = ecs.RunSelfTest();

            // Assert
            Assert.AreEqual(true, result);

        }

        [Test]
        public void RunSelfTest_HeaterSelfTestTureTempSensorSelfTestFalse_ReturnFalse()
        {
            // Arrange
            FakeHeater fakeHeater = new FakeHeater();
            FakeTempSensor fakeTempSensor = new FakeTempSensor();
            fakeTempSensor.RunSelfTestBool = false;
            fakeHeater.RunSelfTestBool = true;
            ECSClass ecs = new ECSClass(fakeTempSensor, fakeHeater, 20);

            // Act
            bool result = ecs.RunSelfTest();

            // Assert
            Assert.AreEqual(false, result);

        }

        [Test]
        public void RunSelfTest_HeaterSelfTestFalseTempSensorSelfTestTrue_ReturnFalse()
        {
            // Arrange
            FakeHeater fakeHeater = new FakeHeater();
            FakeTempSensor fakeTempSensor = new FakeTempSensor();
            fakeTempSensor.RunSelfTestBool = true;
            fakeHeater.RunSelfTestBool = false;
            ECSClass ecs = new ECSClass(fakeTempSensor, fakeHeater, 20);

            // Act
            bool result = ecs.RunSelfTest();

            // Assert
            Assert.AreEqual(false, result);

        }

        [Test]
        public void RunSelfTest_HeaterSelfTestFalseTempSensorSelfTestFalse_ReturnFalse()
        {
            // Arrange
            FakeHeater fakeHeater = new FakeHeater();
            FakeTempSensor fakeTempSensor = new FakeTempSensor();
            fakeTempSensor.RunSelfTestBool = false;
            fakeHeater.RunSelfTestBool = false;
            ECSClass ecs = new ECSClass(fakeTempSensor, fakeHeater, 20);

            // Act
            bool result = ecs.RunSelfTest();

            // Assert
            Assert.AreEqual(false, result);

        }
    }

    internal class FakeHeater : IHeater
    {
        public bool TurnOnCalled
        {
            get;
            set;
        } = false;

        public bool TurnOffCalled
        {
            get;
            set;
        } = false;

        public bool RunSelfTestBool
        {
            get;
            set;
        } = false;

        public void TurnOn()
        {
            TurnOnCalled = true;
        }

        public void TurnOff()
        {
            TurnOffCalled = true;
        }

        public bool RunSelfTest()
        {
            return RunSelfTestBool;
        }
    }

    internal class FakeTempSensor : ITempSensor
    {
        private int _temp;

        public bool RunSelfTestBool
        {
            get;
            set;
        } = false;
        public int GetTemp()
        {
            return _temp;
        }

        public void SetTemp(int temp)
        {
            _temp = temp;
        }

        public bool RunSelfTest()
        {
            return RunSelfTestBool;
        }
    }
}
