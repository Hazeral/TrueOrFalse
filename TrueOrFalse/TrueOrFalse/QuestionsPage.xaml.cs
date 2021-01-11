using Plugin.SimpleAudioPlayer;
using System;
using System.Net;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrueOrFalse
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuestionsPage : ContentPage
    {
        Question[] questions;
        short currentQuestion = 0;

        short score = 0;
        int elapsedTime;

        ISimpleAudioPlayer soundCorrect = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
        ISimpleAudioPlayer soundIncorrect = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
        ISimpleAudioPlayer soundStartFinish = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();

        public QuestionsPage(Question[] _questions)
        {
            InitializeComponent();

            soundCorrect.Load("correct.mp3");
            soundIncorrect.Load("incorrect.wav");
            soundStartFinish.Load("startfinish.mp3");

            questions = _questions;
            showQuestion();
            lblTime.Text = "Time: 00:00";

            stackMain.Scale = 1.5;
            stackMain.Opacity = 0;
            frameMain.Scale = 1.5;
            frameMain.Opacity = 0;
            btnCancel.Scale = 1.5;
            btnCancel.Opacity = 0;

            stackMain.ScaleTo(1, 300);
            stackMain.FadeTo(1, 300);
            frameMain.ScaleTo(1, 300);
            frameMain.FadeTo(1, 300);
            btnCancel.ScaleTo(1, 300);
            btnCancel.FadeTo(1, 300);

            Device.StartTimer(TimeSpan.FromSeconds(1), () => updateTime());
            soundStartFinish.Play();
        }

        bool updateTime()
        {
            elapsedTime++;

            lblTime.Text = string.Format("Time: {0:00}:{1:00}", elapsedTime / 60, (elapsedTime / 60) == 0 ? elapsedTime : elapsedTime - ((elapsedTime / 60) * 60));
            // Divided by 60 then multipled by 60 again as the remainder would be removed when divided

            return true;
        }

        void showQuestion()
        {
            lblScore.Text = $"Score: {score}";
            lblCurrentQ.Text = string.Format("{0:00}/{1}", currentQuestion + 1, questions.Length);
            lblQuestion.Text = WebUtility.HtmlDecode(questions[currentQuestion].question);
            lblCategory.Text = $"Category: {questions[currentQuestion].category}";

            string difficulty = questions[currentQuestion].difficulty[0].ToString().ToUpper() + questions[currentQuestion].difficulty.Substring(1);
            lblDifficulty.Text = $"Difficulty: {difficulty}";
        }

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void btnTrue_Clicked(object sender, EventArgs e)
        {
            if (questions[currentQuestion].correct_answer == "True") correctAnswer();
            else incorrectAnswer();

            newQuestion();
            showQuestion();
        }

        private void btnFalse_Clicked(object sender, EventArgs e)
        {
            if (questions[currentQuestion].correct_answer == "False") correctAnswer();
            else incorrectAnswer();

            newQuestion();
            showQuestion();
        }

        void correctAnswer()
        {
            score += 10;
            soundCorrect.Play();
        }

        void incorrectAnswer()
        {
            score -= 10;
            soundIncorrect.Play();
        }

        void newQuestion()
        {
            if (currentQuestion < questions.Length - 1) currentQuestion++;
            else showResults();
        }

        void showResults()
        {
            Navigation.InsertPageBefore(new ResultsPage(score, elapsedTime), this);
            Navigation.PopAsync();
            soundStartFinish.Play();
        }
    }
}