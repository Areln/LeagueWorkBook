using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MingweiSamuel.Camille;
using MingweiSamuel.Camille.Enums;
using MingweiSamuel.Camille.SummonerV4;
using MingweiSamuel.Camille.Util;
using MingweiSamuel.Camille.SpectatorV4;

namespace LeagueWorkBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RiotApi riotApi = RiotApi.NewInstance("RGAPI-e3c14311-1f44-437b-993b-ac6286c737c2");
        Summoner searchedSummoner;
        CurrentGameInfo CurrentGameInfo;

        public MainWindow()
        {
            InitializeComponent();

            
        }

        private void SummonerNameSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            searchedSummoner = riotApi.SummonerV4.GetBySummonerName(Region.NA, SummonerNameSearchField.Text);
            CurrentGameInfo = riotApi.SpectatorV4.GetCurrentGameInfoBySummoner(Region.NA,searchedSummoner.Id);
            DisplayMatch();
        }
        void DisplayMatch() 
        {
            
        }
    }
}
