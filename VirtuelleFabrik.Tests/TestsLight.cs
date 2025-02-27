using Fabrik;
using NUnit.Framework;
using System.Reflection.PortableExecutable;

namespace Fabrik
{
    [TestFixture]
    public class SignalLightTests
    {
        private SignalLight signalLight;

        [SetUp]
        public void Setup()
        {
            signalLight = new SignalLight();
        }

        [Test]
        public void SignalLight_SetState_To_Green()
        {
            signalLight.SetState(SignalLight.State.Green);
            Assert.That(signalLight.GetState(), Is.EqualTo(SignalLight.State.Green));
        }

        [Test]
        public void SignalLight_SetState_To_Yellow()
        {
            signalLight.SetState(SignalLight.State.Yellow);
            Assert.That(signalLight.GetState(), Is.EqualTo(SignalLight.State.Yellow));
        }

        [Test]
        public void SignalLight_SetState_To_Red()
        {
            signalLight.SetState(SignalLight.State.Red);
            Assert.That(signalLight.GetState(), Is.EqualTo(SignalLight.State.Red));
        }
    }
}
