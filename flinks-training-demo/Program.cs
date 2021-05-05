using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
    public class Program
    {
        public static void Main(string[] args)
        {
            var apiClient = new FlinksClient("43387ca6-0391-4c82-857d-70d95f087ecb", "https://toolbox-api.private.fin.ag");
            var response =  apiClient.Authorize("FlinksCapital", "Greatday", "Everyday", true, false, true, RequestLanguage.en, true);


            Console.WriteLine(" hi Main Method");
            Console.WriteLine((response.SecurityChallenges).First().Prompt);
            


            SecurityChallenge sc = (response.SecurityChallenges).First();


            //need to use input from user
           // String answer = "";
            
           //answer = (string) Console.ReadLine();

           sc.Answer = "Triangle";
            
            //Console.WriteLine("You answered = '{0}' ",  answer);

            var resp2 = apiClient.AnswerMfaQuestionsAndAuthorize((System.Guid) response.RequestId,response.SecurityChallenges);

            
            Console.WriteLine(resp2);

           // CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
