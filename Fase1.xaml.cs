using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Media.Playback;
using Windows.Media.Core;
using RpgAniAlieLib;
using RpgAniAlieLib.Personagens;
using RpgAniAlieLib.Player;
using RpgAniAlieLib.Equipamento;



// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace RpgTelas
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>

    public sealed partial class Fase1 : Page
    {
        List<UIElement> ColisoesLidar = new List<UIElement>();//Uma listas com as colisões que handleCollisions vai ter que lidar
        MediaPlayer tocador;
        Mario m = new Mario("mario2.jpg","Mario");
        Passar p = new Passar();
        public Fase1()
        {
            this.InitializeComponent();
            this.KeyDown += Fase_KeyDown;
            this.Loaded += delegate { this.Focus(FocusState.Programmatic); };
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;//Irá habilitar o cache ou seja caso ele saia desse frame as informações que foram modificadas nele permanecerão
            tocador = new MediaPlayer();
            InventarioC.NlvArmadura = 1;
            Musica();
        }

        public async void Musica()
        {
            
            Windows.Storage.StorageFolder pasta = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");// Irá pegar a pasta onde está musica
            Windows.Storage.StorageFile arquivo = await pasta.GetFileAsync("Super Mario Bros. medley.mp3");//Ira pegar o arquivo da musica
            tocador.AutoPlay = true;// Irá dar um auto play na musica
            tocador.Source = MediaSource.CreateFromStorageFile(arquivo);// Está definindo o arquivo que o tocador irá tocar a musica
            tocador.IsLoopingEnabled = true;//A musica ficará em loop
        }
        /// <summary>
        /// Irá lidar com as colisões
        /// </summary>
        /// <param name="x">Caso o ImgMario estiver se movendo para os lados ele será diferente de 0</param>
        /// <param name="y">Caso o ImgMario estiver se movendo para cima ou para baixo ele será diferente de 0</param>
        public void handleCollisions(double x, double y)
        {
            List<UIElement> AllCollidables = Colidiveis.Children.ToList();//Irá pegar todas as imagens do Canvas Colidiveis
            Rect player = new Rect();// Vai criar um retangulo do mesmo tamanho do image ImgMario e no seu mesmo local
            player.X = Canvas.GetLeft(ImgMario);
            player.Y = Canvas.GetTop(ImgMario);
            player.Height = ImgMario.Height;
            player.Width = ImgMario.Width;


            foreach (Image item in AllCollidables)
            {
                if (item.Visibility == Visibility.Visible)// Se o item não for visivel ele não irá mais ser passar pelo teste de colisão
                {
                    Rect obj = new Rect();// Vai criar um retangulo do mesmo tamanho do image item e no seu mesmo local
                    obj.X = Canvas.GetLeft(item);
                    obj.Y = Canvas.GetTop(item);
                    obj.Height = item.Height;
                    obj.Width = item.Width;

                    obj.Intersect(player);//Caso o retangulo player não entre em contato o retangulo obj o obj se torna um retagulo vazio 

                    if (!obj.IsEmpty)//Caso o retangulo não for vazio o item será adicionado na lista ColisoesLidar
                    {
                        ColisoesLidar.Add(item);
                    }
                }
            }
            foreach (Image item in ColisoesLidar)
            {

                if (item.Name.ToLower().Contains("bloco"))//Caso o nome do item tiver bloco o image ImgMario irá recuar o que foi movido dando a impressão que o bloco é um objeto solido
                {
                    if (y != 0) //Caso for para cima ou para baixo
                    {
                        Canvas.SetTop(ImgMario, Canvas.GetTop(ImgMario) + y );//O ImgMario irá recuar para cima ou para baixo
                    }
                    else if (x != 0)//Caso for para direita ou para esquerda
                    {
                        Canvas.SetLeft(ImgMario, Canvas.GetLeft(ImgMario) + x);//O ImgMario irá recuar para direita ou para esquerda
                    }

                }
                if (item.Name.ToLower().Contains("inimigo"))//Caso o nome do item tiver inimigo, esse item não será mais visivel, além disso o jogador será direcionado para a FaseBatalha
                {
                    item.Visibility = Visibility.Collapsed;// O item não será mais visivel
                    p.DefinirMario(m);
                    p.DefinirTocador(tocador);
                    p.QualInimigo = 'i';
                    this.Frame.Navigate(typeof(FaseBatalha), p);// Irá passar para a tela FaseBatalha, irá passar passar o tocador para a tela FaseBatalha
                }
                if (item.Name.ToLower().Contains("chefe"))//Caso o nome do item tiver chefe, esse item não será mais visivel, além disso o jogador será direcionado para a FaseBatalha
                {
                    item.Visibility = Visibility.Collapsed;// O item não será mais visivel
                    p.DefinirMario(m);
                    p.DefinirTocador(tocador);
                    p.QualInimigo = 'c';
                    this.Frame.Navigate(typeof(FaseBatalha), p);//Como a fase de batahla não está pronta irá passar para a tela para a Fase2
                }
                if (item.Name.ToLower().Contains("pote"))//Caso o nome do item tiver pote, esse item não será mais visivel, além disso o jogador ganhara uma poção
                {
                    item.Visibility = Visibility.Collapsed;// O item não será mais visivel
                    InventarioC.qtdPocao++;
                }
                if (item.Name.ToLower().Contains("tesouro"))//Caso o nome do item tiver tesouro, esse item não será mais visivel, além disso o jogador ganhara 5 moedas
                {
                    item.Visibility = Visibility.Collapsed;// O item não será mais visivel
                    InventarioC.QuantidadeMoeda += 5;
                }
                if (item.Name.ToLower().Contains("vendedor"))//Caso o nome do item tiver vendedor,o jogador será direcionado para o vendedor
                {
                    if (y != 0) //Caso for para cima ou para baixo
                    {
                        Canvas.SetTop(ImgMario, Canvas.GetTop(ImgMario) + y);//O ImgMario irá recuar para cima ou para baixo
                    }
                    else if (x != 0)//Caso for para direita ou para esquerda
                    {
                        Canvas.SetLeft(ImgMario, Canvas.GetLeft(ImgMario) + x);//O ImgMario irá recuar para direita ou para esquerda
                    }
                    this.Frame.Navigate(typeof(Vendedor));
                }
            }
            
            ColisoesLidar.Clear();
        }
        private async void Fase_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Down)
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                     CoreDispatcherPriority.Normal,
                     Down // O Método a ser chamado
                );
            }
            else if (e.Key == Windows.System.VirtualKey.Up)
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                     CoreDispatcherPriority.Normal,
                     Up // O Método a ser chamado
                );
            }
            else if (e.Key == Windows.System.VirtualKey.Right)
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                    CoreDispatcherPriority.Normal,
                    Right // O Método a ser chamado
               );
            }
            else if (e.Key == Windows.System.VirtualKey.Left)
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                     CoreDispatcherPriority.Normal,
                    Left // O Método a ser chamado
               );
            }
        }

        public async void Down()
        {
                Canvas.SetTop(ImgMario, Canvas.GetTop(ImgMario) + 5);
                handleCollisions(0,-5);
                await Task.Delay(50);
        }

        public async void Up()
        {
                Canvas.SetTop(ImgMario, Canvas.GetTop(ImgMario) - 5);
                handleCollisions(0, 5);
                await Task.Delay(50);
        }

        public async void Right()
        {    
                Canvas.SetLeft(ImgMario, Canvas.GetLeft(ImgMario) + 5);
                handleCollisions(-5, 0);
                await Task.Delay(50);
        }

        public async void Left()
        {
                Canvas.SetLeft(ImgMario, Canvas.GetLeft(ImgMario) - 5);
                handleCollisions(5, 0);
                await Task.Delay(50);
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            // Set the input focus to ensure that keyboard events are raised.
            this.Loaded += delegate { this.Focus(FocusState.Programmatic); };
            Debug.WriteLine("NavigatedTo!");

        }

        private void BtnInv_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Inventario));//Irá para a tela de inventario
        }
    }
}

