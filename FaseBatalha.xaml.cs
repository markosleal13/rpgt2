using RpgAniAlieLib.Personagens;
using RpgAniAlieLib.Player;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
using System.Threading.Tasks;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace RpgTelas
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class FaseBatalha : Page
    {
        MediaPlayer tocador;
        Passar p;
        Mario mario;
        Yoshi yo;
        Luigi lui;
        List<Inimigos> InimigosList = new List<Inimigos>();
        int qualInimigo;//Vai dizer quel é o inimigo sortiado
        public FaseBatalha()
        {
            this.InitializeComponent();
            
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            p = (Passar)e.Parameter;
            tocador = p.RetornaTocador();
            mario = p.RetornaMario();
            yo = p.RetornaYo();
            lui = p.RetornaLuigi();

            if(yo == null)//Se o obejeto for nulo vai deixar invisivel todos os botões e imagens associados
            {
                ImgYo.Visibility = Visibility.Collapsed;
                AtkYo.Visibility = Visibility.Collapsed;
                Mordida.Visibility = Visibility.Collapsed;
                CurarYo.Visibility = Visibility.Collapsed;
                HpYo.Visibility = Visibility.Collapsed;
                Veneno.Visibility = Visibility.Collapsed;
            }

            if(lui == null)//Se o obejeto for nulo vai deixar invisivel todos os botões e imagens associados
            {
                ImgLui.Visibility = Visibility.Collapsed;
                AtkLui.Visibility = Visibility.Collapsed;
                Investida.Visibility = Visibility.Collapsed;
                CurarLui.Visibility = Visibility.Collapsed;
                Estamina.Visibility = Visibility.Collapsed;
            }
            if (p.QualInimigo == 'i')//Se for i é um inimigo normal
            {
                InimigosRamdom();
                Random randNum = new Random();
                qualInimigo = randNum.Next(0, InimigosList.Count);
                ImgInimigo.Source = new BitmapImage(new Uri(base.BaseUri, @"" + InimigosList[qualInimigo].SpriteDeBatalha));
            }else if (p.QualInimigo == 'c')//Se for c é o primeiro bos
            {
                InimigosList.Add(new Inimigos(mario.Nivel , "/Assets/Imagens/Personagens/Inimigos/LuigiTalarico.png", "Luigi", "Investida"));
                qualInimigo = 0;
                ImgInimigo.Source = new BitmapImage(new Uri(base.BaseUri, @"" + InimigosList[qualInimigo].SpriteDeBatalha));
                Voltar.IsEnabled = false;
            }
            else if (p.QualInimigo == 'o')//Se for o é o segundo boss
            {
                InimigosList.Add(new Inimigos(mario.Nivel , "/Assets/Imagens/Personagens/Inimigos/Yoshi.png", "Yoshi", "Investida"));
                qualInimigo = 0;
                ImgInimigo.Source = new BitmapImage(new Uri(base.BaseUri, @"" + InimigosList[qualInimigo].SpriteDeBatalha));
                Voltar.IsEnabled = false;
            }
            else if (p.QualInimigo == 'a')//Se for a é o ultimo boss
            {
                InimigosList.Add(new Inimigos(mario.Nivel, "/Assets/Imagens/Personagens/Inimigos/Wario.png", "Comandante Alien", "Laiser"));
                qualInimigo = 0;
                ImgInimigo.Source = new BitmapImage(new Uri(base.BaseUri, @"" + InimigosList[qualInimigo].SpriteDeBatalha));
                Voltar.IsEnabled = false;
            }
            AtualizarTxts();
        }
        public void InimigosRamdom()//Vai adicionar os imigos em uma lista para randomizar eles
        {
            InimigosList.Add(new Inimigos(mario.Nivel, "/Assets/Imagens/Personagens/Inimigos/Urso.png", "Urso","Garrada"));
            InimigosList.Add(new Inimigos(mario.Nivel, "/Assets/Imagens/Personagens/Inimigos/Furao.png", "Furão", "Cabeçada"));
            InimigosList.Add(new Inimigos(mario.Nivel, "/Assets/Imagens/Personagens/Inimigos/Girafa.png", "Girafa", "Pescoçada"));
            InimigosList.Add(new Inimigos(mario.Nivel, "/Assets/Imagens/Personagens/Inimigos/Elefante.png", "Elefante", "Trombada"));
            InimigosList.Add(new InimigosVoadores(mario.Nivel, "/Assets/Imagens/Personagens/Inimigos/Gavião.png", "Gavião", "Garrada"));
            InimigosList.Add(new InimigosVoadores(mario.Nivel, "/Assets/Imagens/Personagens/Inimigos/Urubu.png", "Urubu", " Bicada"));
        }
        public void Pausar_Button()//Vai pausar os botões
        {
            AtkMario.IsEnabled = false;
            AtirarMario.IsEnabled = false;
            CurarMario.IsEnabled = false;
            Recarregar.IsEnabled = false;
            AtkLui.IsEnabled = false;
            CurarLui.IsEnabled = false;
            Investida.IsEnabled = false;
            AtkYo.IsEnabled = false;
            CurarYo.IsEnabled = false;
            Mordida.IsEnabled = false;
            Perder.IsEnabled = false;
            Voltar.IsEnabled = false;
        }
        public void AtualizarTxts()//Vai atualizar os txts da fase de batalha
        {
            HpMario.Text ="HP: " + mario.Vida.ToString() + "/" + mario.VidaMax.ToString();
            Carga.Text ="Munição: "+ mario.MedidorEspecial + "/3";
            PoteTxt.Text = ": " + InventarioC.qtdPocao.ToString();
            MuniTxt.Text = ": " + InventarioC.QtdBala.ToString();
           NomeInimigo.Text ="Nome: " + InimigosList[qualInimigo].Nome;
            HpInmigo.Text = "Hp: " + InimigosList[qualInimigo].Vida + "/" + InimigosList[qualInimigo].VidaMax;
            if(lui != null)
            {
            
                Estamina.Text = "Estamina: " + lui.MedidorEspecial + "/10"; 
            }

            if(yo != null)
            {
                HpYo.Text = "HP: " + yo.Vida.ToString() + "/" + yo.VidaMax;
                Veneno.Text = "Pé: " + yo.MedidorEspecial + "/" + "10";
            }
        }
        private void Voltar_Click(object sender, RoutedEventArgs e)//Vai sair da fase de batalha
        {
            this.Frame.GoBack();
        }

        private void AtkMario_Click(object sender, RoutedEventArgs e)//Vai realizar o ataque do mario
        {
            MensagemDoMeio.Text = "";
            if (0<mario.Vida)
            {


                int aux;
                aux = InimigosList[qualInimigo].ReceberDano(mario.AtaqueTotal(), mario.VelocidadeTotal(), mario.Critico());
                MensagemDoMeio.Text = "O mario causou " + aux.ToString() + " de dano a(ao) " + InimigosList[qualInimigo].Nome + ". ";
                ReacaoInimigo();
                AtualizarTxts();
            }
            else
            {
                MensagemDoMeio.Text = "O mario está mal";
            }
        }

        private void CurarMario_Click(object sender, RoutedEventArgs e)//Vai realizar a cura do mario
        {
            MensagemDoMeio.Text = "";
           

            
            if (mario.Curar() == 1)
            {
                MensagemDoMeio.Text = "O Mario se curou. ";
                ReacaoInimigo();
                AtualizarTxts();
                //Dar aviso em um txt
            }
            else
            {
                MensagemDoMeio.Text = "O Mario não conseguiu se curar. ";
                //Dar aviso em um txt
            }
           
        }

        private void AtirarMario_Click(object sender, RoutedEventArgs e)//Vai realizar o tiro do mario
        {
            MensagemDoMeio.Text = "";
            if (0 < mario.Vida)
            {
                int aux = mario.AtaqueEspecial();
                if (aux != 0)
                {
                    aux = InimigosList[qualInimigo].ReceberDano(aux, mario.VelocidadeTotal(), mario.Critico());
                    MensagemDoMeio.Text = "O Mario atirou  causando " + aux.ToString() + " de dano a(ao) " + InimigosList[qualInimigo].Nome + ". ";
                    ReacaoInimigo();
                }
                else
                {
                    MensagemDoMeio.Text = "O Mario não consegiu atirar.";
                }
                AtualizarTxts();
            }
            else
            {
                MensagemDoMeio.Text = "O Mario está desmaiado";
            }
        }

        private void Recarregar_Click(object sender, RoutedEventArgs e)//Vai realizar a recarga do Mario
        {
            MensagemDoMeio.Text = "";
            if (0<mario.Vida )
            {
                if (mario.Recarregar())
                {
                    MensagemDoMeio.Text = "Você recarregou. ";
                    ReacaoInimigo();
                }
                else
                {
                    MensagemDoMeio.Text = "Não é possível recarregar.";
                }

                AtualizarTxts();
            }
            else
            {
                MensagemDoMeio.Text = "O Mario está desmaiado";
            }
        }

        private void Perder_Click(object sender, RoutedEventArgs e)//Irá direcionar o jogador para a tela de GameOver
        {
            tocador.Source = null;//Irá parar a musica
            this.Frame.Navigate(typeof(GameOver));//Irá passar para a tela GameOver

        }

        private void AtkLui_Click(object sender, RoutedEventArgs e)//Vai realizar o atk do luigi
        {
            MensagemDoMeio.Text = "";
            if (0 < lui.Vida)
            {
                int aux;
                aux = InimigosList[qualInimigo].ReceberDano(lui.AtaqueTotal(), lui.VelocidadeTotal(), lui.Critico());
                MensagemDoMeio.Text = "O luigi causou " + aux.ToString() + " de dano a(ao) " + InimigosList[qualInimigo].Nome + ". ";
                lui.ReceberStamina();
                ReacaoInimigo();
                AtualizarTxts();
            }
            else
            {
                MensagemDoMeio.Text = "O luigi desmaiou";
            }
        }

        private void Investida_Click(object sender, RoutedEventArgs e)//Vai realizar a investida do luigi
        {
            MensagemDoMeio.Text = "";
            if (0 < lui.Vida)
            {
                int aux = lui.AtaqueEspecial();
                if (aux != 0)
                {
                    aux = InimigosList[qualInimigo].ReceberDano(aux, lui.VelocidadeTotal(), lui.Critico());
                    MensagemDoMeio.Text = "A investida do luigi causou " + aux.ToString() + " de dano a(ao) " + InimigosList[qualInimigo].Nome + ". ";
                    ReacaoInimigo();
                }
                else
                {
                    MensagemDoMeio.Text = "O luigi não conseguiu realizar uma investida. ";
                }
                AtualizarTxts();
            }
            else
            {
                MensagemDoMeio.Text = "O luigi desmaiou";
            }
        }

        private void CurarLui_Click(object sender, RoutedEventArgs e)//Vai realizar a cura do luigi
        {
            MensagemDoMeio.Text = "";
                if (lui.Curar() == 1)
                {
                    MensagemDoMeio.Text = "O luigi se curou. ";
                    ReacaoInimigo();
                    AtualizarTxts();
                }
                else
                {
                    MensagemDoMeio.Text = "O luigi não conseguiu se curar. ";
                }
            
        }

        private void AtkYo_Click(object sender, RoutedEventArgs e)//Vai realizar o ataque do yoshi
        {
            MensagemDoMeio.Text = "";
            if (0 < yo.Vida )
            {


                int aux;
                aux = InimigosList[qualInimigo].ReceberDano(yo.AtaqueTotal(), yo.VelocidadeTotal(), yo.Critico());
                MensagemDoMeio.Text = "O yoshi causou " + aux.ToString() + " de dano a(ao) " + InimigosList[qualInimigo].Nome + ". ";
                ReacaoInimigo();
                AtualizarTxts();
            }
            else
            {
                MensagemDoMeio.Text = "O yohi desmaiou";
            }
        }

        private void Mordida_Click(object sender, RoutedEventArgs e)//Vai realizar a pisada do yoshi
        {
            MensagemDoMeio.Text = "";
            if (0 < yo.Vida)
            {
                int aux = yo.AtaqueEspecial();
                if (aux != 0)
                {
                    aux = InimigosList[qualInimigo].ReceberDano(aux, yo.VelocidadeTotal(), yo.Critico());
                    MensagemDoMeio.Text = "A pisada do causou " + aux.ToString() + " de dano a(ao) " + InimigosList[qualInimigo].Nome + ". ";
                    ReacaoInimigo();

                }
                else
                {
                    MensagemDoMeio.Text = "O yoshi não conseguiu pisar no inimigo. ";
                }
                AtualizarTxts();
            }
            else
            {
                MensagemDoMeio.Text = "O yoshi desmaiou";
            }
        }

        private void CurarCo_Click(object sender, RoutedEventArgs e)//Vai realizar a cura do yoshi
        {
            MensagemDoMeio.Text = "";
                if (yo.Curar() == 1)
                {
                    MensagemDoMeio.Text = "O yoshi se curou. ";
                    ReacaoInimigo();
                    AtualizarTxts();
                    //Dar aviso em um txt
                }
                else
                {
                    MensagemDoMeio.Text = "O yoshi não conseguiu se curar. ";
                }
            
            
        }
       
        public async void ReacaoInimigo()//Irá fazer a reção do inimigo
        {

            if (InimigosList[qualInimigo].Vida > 0)//Se ele estiver vivo ele vai fazer a ação
            {
                int aux = 0;
                if (lui != null)///Vai vericar se o objeto existe
                {
                    aux++;//Caso exista o aux vai ser adicionado mais um
                }

                if (yo != null)///Vai vericar se o objeto existe
                {
                    aux++;//Caso exista o aux vai ser adicionado mais um
                }
                Random randNum = new Random();

                while (true)//Vai continuar no wihle até o inimigo causar dano a um aliado
                {

                
                int aleatorio = randNum.Next(0, aux + 1);
                    if (aleatorio == 0 && 0 < mario.Vida)
                    {
                        aux = mario.ReceberDano(InimigosList[qualInimigo].Ataques(), InimigosList[qualInimigo].Velo, InimigosList[qualInimigo].Critico());
                        string aux2;
                        if (InimigosList[qualInimigo].Ataques() > InimigosList[qualInimigo].Atk)
                        {
                            aux2 = " com uma " + InimigosList[qualInimigo].NomeAtaqueDeFuria;
                        }
                        else
                        {
                            aux2 = "";
                        }
                        MensagemDoMeio.Text += "O " + InimigosList[qualInimigo].Nome + " causou " + aux.ToString() + " de dano " + aux2 + " ao  Mario ";
                        break;
                    }
                    else if (0 < lui.Vida && aleatorio == 1)
                    {
                        string aux2;
                        if (InimigosList[qualInimigo].Ataques() > InimigosList[qualInimigo].Atk)
                        {
                            aux2 = " com uma " + InimigosList[qualInimigo].NomeAtaqueDeFuria;
                        }
                        else
                        {
                            aux2 = "";
                        }
                        lui.ReceberDano(InimigosList[qualInimigo].Ataques(), InimigosList[qualInimigo].Velo, InimigosList[qualInimigo].Critico());
                        MensagemDoMeio.Text += "O " + InimigosList[qualInimigo].Nome + " causou " + aux.ToString() + " de dano " + aux2 + " a " + "Luigi";
                        break;
                    }
                    else if (aleatorio == 2 && 0 < yo.Vida)
                    {
                        string aux2;
                        if (InimigosList[qualInimigo].Ataques() > InimigosList[qualInimigo].Atk)
                        {
                            aux2 = " com uma " + InimigosList[qualInimigo].NomeAtaqueDeFuria;
                        }
                        else
                        {
                            aux2 = "";
                        }
                        yo.ReceberDano(InimigosList[qualInimigo].Ataques(), InimigosList[qualInimigo].Velo, InimigosList[qualInimigo].Critico());
                        MensagemDoMeio.Text += "O " + InimigosList[qualInimigo].Nome + " causou " + aux.ToString() + " de dano " + aux2 + " o " + "yoshi";
                        break;
                    }
                    
                    
                    
            }
                if(mario.Vida <= 0  && lui == null && yo == null)//Ira verficar se os aliados perderam
                {
                    Pausar_Button();
                    tocador.Source = null;//Irá parar a musica
                    await Task.Delay(TimeSpan.FromSeconds(5));
                    this.Frame.Navigate(typeof(GameOver));//Irá passar para a tela GameOver
                }
                else if(mario.Vida <= 0 && lui.Vida <= 0 && yo == null)// Ira verficar se os aliados perderam
                {
                    Pausar_Button();
                    tocador.Source = null;//Irá parar a musica
                    await Task.Delay(TimeSpan.FromSeconds(5));
                    this.Frame.Navigate(typeof(GameOver));//Irá passar para a tela GameOver
                }else if(mario.Vida <= 0 && lui.Vida <= 0 && yo.Vida < 0)// Ira verficar se os aliados perderam
                {
                    Pausar_Button();
                    tocador.Source = null;//Irá parar a musica
                    await Task.Delay(TimeSpan.FromSeconds(5));
                    this.Frame.Navigate(typeof(GameOver));//Irá passar para a tela GameOver
                }
            }
            else
            {//Caso o inimigo morra
                Pausar_Button();//Não deixar o jogador apertar os botões 
                await Task.Delay(TimeSpan.FromSeconds(5));
                MensagemDoMeio.Text = "Você ganhou a batalha. Você recebeu uma moeda. Todos os seus aliados ganharão 10 de xp";
                InventarioC.QuantidadeMoeda++;
                mario.GanharXP(10);
                if(lui != null)//Caso objeto exista ele vai ganahr xp
                {
                    lui.GanharXP(10);
                }

                if (yo != null)//Caso objeto exista ele vai ganahr xp
                {
                    yo.GanharXP(10);
                    yo.RecarregarPe();//O yoshi recupera a energia toda vez q o inimigo morre
                }
                await Task.Delay(TimeSpan.FromSeconds(5));
                if(p.QualInimigo == 'i')
                {
                    this.Frame.GoBack();
                }else if(p.QualInimigo == 'c')
                {
                    tocador.Source = null;// Irá parar a musica
                    this.Frame.Navigate(typeof(Fase2), p);
                }
                else if (p.QualInimigo == 'o')
                {
                    tocador.Source = null;// Irá parar a musica
                    this.Frame.Navigate(typeof(Fase3), p);
                }
                else if (p.QualInimigo == 'a')
                {
                    tocador.Source = null;// Irá parar a musica
                    this.Frame.Navigate(typeof(TelaDeFim));
                }

            }
        }


    }
}
