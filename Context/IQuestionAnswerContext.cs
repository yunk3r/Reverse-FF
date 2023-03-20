using Reverse_FF;

public interface IQuestionAnswerContext
{
    Task<QuestionAnswer> GetRandomQuestionAnswer();

    Task<bool> ProcessFile3();

    Task<bool> ProcessFile4();

    Task<bool> ProcessFile5();

    Task<bool> ProcessFile6();

    Task<bool> ProcessFile7();

    String Compare(String s1, String s2);
}