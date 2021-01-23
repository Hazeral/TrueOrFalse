using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TrueOrFalse
{
    public partial class MainPage : ContentPage
    {
        RestService restService = new RestService();
        short apiResponse;
        Question[] apiQuestions;

        Dictionary<short, string> responses = new Dictionary<short, string>()
        {
            { 0, "Success" },
            { 1, "No results" },
            { 2, "Invalid parameter" },
            { 3, "Token not found" },
            { 4, "Token empty session" }
        };

        Dictionary<string, short> categories = new Dictionary<string, short>()
        {
            { "Mixed", 0 },
            { "Animals", 27 },
            { "Anime & Manga", 31 },
            { "Art", 25 },
            { "Board Games", 16 },
            { "Books", 10 },
            { "Cartoon & Animations", 32 },
            { "Celebrities", 26 },
            { "Comics", 29 },
            { "Computers", 18 },
            { "Film", 11 },
            { "Gadgets", 30 },
            { "General Knowledge", 9 },
            { "Geography", 22 },
            { "History", 23 },
            { "Mathematics", 19 },
            { "Music", 12 },
            { "Musicals & Theatres", 13 },
            { "Mythology", 20 },
            { "Politics", 24 },
            { "Science & Nature", 17 },
            { "Sports", 21 },
            { "Television", 14 },
            { "Vehicles", 28 },
            { "Video Games", 15 }
        };

        string[] difficulties = { "Mixed", "Easy", "Medium", "Hard" };

        public MainPage()
        {
            InitializeComponent();

            pickCategory.ItemsSource = categories.Keys.ToList();
            pickDifficulty.ItemsSource = difficulties;

            pickCategory.SelectedIndex = 0;
            pickDifficulty.SelectedIndex = 0;
            pickAmount.SelectedIndex = 0;

            fadeIn();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            fadeIn();
        }

        void fadeIn()
        {
            stackMain.Scale = 0.5;
            stackMain.Opacity = 0;
            frameMain.Scale = 0.5;
            frameMain.Opacity = 0;
            pathMain.Opacity = 0;

            stackMain.FadeTo(1, 500);
            stackMain.ScaleTo(1, 500);
            frameMain.FadeTo(1, 500);
            frameMain.ScaleTo(1, 500);
            pathMain.FadeTo(1, 500);
        }

        private void btnBegin_Clicked(object sender, EventArgs e)
        {
            short category = categories[pickCategory.SelectedItem.ToString()];
            string difficulty = pickDifficulty.SelectedItem.ToString().ToLower();

            lblError.IsVisible = false;
            btnBegin.IsVisible = false;
            loadingSpinner.IsVisible = true;

            Task.Run(async () => {
                await getQuestions(category, difficulty);

                Device.BeginInvokeOnMainThread(() => {
                    loadingSpinner.IsVisible = false;
                    btnBegin.IsVisible = true;

                    if (apiResponse == 0)
                    {
                        stackMain.ScaleTo(1.3, 200);
                        stackMain.FadeTo(0, 200);
                        frameMain.ScaleTo(1.3, 200);
                        frameMain.FadeTo(0, 200);
                        pathMain.FadeTo(0, 200);

                        Task.Run(async () => {
                            await Task.Delay(200);

                            Device.BeginInvokeOnMainThread(async () => {
                                await Navigation.PushAsync(new QuestionsPage(apiQuestions));
                            });
                        });
                    } else
                    {
                        lblError.Text = $"Error: {responses[apiResponse]}";
                        lblError.IsVisible = true;
                    }
                });
            });
        }

        async Task getQuestions(short category, string difficulty)
        {            
            string url = $"https://opentdb.com/api.php?amount={pickAmount.SelectedItem}&type=boolean";
            if (category != 0) url += $"&category={category}";
            if (difficulty != "mixed") url += $"&difficulty={difficulty}";

            Response resp = await restService.GetQuestions(url);

            apiResponse = resp.response_code;
            apiQuestions = resp.results;
        }
    }
}
