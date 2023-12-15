using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace jmmb
{
    public partial class MainPage : TabbedPage
    {
        DateTime Alarm = new DateTime();

        bool isTimeRunning = false;
        TimeSpan remainingTime = TimeSpan.Zero;
        public MainPage()
        {
            InitializeComponent();

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                clockLabel.Text = DateTime.Now.ToString("HH:mm:ss");
                clockDateLabel.Text = DateTime.Today.ToString("dd MMMM yyy");
                return true;
            });
        }

        private async void SetAlarm_Clicked(object sender, EventArgs e)
        {

            var TimePicker = TimePickerAlarm.Time;
            Alarm = DateTime.Today + TimePicker;

            if(Alarm < DateTime.Now)
            {
                Alarm.AddDays(1);
            }

            var timeToWait = Alarm - DateTime.Now;

            await Task.Delay(timeToWait);

            Device.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("Alarm", "Pobudkaaa!!!", "OK");
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
