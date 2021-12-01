using NUnit.Framework;
using RpgAniAlieLib.Equipamento;
using RpgAniAlieLib.Personagens;
using RpgAniAlieLib.Player;

namespace Tests
{
    public class Tests4
    {
        [SetUp]
        public void Setup4()
        {

        }

        [Test]
        public void TesteDanoDfIgualQAtk()
        {
            var inimigo = new Inimigos(1,"deslizando","inimigo","rapaz");
            inimigo.Vida = 23;
            inimigo.Velo = 0;
            inimigo.Def = 1000;
            inimigo.ReceberDano(1000, 100, false);

            Assert.AreEqual(inimigo.Vida, 22);
           
        }

        [Test]
        
        public void testarXp()
        {
            var aliado = new Aliados("caminhar","bichinho");
            aliado.XP = 5;
            aliado.XPProxNlv = 10;
            aliado.Nivel = 1;
            aliado.GanharXP(5);
            Assert.AreEqual(aliado.XP, 10);
            Assert.AreEqual(aliado.Nivel, 2);
            Assert.AreEqual(aliado.XPProxNlv, 15);
        }

        [Test]

        public void Defender()
        {
            var aliado = new Aliados("caminhar", "bichinho");
            InventarioC.NlvArmadura = 1;
            aliado.Def = 10;
            int aux = aliado.DefesaTotal();
            Assert.AreEqual(aux, 13);
            Assert.AreEqual(aliado.Defender(10), 1);


        }
    }
}