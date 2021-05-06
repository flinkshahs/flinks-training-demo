using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Flinks.CSharp.SDK;
using Flinks.CSharp.SDK.Model.Attributes;
using Flinks.CSharp.SDK.Model.Authorize;
using Flinks.CSharp.SDK.Model.Constant;
using Flinks.CSharp.SDK.Model.DeleteCard;
using Flinks.CSharp.SDK.Model.Enums;
using Flinks.CSharp.SDK.Model.GetAccountsDetail;
using Flinks.CSharp.SDK.Model.GetAccountsSummary;
using Flinks.CSharp.SDK.Model.GetStatement;
using Flinks.CSharp.SDK.Model.Score;
using Flinks.CSharp.SDK.Model.Shared;



namespace flinks_training_demo.Controllers
{
  [ApiController]
  [Route("[controller]/[action]")]
  public class CustomerController : ControllerBase
  {
    private readonly ILogger<CustomerController> _logger;
    private readonly HttpClient client = new HttpClient();

    private Customer c = new Customer();

    private MessageMFA m = new MessageMFA();

    private FlinksClient apiClient = new FlinksClient("43387ca6-0391-4c82-857d-70d95f087ecb", "https://toolbox-api.private.fin.ag");

    public CustomerController(ILogger<CustomerController> logger)
    {
      _logger = logger;
    }

    [HttpPost, ActionName("Login")]
    public Customer PostCredentials([FromBody] List<string> credentials)
    {
      Console.WriteLine("Yay it's being called.");
      var response = apiClient.Authorize("FlinksCapital", credentials[0], credentials[1], true, false, true, RequestLanguage.en, true);

      bool flag = true;
      string securityChallenge = null;
      try
      {
        securityChallenge = response.SecurityChallenges.First().Prompt;
        Console.WriteLine(securityChallenge);
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        flag = false;
      }

      if (flag)
      {
        c.username = credentials[0];
        Console.WriteLine("This is username" + c.username);
        c.requestId = response.RequestId.Value;
        c.securityChallenge = securityChallenge;
        c.securityChallenges = response.SecurityChallenges;
        c.answer = null;
        return c;
      }
      else
      {
        return c;
      }
    }

    [HttpPost, ActionName("Answer")]
    public MessageMFA PostAnswerMFA([FromForm] String answer)
    {
      Console.WriteLine("Yay it's being called for ANSWER.");

      Console.WriteLine("This is username" + c.username);

      foreach (var securityChallenge in c.securityChallenges)
      {
        Console.WriteLine(securityChallenge.Prompt);
        securityChallenge.Answer = answer;
      }


      var response = apiClient.AnswerMfaQuestionsAndAuthorize(c.requestId, c.securityChallenges);

      m.message = response.Message;
      return m;
    }

  }
}
