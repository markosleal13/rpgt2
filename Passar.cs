using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RpgAniAlieLib;
using RpgAniAlieLib.Personagens;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace RpgTelas
{
    /// <summary>
    /// Vai pegar as os obejtos dos personagens, o tocador e q tipo de inimigo é para passar eles entre as telas
    /// </summary>
    public class Passar
    {
        Mario m = null;
        Luigi lu = null;
        Yoshi yo = null;
        MediaPlayer t = null;
        public char QualInimigo { get; set; }
        public void DefinirMario(Mario m1)
        {
            m = m1;
        }
        public void DefinirLui(Luigi c1)
        {
            lu = c1;
        }

        public void DefinirYoshi(Yoshi c1)
        {
            yo = c1;
        }

        public void DefinirTocador(MediaPlayer t1)
        {
            t = t1;
        }

        public Mario RetornaMario()
        {
            return m;
        }
        public Luigi RetornaLuigi()
        {
            return lu;
        }
        public Yoshi RetornaYo()
        {
            return yo;
        }

        public MediaPlayer RetornaTocador()
        {
            return t;
        }

        
    }
}
