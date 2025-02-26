using Fabrik;
using NUnit.Framework;

[TestFixture]
public class MachineTests
{
    private Machine machine;

    [SetUp]
    public void Setup()
    {
        machine = new Machine();
    }

    [Test]
    public void Maschine_Startet_Von_Ready_Nach_Running()
    {
        machine.Start();
        Assert.That(machine.IsRunning(), Is.True);
    }

    [Test]
    public void Maschine_Stoppt_Von_Running_Nach_Ready()
    {
        machine.Start();
        machine.Stop();
        Assert.That(machine.IsRunning(), Is.False);
    }

    [Test]
    public void Maschine_Geht_In_Error_Wenn_Fail_Ausgeführt_Wird()
    {
        machine.Start();
        machine.Fail();
        Assert.That(machine.IsRunning(), Is.False);
    }

    [Test]
    public void Maschine_Kann_Nach_Error_Zurückgesetzt_Werden()
    {
        machine.Start();
        machine.Fail();
        machine.Reset();
        machine.Start();
        Assert.That(machine.IsRunning(), Is.True);
    }
}