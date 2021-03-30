using PocketFriend.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PocketFriend
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FriendPage : ContentPage
    {
        private VirtualFriend friend = new VirtualFriend();

        private TimeKeeper timeKeeper = new TimeKeeper();

        private static Timer timer;

        double mood = 0.5;
        double hunger = 0.5;
        double energy = 0.5;
        int health = 100;

        private void StartTimer()
        {
            timer = new Timer();

            timer.Interval = 1000;
            timer.Enabled = true;
            timer.Elapsed += UpdateTimedData;
            timer.Start();
        }

        private void ResetTimer()
        {
            timeKeeper.StartTime = DateTime.Now;

            StartTimer();
        }

        private void UpdateTimedData(Object sender, ElapsedEventArgs e)
        {
            TimeSpan timeElapsed = e.SignalTime - timeKeeper.StartTime;

            FriendState newFriendState = friend.CurrentFriendState;

            if (timeElapsed.TotalSeconds < 10)
            {
                newFriendState = FriendState.happy;
            }
            else if (timeElapsed.TotalSeconds < 20)
            {
                newFriendState = FriendState.sad;

                mood -= 0.2;
                hunger -= 0.3;
                energy -= 0.35;
               

            }
            else if (timeElapsed.TotalSeconds >= 20) 
            {
                newFriendState = FriendState.depressed;

                mood -= 0.3;
                hunger -= 0.2;
                energy -= 0.15;
    
            }

            if (newFriendState != friend.CurrentFriendState)
            {
                friend.CurrentFriendState = newFriendState;
                updateUI();
            }
        }

        public FriendPage()
        {
            InitializeComponent();

            updateUI();
            StartTimer();
        }

        void feedFriendTapped(System.Object sender, System.EventArgs e)
        {
            friend.giveFood();

            hunger += 0.01;
            HungerBar.ProgressTo(hunger, 400, Easing.Linear);
            energy += 0.019;
            EnergyBar.ProgressTo(energy, 400, Easing.Linear);
            mood += 0.016;
            MoodBar.ProgressTo(mood, 400, Easing.Linear);

            if (health < 100)
            {
                health += 6;
            }
            HealthLabel.Text = health.ToString();

            ResetTimer();
            updateUI();
        }

        void sleepFriendTapped(System.Object sender, System.EventArgs e)
        {
            friend.giveSleep();

            hunger -= 0.013;
            HungerBar.ProgressTo(hunger, 400, Easing.Linear);
            energy += 0.019;
            EnergyBar.ProgressTo(energy, 400, Easing.Linear);
            mood += 0.016;
            MoodBar.ProgressTo(mood, 400, Easing.Linear);
            if (health < 100)
            {
                health += 10;
            }
            HealthLabel.Text = health.ToString();

            ResetTimer();
            updateUI();
        }

        void workFriendTapped(System.Object sender, System.EventArgs e)
        {
            friend.giveWork();

            hunger -= 0.018;
            HungerBar.ProgressTo(hunger, 400, Easing.Linear);
            energy -= 0.015;
            EnergyBar.ProgressTo(energy, 400, Easing.Linear);
            mood -= 0.006;
            MoodBar.ProgressTo(mood, 400, Easing.Linear);
            if (health > 0)
            {
                health -= 3;
            }
            else if (health == 0)
            {
                DisplayAlert("Healt declined!", "Your friend is sick.", "Start over");
            }
            HealthLabel.Text = health.ToString();
            updateUI();

            ResetTimer();
            updateUI();
        }

        void partyFriendTapped(System.Object sender, System.EventArgs e)
        {
            friend.giveParty();

            hunger -= 0.013;
            HungerBar.ProgressTo(hunger, 400, Easing.Linear);
            energy -= 0.01;
            EnergyBar.ProgressTo(energy, 400, Easing.Linear);
            mood += 0.019;
            MoodBar.ProgressTo(mood, 400, Easing.Linear);
            if (health > 0)
            {
                health -= 2;
            }
            else if (health == 0)
            {
                DisplayAlert("Healt declined!", "Your friend is sick.", "Start over");
            }
            HealthLabel.Text = health.ToString();

            ResetTimer();
            updateUI();
        }

        private async void FriendDepressed()
        {
            await DisplayAlert("Depressed", "Your friend has left you", "New Friend");
            friend.Xp = 0;
            friend.CurrentFriendState = FriendState.happy;
            HealthLabel.Text ="100";
            ResetTimer();

            hunger = 0.5;
            energy = 0.5;
            mood = 0.5;

           HungerBar.ProgressTo(hunger, 400, Easing.Linear);
           EnergyBar.ProgressTo(energy, 400, Easing.Linear);
           MoodBar.ProgressTo(mood, 400, Easing.Linear);

            updateUI();
        }

        async private void PreviousPage(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MainPage());
        }

        void updateUI()
        {
            int friendXp = friend.Xp;


            if (friendXp < 1)
            {
                levelLabel.Text = "No level yet" ;
                xpLabel.Text = "take care of your friend";
            }
            else
            {
                levelLabel.Text = "Level " + Levels.GetLevelFromXp(friendXp).ToString();
                xpLabel.Text = friendXp.ToString();
            }

            //friendNameButton.Text = friend.FriendName;

            Device.BeginInvokeOnMainThread(async () =>
            {
                friendImage.Source = "hoodie" + friend.CurrentFriendState;

                if (friend.CurrentFriendState == FriendState.depressed)
                {
                    FriendDepressed();
                }
            });
        }
    }
}