using EnterpriseCQRS.Api.Controllers;
using EnterpriseCQRS.Data.Model;
using EnterpriseCQRS.Domain.Commands.ProductCommand;
using EnterpriseCQRS.Domain.Responses;
using EnterpriseCQRS.Services.CommandHandlers.ProductCommandHandler;
using EnterpriseCQRS.Services.CommandHandlers.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnterpriseCQRS.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private SqLiteDbFake sqLiteDbFake;
        public IMediator _mediator { get; }

        [TestInitialize]
        public void Init()
        {
            sqLiteDbFake = new SqLiteDbFake();
        }

        [TestMethod]
        public async Task GetData_GetTransaction()
         {
            var context = sqLiteDbFake.GetDbContext();

            var url = new Uri("http://quiet-stone-2094.herokuapp.com/transactions.json");

            var response = new GenericResponse<IList<Transaction>>();

            var transactions = new Utilities<Transaction>();

            context.Database.ExecuteSqlRaw("DELETE FROM [Transaction]");

            var responses = await transactions.test(url);
            await context.Transaction.AddRangeAsync(responses.Result);
            await context.SaveChangesAsync();

            response.Result = responses.Result;

            Assert.IsNotNull(response.Result);
        }

        [TestMethod]
        public async Task GetData_GetRates()
        {
            var context = sqLiteDbFake.GetDbContext();

            var url = new Uri("http://quiet-stone-2094.herokuapp.com/rates.json");

            var response = new GenericResponse<IList<Rates>>();

            var rates = new Utilities<Rates>();

            context.Database.ExecuteSqlRaw("DELETE FROM [Transaction]");

            var responses = await rates.test(url);
            await context.Rates.AddRangeAsync(responses.Result);
            await context.SaveChangesAsync();

            response.Result = responses.Result;

            Assert.IsNotNull(response);
        }
    }
}
