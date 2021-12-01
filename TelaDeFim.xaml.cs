using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace RpgTelas
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class TelaDeFim : Page
    {
        MediaPlayer tocador;
        public TelaDeFim()
        {
            this.InitializeComponent();
            tocador = new MediaPlayer();
            Musica();
        }
        /// <summary>
        /// Irá apagar todos os caches
        /// </summary>
        private void ResetPageCache()//Irá apagar o cache de todas as telas
        {
            var cacheSize = ((Frame)Parent).CacheSize;
            ((Frame)Parent).CacheSize = 0;
            ((Frame)Parent).CacheSize = cacheSize;
        }
        public async void Musica()
        {

            Windows.Storage.StorageFolder pasta = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets"); // Irá pegar a pasta onde está musica
            Windows.Storage.StorageFile arquivo = await pasta.GetFileAsync("Parabains.mp3");//Ira pegar o arquivo da musica
            tocador.AutoPlay = true;// Irá dar um auto play na musica
            tocador.Source = MediaSource.CreateFromStorageFile(arquivo);// Está definindo o arquivo que o tocador irá tocar a musica
            tocador.IsLoopingEnabled = true;//A musica ficará em loop
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tocador.Source = null;
            ResetPageCache();
            this.Frame.Navigate(typeof(MainPage));//Irá passar para a tela MainPage
        }
    }
}
