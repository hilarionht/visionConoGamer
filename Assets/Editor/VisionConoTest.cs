using NUnit.Framework;
using UnityEngine;

public class VisionConoTests
{
    [Test]
    public void ObjetivoDentroRangoYAngulo_CentroExacto()
    {
        Vector2 origen = Vector2.zero;
        Vector2 frente = Vector2.up;
        Vector2 objetivo = new Vector2(0f, 3f);
        bool resultado = VisionCono2.EstaEnVision(origen, frente, objetivo, 90f, 5f);
        Assert.IsTrue(resultado);
    }

    [Test]
    public void ObjetivoFueraDelRango()
    {
        Vector2 origen = Vector2.zero;
        Vector2 frente = Vector2.up;
        Vector2 objetivo = new Vector2(0f, 10f);
        bool resultado = VisionCono2.EstaEnVision(origen, frente, objetivo, 90f, 5f);
        Assert.IsFalse(resultado);
    }

    [Test]
    public void ObjetivoFueraDelAngulo()
    {
        Vector2 origen = Vector2.zero;
        Vector2 frente = Vector2.up;
        Vector2 objetivo = new Vector2(5f, 0f);
        bool resultado = VisionCono2.EstaEnVision(origen, frente, objetivo, 60f, 5f);
        Assert.IsFalse(resultado);
    }

    [Test]
    public void ObjetivoEnElBordeDelAngulo()
    {
        Vector2 origen = Vector2.zero;
        Vector2 frente = Vector2.up;
        Vector2 objetivo = new Vector2(1f, 1f); // 45 grados
        bool resultado = VisionCono2.EstaEnVision(origen, frente, objetivo, 90f, 5f);
        Assert.IsTrue(resultado);
    }

    //[Test]
    //public void ObjetivoEnOrigen()
    //{
    //    Vector2 origen = Vector2.zero;
    //    Vector2 frente = Vector2.up;
    //    Vector2 objetivo = Vector2.zero;
    //    bool resultado = VisionCono2.EstaEnVision(origen, frente, objetivo, 90f, 5f);
    //    Assert.IsTrue(resultado);
    //}

    [Test]
    public void FrenteNoNormalizado()
    {
        Vector2 origen = Vector2.zero;
        Vector2 frente = new Vector2(0f, 10f); // no unitario
        Vector2 objetivo = new Vector2(0f, 3f);
        bool resultado = VisionCono2.EstaEnVision(origen, frente, objetivo, 90f, 5f);
        Assert.IsTrue(resultado);
    }

    [Test]
    public void AnguloVisionCeroPerfectamenteAlineado()
    {
        Vector2 origen = Vector2.zero;
        Vector2 frente = Vector2.up;
        Vector2 objetivo = new Vector2(0f, 3f);
        bool resultado = VisionCono2.EstaEnVision(origen, frente, objetivo, 0f, 5f);
        Assert.IsTrue(resultado);
    }

    [Test]
    public void AnguloVisionCeroNoAlineado()
    {
        Vector2 origen = Vector2.zero;
        Vector2 frente = Vector2.up;
        Vector2 objetivo = new Vector2(1f, 0f);
        bool resultado = VisionCono2.EstaEnVision(origen, frente, objetivo, 0f, 5f);
        Assert.IsFalse(resultado);
    }
}
