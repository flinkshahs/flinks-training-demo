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
  public class AccountDetails
  {

    public string title { get; set; }

    public string accountNumber { get; set; }

    public double? availableBalance { get; set; }

    public double? currentBalance { get; set; }

    public double? limitBalance { get; set; }

    public string category { get; set; }

    public string currency { get; set; }

    public string name { get; set; }

    public string civicAddress { get; set; }

    public string city { get; set; }

    public string province { get; set; }

    public string postalCode { get; set; }

    public string Country { get; set; }

    public string email { get; set; }

    public string phoneNumber { get; set; }

    public string id { get; set; }

  }
}
