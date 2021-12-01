using NUnit.Framework;
using RpgAniAlieLib.Personagens;

namespace Tests
{
    public class Tests7
    {
        [SetUp]
        public void Setup7()
        {

        }

        [Test]
        public void Recarregar()
        {
            var yoshi = new Yoshi("pisada", "yoshi");
            yoshi.MedidorEspecial = 9;
            Assert.IsTrue(yoshi.RecarregarPe());
            yoshi.RecarregarPe();
            Assert.AreEqual(yoshi.MedidorEspecial, 10);
        }

        [Test]
        public void AtaqueTest()
        {
            var yoshi = new Yoshi("pisada", "yoshi");
            yoshi.MedidorEspecial = 9;
            yoshi.Atk = 10;
            yoshi.Nivel = 1;
            Assert.AreEqual(yoshi.AtaqueEspecial(), 17);
            yoshi.AtaqueEspecial();
            Assert.AreEqual(yoshi.MedidorEspecial, 3);
        }
    }
}