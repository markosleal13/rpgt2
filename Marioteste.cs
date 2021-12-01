using NUnit.Framework;
using RpgAniAlieLib.Personagens;
using RpgAniAlieLib.Player;

namespace Tests
{
    public class Tests5
    {
        [SetUp]
        public void Setup5()
        {

        }

        [Test]

        public void Recarregar()
        {
            var mario = new Mario("pulando", "mario");
            InventarioC.QtdBala = 5;
            mario.MedidorEspecial = 0;
            Assert.IsTrue(mario.Recarregar());
        }

        [Test]
        public void AtaqueEspecial()
        {
            var mario = new Mario("pulando", "mario");
            mario.MedidorEspecial = 2;
            mario.Atk = 10;
            mario.Nivel = 1;
            Assert.AreEqual(mario.AtaqueEspecial(), 15);
        }
    }
}