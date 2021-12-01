using RpgAniAlieLib.Player;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class Inventario : Page
    {
        public Inventario()
        {
            this.InitializeComponent();
            AtualizarTxt();
        }
        /// <summary>
        /// atualiza os itens do iventario
        /// </summary>
        public void AtualizarTxt()
        {
            Pote.Text = ": " + InventarioC.qtdPocao.ToString();
            Muni.Text = ": " + InventarioC.QtdBala.ToString();
            Moeda.Text = ": " + InventarioC.QuantidadeMoeda.ToString();
            Armadura.Text = "Nivel: " + InventarioC.NlvArmadura.ToString();
        }
        private void Voltar_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
    }
}
