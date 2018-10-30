using SafePatient.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SafePatient.Core.Services {
    interface IApiService {
        Task GetTokenAsync();
        Task<Trade> SearchTradeAsync(string searchTerm);
        Task<Interaction> CheckInteractionsAsync(string[] medicationIds);
        Task<ProviderDrugInfo> ProviderSDLAsync(string medicationId);
    }
}
