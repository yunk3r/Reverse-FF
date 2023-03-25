using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Reverse_FF.Controllers;

[ApiController]
[Route("[controller]")]
public class QuestionAnswerController : ControllerBase
{
    private IQuestionAnswerContext _QuestionAnswerContext;

    public QuestionAnswerController(IQuestionAnswerContext QuestionAnswerContext)
    {
        _QuestionAnswerContext = QuestionAnswerContext;
    }


    [Route("GetRandomQuestionAnswer")]
    [HttpGet]
    public async Task<IActionResult> GetRandomQuestionAnswer()
    {
        try
        {
            var item = await _QuestionAnswerContext.GetRandomQuestionAnswer();
            return Ok(item);
        }
        catch(Exception e)
        {
            return BadRequest(null);
        }
    }

    [Route("Process3")]
    [HttpGet]
    public async Task<bool> Process3()
    {
        return await _QuestionAnswerContext.ProcessFile3();
    }

    [Route("Process4")]
    [HttpGet]
    public async Task<bool> Process4()
    {
        return await _QuestionAnswerContext.ProcessFile4();
    }

    [Route("Process5")]
    [HttpGet]
    public async Task<bool> Process5()
    {
        return await _QuestionAnswerContext.ProcessFile5();
    }

    [Route("Process6")]
    [HttpGet]
    public async Task<bool> Process6()
    {
        return await _QuestionAnswerContext.ProcessFile6();
    }

    [Route("Process7")]
    [HttpGet]
    public async Task<bool> Process7()
    {
        return await _QuestionAnswerContext.ProcessFile7();
    }

    // [Route("Compare/{s1}/{s2}")]
    // [HttpGet]
    // public  string Compare(string s1, string s2)
    // {
    //     return _QuestionAnswerContext.Compare(s1,s2);
    // }
}
