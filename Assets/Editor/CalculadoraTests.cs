using NUnit.Framework;

public class CalculadoraTests
{
    private Calculadora calc;

    [SetUp]
    public void Setup()
    {
        calc = new Calculadora();
    }

    [Test]// AAA
    public void Sumar_DosNumerosPositivos()
    {
        // Arrange
        var calc = new Calculadora();
        int a = 5, b = 3;

        // Act
        int resultado = calc.Sumar(a, b);

        // Assert
        Assert.AreEqual(8, resultado);
    }

    [Test]
    public void Restar_DosNumeros()
    {
        int resultado = calc.Restar(10, 4);
        Assert.AreEqual(6, resultado);
    }

    [Test]
    public void Multiplicar_DosNumeros()
    {
        int resultado = calc.Multiplicar(7, 6);
        Assert.AreEqual(42, resultado);
    }

    [Test]
    public void Dividir_OperacionValida()
    {
        float resultado = calc.Dividir(10, 2);
        Assert.AreEqual(5f, resultado);
    }

    [Test]
    public void Dividir_EntreCero_LanzaExcepcion()
    {
        Assert.Throws<System.DivideByZeroException>(() => calc.Dividir(10, 0));
    }
}
