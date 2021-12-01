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
    public sealed partial class Vendedor : Page
    {
        public Vendedor()
        {
            this.InitializeComponent();
            AtualizarTxt();
        }
        /// <summary>
        /// compra de itens
        /// </summary>
        public void AtualizarTxt()
        {
            Moeda.Text = ": " + InventarioC.QuantidadeMoeda.ToString();
        }

        private void BtnPote_Click(object sender, RoutedEventArgs e)
        {
            if(InventarioC.QuantidadeMoeda >= 1)
            {
                InventarioC.QuantidadeMoeda--;
                InventarioC.qtdPocao++;
                AtualizarTxt();
            }
        }

        private void BtnMuni_Click(object sender, RoutedEventArgs e)
        {
            if (InventarioC.QuantidadeMoeda >= 2)
            {
                InventarioC.QuantidadeMoeda -= 2;
                InventarioC.QtdBala++;
                AtualizarTxt();
            }
        }

        private void BtnDura_Click(object sender, RoutedEventArgs e)
        {
            if (InventarioC.QuantidadeMoeda >= 3)
            {
                InventarioC.QuantidadeMoeda -= 3;
                InventarioC.NlvArmadura++;
                AtualizarTxt();
            }
        }

        private void Voltar_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
    }
}
