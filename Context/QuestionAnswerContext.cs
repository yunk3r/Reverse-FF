using Reverse_FF;


public class QuestionAnswerContext : IQuestionAnswerContext
{
    private IQuestionAnswerRepository _QuestionAnswerRepository;
    public QuestionAnswerContext (IQuestionAnswerRepository QuestionAnswerRepository)
    {
        _QuestionAnswerRepository = QuestionAnswerRepository;
    }
    public string Compare(string s1, string s2)
    {
        return "1";
    }

    public async Task<QuestionAnswer> GetRandomQuestionAnswer()
    {
        var max = await _QuestionAnswerRepository.GetQuestionAnswerMaxId();
        var randomId = new Random().Next(1, ((int)max) + 1);
        return await _QuestionAnswerRepository.GetQuestionAnswerById(randomId);
    }

     public async Task<bool> ProcessFile3()
     {
        using (var sr = new StreamReader("DataFiles/3_Answers.csv"))
        {
            while(sr.Peek() >= 0)
            {
                var line = sr.ReadLine();
                var parts = line?.Split("|").ToList();
                if(parts?.Count == 7)
                {
                    await Process(parts);
                }
                else
                {
                    Console.WriteLine($"Not enought items {line}");
                }
            }
            return true;
            
        }
     }

     public async Task<bool> ProcessFile4()
     {
        using (var sr = new StreamReader("DataFiles/4_Answers.csv"))
        {
            while(sr.Peek() >= 0)
            {
                var line = sr.ReadLine();
                var parts = line?.Split("|").ToList();
                if(parts?.Count == 9)
                {
                    await Process(parts);
                }
                else
                {
                    Console.WriteLine($"Not enought items {line}");
                }
            }
            return true;
            
        }
    }

    public async Task<bool> ProcessFile5()
    {
        using (var sr = new StreamReader("DataFiles/5_Answers.csv"))
        {
            while(sr.Peek() >= 0)
            {
                var line = sr.ReadLine();
                var parts = line?.Split("|").ToList();
                if(parts?.Count == 11)
                {
                    await Process(parts);
                }
                else
                {
                    Console.WriteLine($"Not enought items {line}");
                }
            }
            return true;
            
        }
    }

    public async Task<bool> ProcessFile6()
    {
        using (var sr = new StreamReader("DataFiles/6_Answers.csv"))
        {
            while(sr.Peek() >= 0)
            {
                var line = sr.ReadLine();
                var parts = line?.Split("|").ToList();
                if(parts?.Count == 13)
                {
                   await Process(parts);
                }
                else
                {
                    Console.WriteLine($"Not enought items {line}");
                }
            }
            return true;
            
        }
    }

    public async Task<bool> ProcessFile7()
    {
        using (var sr = new StreamReader("DataFiles/7_Answers.csv"))
        {
            while(sr.Peek() >= 0)
            {
                var line = sr.ReadLine();
                var parts = line?.Split("|").ToList();
                if(parts?.Count == 15)
                {
                    await Process(parts);
                }
                else
                {
                    Console.WriteLine($"Not enought items {line}");
                }
            }
            return true;
            
        }
    }

    private async Task<bool> Process (List<String> parts)
    {
        try
        {
            var questionAnswer = new QuestionAnswer
            {
                Question = parts[0]
            };

            for(int i = 1; i < parts.Count; i = i + 2)
            {
                var answer = new Answer
                {
                    AnswerText = parts[i],
                    Points = Int32.Parse(parts[i + 1])
                };
                questionAnswer.Answers.Add(answer);
            }
            if(questionAnswer.Answers.All(x => int.TryParse(x.AnswerText, out _)))
            {
                Console.WriteLine("All numbers. Skipping");
            }
            else
            {
              //  await _QuestionAnswerRepository.AddQuestionAnswer(questionAnswer);
            }
        }
        catch(Exception e)
        {
            Console.WriteLine(e.GetBaseException());
        }  
        return true;     
    }
}
