using Cashbox.Models;
using Cashbox.Services;
using Cashbox.Tests.Fake;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace Cashbox.Specs
{
    [Binding]
    public class CashboxCalculatuinDiscountSteps
    {
        private readonly FakeUnitOfWorkFactory _fakeUnitOfWorkFactory;
        private readonly FakeRepository<Account> _fakeAccountRepository;
        private readonly FakeRepository<Order> _fakeOrderRepository;
        private readonly FakeRepository<Product> _fakeProductRepository;
        private readonly PurchaseService _service;

        public CashboxCalculatuinDiscountSteps()
        {
            _fakeAccountRepository = new FakeRepository<Account>();
            _fakeOrderRepository = new FakeRepository<Order>();
            _fakeProductRepository = new FakeRepository<Product>();

            _fakeUnitOfWorkFactory = new FakeUnitOfWorkFactory(
                uow =>
                {
                    uow.SetRepository(_fakeAccountRepository);
                    uow.SetRepository(_fakeOrderRepository);
                    uow.SetRepository(_fakeProductRepository);
                });
            _service = new PurchaseService(_fakeUnitOfWorkFactory);
            TestSetup();
        }

        public void TestSetup()
        {
            // Prepare here our fake dependencies.
            var account1 = new Account { Id = 1, Name = "Account1", Balance = 100.5m };
            var account2 = new Account { Id = 2, Name = "Account2", Balance = 2000.17m };

            var order1 = new Order
            {
                Id = 1,
                AccountId = 1,
                Account = account1,
                Date = DateTime.Now,
                Total = PurchaseService.ORDERS_HISTORY_DISCOUNT_THRESHOLD - 10
            };
            var order2 = new Order { Id = 2, AccountId = 1, Account = account1, Date = DateTime.Now, Total = 20.1m };
            var order3 = new Order { Id = 3, AccountId = 2, Account = account2, Date = DateTime.Now, Total = 170.87m };

            var product1 = new Product { Id = 1, Title = "Product1", Price = 250.99m, Amount = 2 };
            var product2 = new Product { Id = 2, Title = "Product2", Price = 50.5m, Amount = 1 };
            var product3 = new Product { Id = 3, Title = "Product3", Price = 70.15m, Amount = 10 };
            var product4 = new Product { Id = 4, Title = "Product4", Price = 10.75m, Amount = 0 };

            _fakeAccountRepository.Data.AddRange(new[] { account1, account2 });
            _fakeOrderRepository.Data.AddRange(new[] { order1, order2, order3 });
            _fakeProductRepository.Data.AddRange(new[] { product1, product2, product3, product4 });
        }

        private int _AccountID;
        private decimal _TotalPrice;
        private decimal _Discount;

        [Given(@"I have entered (.*) as a account id")]
        public void GivenIHaveEnteredAsAAccountId(int accountID)
        {
            _AccountID = accountID;
        }
        
        [Given(@"I have entered (.*)m as a total price")]
        public void GivenIHaveEnteredMAsATotalPrice(Decimal totalPrice)
        {
            _TotalPrice = totalPrice;
        }
        
        [When(@"I call method service method GetDiscount")]
        public void WhenICallMethodServiceMethodGetDiscount()
        {
            _Discount = _service.GetDiscount(_AccountID, _TotalPrice);
        }
        
        [Then(@"the result should be (.*)")]
        public void ThenTheResultShouldBe(Decimal discount)
        {
            Assert.That(_Discount, Is.EqualTo(discount));
        }
    }
}
