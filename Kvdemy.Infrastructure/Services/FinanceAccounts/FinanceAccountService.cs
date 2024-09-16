using AutoMapper;
using Kvdemy.Core.Constant;
using Kvdemy.Core.Constants;
using Kvdemy.Core.Dtos;
using Kvdemy.Core.Dtos.Helpers;
using Kvdemy.Core.Enums;
using Kvdemy.Core.Exceptions;
using Kvdemy.Core.Resourses;
using Kvdemy.Core.ViewModels;
using Kvdemy.Data;
using Kvdemy.Data.Models;
using Kvdemy.Infrastructure.Services.Auth;
using Kvdemy.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Infrastructure.Services.Wallet
{
    public class FinanceAccountService : IFinanceAccountService
    {

        private readonly KvdemyDbContext _db;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly IStringLocalizer<Messages> _localizedMessages;

        public FinanceAccountService(KvdemyDbContext db, IMapper mapper, IAuthService authService , IStringLocalizer<Messages> localizedMessages)
        {
            _db = db;
            _mapper = mapper;
            _authService = authService;
            _localizedMessages = localizedMessages;

        }




        public async Task<FinanceAccountViewModel> GetWalletTransaction()
        {

            string userId = await _authService.GetUserIdFromToken();
            var query = await _db.FinanceAccounts.Include(x => x.AccountTransactions)
                         .FirstOrDefaultAsync(x => !x.IsDelete && x.UserId == userId);

            if (query != null)
            {
                // Order transactions in descending order by TransactionDate
                query.AccountTransactions = query.AccountTransactions.OrderByDescending(t => t.CreatedAt).ToList();
            }


            var modelViewModels = _mapper.Map<FinanceAccountViewModel>(query);

             


            return modelViewModels;
        }
        public async Task<int> AddTransaction(CreateTransactionDto dto)
        {

            string userId = await _authService.GetUserIdFromToken();

            if (dto is null)
            {
                throw new InvalidDateException();
            }




            var model = _mapper.Map<AccountTransactions>(dto);


            await _db.AccountTransactions.AddAsync(model);
            await _db.SaveChangesAsync();




            UpdateFinancialAccountDto financeDto = new UpdateFinancialAccountDto();
            financeDto.Balance = dto.ValueAfterDiscount;

            await UpdateFinanceAccount(financeDto);




            return model.Id;
        }


        public async Task<int> UpdateFinanceAccount(UpdateFinancialAccountDto dto)
        {
            string userId = await _authService.GetUserIdFromToken();
            var financeAccount = await _db.FinanceAccounts.Include(x => x.AccountTransactions)
                         .FirstOrDefaultAsync(x => !x.IsDelete && x.UserId == userId);


            financeAccount.Balance = dto.Balance;

            _db.FinanceAccounts.Update(financeAccount);
            await _db.SaveChangesAsync();



            return 0;
        }

        public async Task<PaginationWebViewModel> GetAllWalletRequests(Pagination pagination, Query query)
        {

            var queryString = _db.WalletRequests.Include(x => x.User).Where(x => !x.IsDelete && (x.User.FirstName.Contains(query.GeneralSearch) || string.IsNullOrWhiteSpace(query.GeneralSearch))).AsQueryable();

            var dataCount = queryString.Count();
            pagination.Total = dataCount;
            var skipValue = pagination.GetSkipValue();
            var dataList = await queryString.Skip(skipValue).Take(pagination.PerPage).ToListAsync();
            var products = _mapper.Map<List<WalletRequestViewModel>>(dataList);
            var pages = pagination.GetPages(dataCount);

            var result = new PaginationWebViewModel
            {
                data = products,
                meta = new Meta
                {
                    page = pagination.Page,
                    perpage = pagination.PerPage,
                    pages = pages,
                    total = dataCount
                }
            };

            return result;
        }

        public async Task<dynamic> AddWalletRequest(double amount)
        {
            IQueryable<Settings> query = _db.Settings;

            var model = query.FirstOrDefault();
            var modelViewModels = _mapper.Map<SettingsViewModel>(model);
            int min = (int) modelViewModels.MinimumWithdrawal;


            if(amount < min)
            {
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.InvalidMinimumWithdrawal]);

            }
            else
            {
                string userId = await _authService.GetUserIdFromToken();
                var WalletReq = new WalletRequest();
                WalletReq.Amount = amount;
                WalletReq.UserId = userId;
                var financeAccount = await _db.FinanceAccounts.FirstOrDefaultAsync(x => !x.IsDelete && x.UserId == userId);

                if(amount > financeAccount.Balance)
                {
                    return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.NoEnoughBalance]);
                }
                else
                {
                    await _db.WalletRequests.AddAsync(WalletReq);
                    await _db.SaveChangesAsync();
                    return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.WalletRequestSuccess]);
                }
            }
        }

        public async Task<int> AcceptWalletRequest(int id)
        {
            var model = await _db.WalletRequests.Include(x => x.User).ThenInclude(x => x.FinanceAccount).SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            string userId = model.UserId;


            CreateTransactionDto dtoTransaction = new CreateTransactionDto();
            dtoTransaction.TransactionPaymentType = TransactionPaymentType.Wallet;
            dtoTransaction.TransactionType = TransactionType.Withdraw;
            dtoTransaction.ValueDiscount = 0.0;
            dtoTransaction.ValueBeforDiscount = 0.0;
            dtoTransaction.ValueAfterDiscount = model.Amount;
            dtoTransaction.FinanceAccountId = model.User.FinanceAccount.Id;



            var Tmodel = _mapper.Map<AccountTransactions>(dtoTransaction);


            await _db.AccountTransactions.AddAsync(Tmodel);
            await _db.SaveChangesAsync();



            var financeAccount = await _db.FinanceAccounts.Include(x => x.AccountTransactions)
                         .FirstOrDefaultAsync(x => !x.IsDelete && x.UserId == userId);


            financeAccount.Balance -= dtoTransaction.ValueAfterDiscount;

            _db.FinanceAccounts.Update(financeAccount);
            await _db.SaveChangesAsync();


            model.Status = WalletRequestStatus.Accepted;
            _db.WalletRequests.Update(model);
            await _db.SaveChangesAsync();

            return model.Id;
        }

        public async Task<string> RejectWalletRequest(int id)
        {
            var model = await _db.WalletRequests.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            model.Status = WalletRequestStatus.Rejected;
            _db.WalletRequests.Update(model);
            await _db.SaveChangesAsync();

            return model.UserId;
        }



    }
}
