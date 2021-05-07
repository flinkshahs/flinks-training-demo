using System;
using System.Collections.Generic;
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

namespace flinks_training_demo
{
  public class AccountSummary
  {
    public long transitNumber { get; set; }

    public long institutionNumber { get; set; }

    public float overdraftLimit { get; set; }

    public string title { get; set; }

    public string accountNumber { get; set; }

    public float availableBalance { get; set; }

    public float currentBalance { get; set; }

    public float limitBalance { get; set; }

    public string category { get; set; }

    public string type { get; set; }

    public string currency { get; set; }

    public string holder { get; set; }

    public string loginId { get; set; }

  }
}
