using CommunityToolkit.Mvvm.ComponentModel;

// Change to a partial class and inherit from ObservableObject
public partial class QuestionObject : ObservableObject
{
    [ObservableProperty]
    private string _question;

    [ObservableProperty]
    private string _answerOne;

    [ObservableProperty]
    private string _answerTwo;

    [ObservableProperty]
    private int _answerOneCount;

    [ObservableProperty]
    private int _answerTwoCount;

    public QuestionObject(
    string question,
    string answerOne,
    string answerTwo,
    int answerOneCount,
    int answerTwoCount)
    {
        _question = question;
        _answerOne = answerOne;
        _answerTwo = answerTwo;
        _answerOneCount = answerOneCount;
        _answerTwoCount = answerTwoCount;
    }
}