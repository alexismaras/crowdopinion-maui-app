using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrowdOpinion.Models;
using CrowdOpinion.Services;
using System.Diagnostics;
using ImageCropper.Maui;


namespace CrowdOpinion.ViewModels
{
    public partial class AskViewModel : ObservableObject
    {
        private readonly IDataService _dataService;

        public AskViewModel(IDataService dataService)
        {
            _dataService = dataService;
        }

        [ObservableProperty]
        private string _questionText;

        [ObservableProperty]
        private string _answerOneText;

        [ObservableProperty]
        private string _answerTwoText;

        [ObservableProperty]
        private string _answerOneImage;

        [ObservableProperty]
        private string _answerTwoImage;

        [RelayCommand]
        public async Task FrameOneTapped()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.PickPhotoAsync();

                if (photo != null)
                {
                    AnswerOneImage = photo.FullPath;
                }
            }

        }

        [RelayCommand]
        public async Task FrameTwoTapped()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.PickPhotoAsync();

                if (photo != null)
                {
                    AnswerTwoImage = photo.FullPath;
                }
            }

        }

        [RelayCommand]
        public async Task PickImageOne()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.PickPhotoAsync();

                if (photo != null)
                {
                    AnswerOneImage = photo.FullPath;
                }
            }

        }

        [RelayCommand]
        public async Task PickImageTwo()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.PickPhotoAsync();

                if (photo != null)
                {
                    AnswerTwoImage = photo.FullPath;
                }

            }
        }

        [RelayCommand]
        private async Task AskQuestion()
        {
            try
            {
                if (!string.IsNullOrEmpty(QuestionText) &&
                    !string.IsNullOrEmpty(AnswerOneText) &&
                    !string.IsNullOrEmpty(AnswerTwoText) &&
                    !string.IsNullOrEmpty(AnswerOneImage) &&
                    !string.IsNullOrEmpty(AnswerTwoImage))
                {
                    QuestionObjectSupa questionObjectSupa = new()
                    {
                        Question = QuestionText,
                        AnswerOne = AnswerOneText,
                        AnswerTwo = AnswerTwoText,
                        AnswerOneCount = 0,
                        AnswerTwoCount = 0,
                    };
                    await _dataService.CreateQuestionObject(questionObjectSupa, AnswerOneImage, AnswerTwoImage);

                    QuestionText = string.Empty;
                    AnswerOneText = string.Empty;
                    AnswerTwoText = string.Empty;
                    AnswerOneImage = string.Empty;
                    AnswerTwoImage = string.Empty;

                    await Shell.Current.GoToAsync("//ProfileShellTab");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Fill All", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }

        }
    }
}

