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

            if (score <= 0) frmScore.BackgroundColor = Color.FromHex("C0392B");
            else frmScore.BackgroundColor = Color.FromHex("16A085");

            lblScore.Text = score.ToString();
            lblTime.Text = string.Format("{0:00}:{1:00}", time / 60, (time / 60) == 0 ? time : time - ((time / 60) * 60));
            // Divided by 60 then multipled by 60 again as the remainder would be removed when divided

            stackInfo.Scale = 1.5;
            stackInfo.Opacity = 0;
            frameMain.Scale = 1.5;
            frameMain.Opacity = 0;
            stackInfo.ScaleTo(1, 500);
            stackInfo.FadeTo(1, 500);
            frameMain.ScaleTo(1, 500);
            frameMain.FadeTo(1, 500);
        }

        private void btnClose_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}