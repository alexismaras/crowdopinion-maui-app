using CrowdOpinion.Models;
using CrowdOpinion.ViewModels;

namespace CrowdOpinion.Pages;

public partial class Answer : ContentPage
{
	public Answer(AnswerViewModel answerViewModel)
	{
		InitializeComponent();
		BindingContext = answerViewModel;
	}
}