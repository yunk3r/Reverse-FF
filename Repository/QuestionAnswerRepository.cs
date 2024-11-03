using Couchbase.Extensions.DependencyInjection;
using Couchbase.KeyValue;
using Reverse_FF;

public class QuestionAnswerRepository : IQuestionAnswerRepository
{
    private readonly INamedBucketProvider _provider;
    
    public QuestionAnswerRepository (INamedBucketProvider provider)
    {
        _provider = provider;
    }

    public async Task<bool> AddQuestionAnswer(QuestionAnswer item)
    {
        try 
        {
            var collection = await (await _provider.GetBucketAsync()).DefaultCollectionAsync();
            var idResult = await collection.Binary.IncrementAsync($"{nameof(QuestionAnswer)}_Id");
            item.QuestionNumber = ((long)idResult.Content);
            await collection.InsertAsync<QuestionAnswer>(item.DocId, item);
            return true;
        } 
        catch (Exception ex)
        {
            Console.WriteLine("ERROR");
            return false;
        }
    }

        public async Task<QuestionAnswer> GetQuestionAnswerById(int Id)
    {
            var collection = await (await _provider.GetBucketAsync()).DefaultCollectionAsync();
            var qaResult = await collection.GetAsync($"{nameof(QuestionAnswer)}_{Id}");
            return qaResult.ContentAs<QuestionAnswer>();
    }

    public async Task<long> GetQuestionAnswerMaxId()
    {
            var collection = await (await _provider.GetBucketAsync()).DefaultCollectionAsync();
            var idResult = await collection.GetAsync($"{nameof(QuestionAnswer)}_Id");
            return idResult.ContentAs<long>();
    }
}
