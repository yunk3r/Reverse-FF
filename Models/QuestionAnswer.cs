using System.Collections.Generic;

namespace Reverse_FF;

public class QuestionAnswer
{
    public string DocId => $"{DocType}_{QuestionNumber}";

    public string DocType { get; set; } = nameof(QuestionAnswer);

    public long QuestionNumber { get; set; }

    public string Question { get; set; }

    public List<Answer> Answers { get; set; } = new List<Answer>(); 

}

public class Answer
{
    public string AnswerText { get; set; }

    public int Points { get; set; }

}
