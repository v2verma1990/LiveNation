using Microsoft.Extensions.Caching.Memory;
using SETest.Models;
using SETest.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SETest.Services
{
    public class LiveNationService : ILiveNationService
    {
        private static IMemoryCache memoryCache;

        List<IRuleValidator> ruleValidators;
        public LiveNationService()
        {
            if (memoryCache == null)
            {
                memoryCache = new MemoryCache(new MemoryCacheOptions());
            }

            this.ruleValidators = new List<IRuleValidator>();
            this.ruleValidators.Add(new LiveRuleValidator());
            this.ruleValidators.Add(new NationRuleValidator());
            this.ruleValidators.Add(new LiveNationRuleValidator());
        }

        public LiveNationSummaryResponseDto GetLiveNationSummary(int start, int end)
        {
            var response = new LiveNationSummaryResponseDto();

            string result = string.Empty;
            Dictionary<string, int> numberCounts = new Dictionary<string, int>();
            
            for (int i = start; i <=end; i++)
            {
                string ruleResponse = string.Empty;

                var existInCache = memoryCache.TryGetValue(i, out ruleResponse);
                if (!existInCache)
                {
                    ruleResponse = this.ApplyRule(i);
                    using (var entry = memoryCache.CreateEntry(i))
                    {
                        entry.Value = ruleResponse;
                        entry.AbsoluteExpiration = DateTime.UtcNow.AddDays(1);
                    }
                }

                result += ruleResponse == null ? i + " " : ruleResponse + " ";
                this.insertInDictionary(numberCounts, ruleResponse, "Integer");
            }

            response.Result = result.Trim();
            response.Summary = numberCounts;
            return response;
        }

        private string ApplyRule(int number)
        {
            foreach (var ruleValidator in this.ruleValidators)
            {
                var isValidate = ruleValidator.ApplyRule(number);
                if (isValidate)
                {
                    return ruleValidator.getDescription();
                }
            }
            return null;
        }

        private void insertInDictionary(Dictionary<string, int> dict, string key, string defaultKey)
        {
            if(key == null)
            {
                key = defaultKey;
            }

            if(!dict.ContainsKey(key))
            {
                dict[key] = 1;
            }
            else
            {
                dict[key]++;
            }
        }
    }
}