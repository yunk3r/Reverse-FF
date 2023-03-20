using Reverse_FF;

public interface IQuestionAnswerRepository
{
    Task<bool> AddQuestionAnswer(QuestionAnswer item);
    Task<QuestionAnswer> GetQuestionAnswerById(int Id);
    Task<long> GetQuestionAnswerMaxId();
}