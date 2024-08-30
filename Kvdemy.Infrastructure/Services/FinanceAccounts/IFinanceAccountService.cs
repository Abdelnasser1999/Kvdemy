using Kvdemy.Core.Dtos;
using Kvdemy.Core.Dtos.Helpers;
using Kvdemy.Core.ViewModels;
using Kvdemy.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Infrastructure.Services.Wallet
{
    public interface IFinanceAccountService
    {
        Task<FinanceAccountViewModel> GetWalletTransaction();
        Task<int> AddTransaction(CreateTransactionDto dto);
        Task<dynamic> AddWalletRequest(double amount);
        Task<PaginationWebViewModel> GetAllWalletRequests(Pagination pagination, Query query);

        Task<int> AcceptWalletRequest(int id);
        Task<string> RejectWalletRequest(int id);
    }
}
