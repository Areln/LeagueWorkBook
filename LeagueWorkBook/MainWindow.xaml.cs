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
using MingweiSamuel.Camille.ChampionMasteryV4;
using System.Text.RegularExpressions;

namespace LeagueWorkBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RiotApi riotApi = RiotApi.NewInstance("RGAPI-97268b1a-9c8c-4f08-8cd5-baf87e0e4bf9");
        Summoner searchedSummoner;
        CurrentGameInfo CurrentGameInfo;
        ChampionMastery[] championMastery;
        List<Image> champMasteryImages = new List<Image>();
        public MainWindow()
        {
            InitializeComponent();

            //adds image objects to an array
            champMasteryImages.Add(champMasteryIcon1);
            champMasteryImages.Add(champMasteryIcon2);
            champMasteryImages.Add(champMasteryIcon3);
            champMasteryImages.Add(champMasteryIcon4);
            champMasteryImages.Add(champMasteryIcon5);

        }
        void ChangeProfileIcon(int iconID) 
        {
            ProfileIconImage.Source = new BitmapImage(new Uri(@"/Images/profileicon/" + iconID + ".png", UriKind.Relative));
        }
        private void SummonerNameSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SummonerNameSearchField.Text != "")
            {
                try
                {
                    searchedSummoner = riotApi.SummonerV4.GetBySummonerName(Region.NA, SummonerNameSearchField.Text);
                    //CurrentGameInfo = riotApi.SpectatorV4.GetCurrentGameInfoBySummoner(Region.NA,searchedSummoner.Id);
                    championMastery = riotApi.ChampionMasteryV4.GetAllChampionMasteries(Region.NA, searchedSummoner.Id);
                }
                catch (NullReferenceException ex)
                {
                    MatchupNotes.Text = "Summoner Not Found!";
                }
                if (searchedSummoner != null)
                {
                    DisplayMatch();
                }
                else
                {
                    ChangeProfileIcon(0);
                }
            }
            
        }
        void DisplayMatch() 
        {
            //clears the notes
            MatchupNotes.Text = "";
            for (int i = 0; i < 5; i++)
            {
                ChampionMastery mastery = championMastery[i];
                Champion champion = (Champion)mastery.ChampionId;
                MatchupNotes.Text = string.Concat(MatchupNotes.Text, (i+1)+")",champion.Name(), $": {mastery.ChampionPoints} \n");
                string champShell = champion.Name().Replace(" ", string.Empty);
                champShell = champShell.Replace("'", string.Empty);
                champMasteryImages[i].Source = new BitmapImage(new Uri(@"/Images/champion/" + champShell + ".png", UriKind.Relative));
            }
            ChangeProfileIcon(searchedSummoner.ProfileIconId);
        }
    }
}
