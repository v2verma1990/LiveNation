using SETest.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SETest.Services
{
    public class LiveRuleValidator : IRuleValidator
    {
        public bool ApplyRule(int number)
        {
            return number % 3 == 0 && number % 5 != 0;
        }

        public string getDescription()
        {
            return "Live";
        }
    }
}