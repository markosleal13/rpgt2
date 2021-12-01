using NUnit.Framework;
using RpgAniAlieLib.Personagens;

namespace Tests
{
    public class Tests6
    {
        [SetUp]
        public void Setup6()
        {

        }

        [Test]
        
        public void Recarregar()
        {
            var luigi = new Luigi("chute","lui");
            luigi.MedidorEspecial = 9;
            luigi.ReceberStamina();
            Assert.AreEqual(luigi.MedidorEspecial, 10);
        }

        [Test]
        public void AtaqueEspecial()
        {
            var luigi = new Luigi("chute", "lui");
            luigi.MedidorEspecial = 9;
            luigi.Nivel = 1;
            luigi.Atk = 10;
            Assert.AreEqual(luigi.AtaqueEspecial(), 16);
            Assert.AreEqual(luigi.MedidorEspecial, 4);
            Assert.AreEqual(luigi.AtaqueEspecial(), 0);
        }
    }
}