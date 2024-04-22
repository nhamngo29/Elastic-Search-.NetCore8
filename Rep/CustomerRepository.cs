using Bogus;
using Domain.Models;
using Elastic;
using Elastic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rep
{
    public class CustomerRepository
    {
        private readonly IElasticService<Customer> _customerService;

        public CustomerRepository(
            IElasticService<Customer> customerService)
        {
            _customerService = customerService;
        }
        public Customer GetCustomerTestData()
        {
            var accountFaker = new Faker<AccountDetail>()
               .RuleFor(r => r.AccountName, f => f.Finance.AccountName())
               .RuleFor(r => r.Balance, f => $"{f.Finance.Currency().Symbol}")
               .RuleFor(r => r.BalanceValue, f => f.Finance.Amount());

            var customerFaker = new Faker<Customer>().CustomInstantiator(ci => new Customer())
                .RuleFor(r => r.Name, f => f.Name.FullName())
                .RuleFor(r => r.Country, f => f.Address.Country())
                .RuleFor(r => r.Company, f => f.Company.CompanyName())
                .RuleFor(r => r.Account, f => accountFaker.Generate())
                .RuleFor(r => r.SecurityNo, f => f.Finance.Account(length: 5));

            var customer = customerFaker.Generate();

            customer.RefreshSymbol();
            return customer;
        }
        public async Task<(long Count, IEnumerable<Customer> Documents)> GetCustomersAsync(GridQueryModel gridQueryModel)
        {
            return await _customerService.GetDocumentsAsync(gridQueryModel);
        }

    }
}
