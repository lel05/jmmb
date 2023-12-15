using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace jmmb
{
    public partial class MainPage : TabbedPage
    {
        bool isTimeRunning = false;
        TimeSpan remainingTime = TimeSpan.Zero;
        public MainPage()
        {
            InitializeComponent();

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                clockLabel.Text = DateTime.Now.ToString();
                return true;
            });

        }

        private void startButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                int minutes = int.Parse(entryTime.Text);
                remainingTime = TimeSpan.FromMinutes(minutes);
                isTimeRunning = true;
                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    remainingTimeLabel.Text = remainingTime.ToString("mm\\:ss");
                    remainingTime = remainingTime.Subtract(TimeSpan.FromSeconds(1));
                    if (remainingTime.TotalSeconds <= 0)
                    {
                        remainingTimeLabel.Text = remainingTime.ToString("mm\\:ss");
                        isTimeRunning = false;
                        DisplayAlert("Uwaga!", "Czas minął.", "OK");
                    }
                    return isTimeRunning;
                });
        }
            catch (Exception ex)
            {
                DisplayAlert("Błąd!", "Wprowadzono niepoprawne dane.", "OK");
            }
        }
    }
}
