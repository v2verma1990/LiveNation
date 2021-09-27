using SETest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETest.Services.Interface
{
    public interface ILiveNationService
    {
        LiveNationSummaryResponseDto GetLiveNationSummary(int start, int end);
    }
}
