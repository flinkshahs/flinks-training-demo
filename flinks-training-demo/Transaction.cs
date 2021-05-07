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
  public class Transaction
  {

    public double? Balance { get; set; }

    public string Date { get; set; }

    public double? Debit { get; set; }

    public string Description { get; set; }

    public double? Credit { get; set; }

    public string Code { get; set; }

    public string Id { get; set; }

  }
}
