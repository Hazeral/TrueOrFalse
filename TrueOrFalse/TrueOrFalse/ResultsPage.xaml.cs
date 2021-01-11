using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrueOrFalse
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultsPage : ContentPage
    {
        public ResultsPage(short score, int time)
        {
            InitializeComponent();

            lblScore.Text = $"Score: {score}";
            lblTime.Text = string.Format("Time taken: {0:00}:{1:00}", time / 60, (time / 60) == 0 ? time : time - ((time / 60) * 60));
            // Divided by 60 then multipled by 60 again as the remainder would be removed when divided

            stackInfo.Scale = 0.5;
            stackInfo.Opacity = 1;
            frameMain.Scale = 0.5;
            frameMain.Opacity = 1;
            stackInfo.ScaleTo(1, 300);
            stackInfo.FadeTo(1, 300);
            frameMain.ScaleTo(1, 300);
            frameMain.FadeTo(1, 300);
        }

        private void btnClose_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}