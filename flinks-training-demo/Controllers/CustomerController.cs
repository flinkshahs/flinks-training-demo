using System;
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
using Newtonsoft.Json;



namespace flinks_training_demo.Controllers
{
  [ApiController]
  [Route("[controller]/[action]")]
  public class CustomerController : Controller
  {
    private readonly ILogger<CustomerController> _logger;
    private readonly HttpClient client = new HttpClient();

    private Customer c;

    private MessageMFA m;

    private List<AccountSummary> accountSummary;

    private List<AccountDetails> accountDetails;
    private FlinksClient apiClient = new FlinksClient("43387ca6-0391-4c82-857d-70d95f087ecb", "https://toolbox-api.private.fin.ag");

    public CustomerController(ILogger<CustomerController> logger)
    {
      _logger = logger;
    }

    [HttpPost, ActionName("Login")]
    public Customer PostCredentials([FromBody] List<string> credentials)
    {
      c = new Customer();
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
        c.requestId = (Guid)response.RequestId;
        Console.WriteLine(c.requestId.ToString());
        c.securityChallenge = securityChallenge;
        c.securityChallenges = response.SecurityChallenges;
        c.answer = null;
        TempData["Customer"] = JsonConvert.SerializeObject(c);
        return c;
      }
      else
      {
        return c;
      }
    }

    [HttpPost, ActionName("Answer")]
    public MessageMFA PostAnswerMFA([FromBody] List<string> answer)
    {
      m = new MessageMFA();
      c = JsonConvert.DeserializeObject<Customer>((string)TempData["Customer"]);
      // apiClient = JsonConvert.DeserializeObject<FlinksClient>((string)TempData["Client"]);

      Console.WriteLine("Yay it's being called for ANSWER.");

      foreach (var securityChallenge in c.securityChallenges)
      {
        Console.WriteLine(securityChallenge.Prompt);
        Console.WriteLine(answer[0]);
        securityChallenge.Answer = answer[0];
      }

      apiClient.ClientStatus = ClientStatus.PENDING_MFA_ANSWERS;
      Console.WriteLine(c.requestId.ToString());
      var response = apiClient.AnswerMfaQuestionsAndAuthorize(c.requestId, c.securityChallenges);

      TempData["Customer"] = JsonConvert.SerializeObject(c);
      m.message = response.Message;
      Console.WriteLine(response.Message);
      return m;
    }

    [HttpGet, ActionName("GetAccountSummary")]
    public List<AccountSummary> GetAccountSummary()
    {
      c = JsonConvert.DeserializeObject<Customer>((string)TempData["Customer"]);

      Console.WriteLine("Yay it's being called for Summary.");

      apiClient.ClientStatus = ClientStatus.AUTHORIZED;
      Console.WriteLine(c.requestId);
      AccountsSummaryResult response = apiClient.GetAccountSummary(c.requestId);

      List<Account> accounts = response.Accounts;

      accountSummary = new List<AccountSummary>();


      // Console.WriteLine(response.Accounts.First().Title);

      foreach (var account in accounts)
      {
        AccountSummary a = new AccountSummary();
        Console.WriteLine("This is account title" + account.Title);
        a.title = account.Title;
        a.accountNumber = account.AccountNumber;
        a.availableBalance = account.Balance.Available;
        a.currentBalance = account.Balance.Current;
        a.limitBalance = account.Balance.Limit;
        a.category = account.Category;
        a.currency = account.Currency;
        a.name = account.Holder.Name;
        a.civicAddress = account.Holder.Address.CivicAddress;
        a.city = account.Holder.Address.City;
        a.province = account.Holder.Address.Province;
        a.postalCode = account.Holder.Address.PostalCode;
        a.Country = (string)account.Holder.Address.Country;
        a.email = account.Holder.Email;
        a.phoneNumber = account.Holder.PhoneNumber;
        a.id = account.Id.ToString();
        accountSummary.Add(a);
      }
      TempData["Customer"] = JsonConvert.SerializeObject(c);

      return accountSummary;
    }

    [HttpGet, ActionName("GetAccountDetail")]
    public List<AccountDetails> GetAccountDetail()
    {
      c = JsonConvert.DeserializeObject<Customer>((string)TempData["Customer"]);

      Console.WriteLine("Yay it's being called for Details.");

      apiClient.ClientStatus = ClientStatus.AUTHORIZED;
      Console.WriteLine(c.requestId);
      AccountsDetailResult response = apiClient.GetAccountDetails(c.requestId, true, true, true, true, DaysOfTransaction.Days90);

      List<Account> accounts = response.Accounts;

      accountDetails = new List<AccountDetails>();


      // Console.WriteLine(response.Accounts.First().Title);

      foreach (var account in accounts)
      {
        AccountDetails a = new AccountDetails();
        Console.WriteLine("This is account title" + account.Title);
        a.title = account.Title;
        a.accountNumber = account.AccountNumber;
        a.availableBalance = account.Balance.Available;
        a.currentBalance = account.Balance.Current;
        a.limitBalance = account.Balance.Limit;
        a.category = account.Category;
        a.currency = account.Currency;
        a.name = account.Holder.Name;
        a.civicAddress = account.Holder.Address.CivicAddress;
        a.city = account.Holder.Address.City;
        a.province = account.Holder.Address.Province;
        a.postalCode = account.Holder.Address.PostalCode;
        a.Country = (string)account.Holder.Address.Country;
        a.email = account.Holder.Email;
        a.phoneNumber = account.Holder.PhoneNumber;
        a.id = account.Id.ToString();
        List<Transaction> lt = new List<Transaction>();
        foreach (var transaction in account.Transactions)
        {
          Transaction t = new Transaction();
          t.Balance = transaction.Balance;
          t.Date = transaction.Date.ToString();
          t.Debit = transaction.Debit;
          t.Credit = transaction.Credit;
          t.Description = transaction.Description;
          t.Code = (string)transaction.Code;
          t.Id = transaction.Id.ToString();
          lt.Add(t);
        }
        a.transactions = lt;
        accountDetails.Add(a);
      }
      TempData["Customer"] = JsonConvert.SerializeObject(c);

      return accountDetails;
    }

  }
}
