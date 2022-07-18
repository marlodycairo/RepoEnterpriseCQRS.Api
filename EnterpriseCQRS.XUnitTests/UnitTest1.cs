using EnterpriseCQRS.Api.Controllers;
using EnterpriseCQRS.Data.Model;
using EnterpriseCQRS.Domain.Responses;
using EnterpriseCQRS.Services.CommandHandlers.Utilities;
using EnterpriseCQRS.Tests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EnterpriseCQRS.XUnitTests
{
    public class UnitTest1
    {
        public IMediator _mediator { get; }

        [Fact]
        public async Task IsNotNullResponse_GetTransactions()
        {
            var sqLiteDbFake = new SqLiteDbFake();

            var context = sqLiteDbFake.GetDbContext();

            var url = new Uri("http://quiet-stone-2094.herokuapp.com/transactions.json");

            var response = new GenericResponse<IList<Transaction>>();

            var transactions = new Utilities<Transaction>();

            context.Database.ExecuteSqlRaw("DELETE FROM [Transaction]");

            //trae el listado transactions
            var responses = await transactions.test(url);
            await context.Transaction.AddRangeAsync(responses.Result);
            await context.SaveChangesAsync();

            response.Result = responses.Result;

            var controller = new ProductController(_mediator);

            Assert.NotNull(response.Result);
        }

        [Fact]
        public async Task IsNotNullResponse_GetRates()
        {
            var sqLiteDbFake = new SqLiteDbFake();

            var context = sqLiteDbFake.GetDbContext();

            var url = new Uri("http://quiet-stone-2094.herokuapp.com/rates.json");

            var response = new GenericResponse<IList<Rates>>();

            var rates = new Utilities<Rates>();

            context.Database.ExecuteSqlRaw("DELETE FROM [Transaction]");

            var responses = await rates.test(url);
            await context.Rates.AddRangeAsync(responses.Result);
            await context.SaveChangesAsync();

            response.Result = responses.Result;

            Assert.NotNull(response);
        }

        [Fact]
        public async Task IsTrueTransactions_Transactions()
        {
            var sqLiteDbFake = new SqLiteDbFake();

            var context = sqLiteDbFake.GetDbContext();

            var url = new Uri("http://quiet-stone-2094.herokuapp.com/rates.json");

            var response = new GenericResponse<IList<Rates>>();

            var rates = new Utilities<Rates>();

            context.Database.ExecuteSqlRaw("DELETE FROM [Transaction]");

            var responses = await rates.test(url);
            await context.Rates.AddRangeAsync(responses.Result);
            await context.SaveChangesAsync();

            var rateExists = context.Rates.AnyAsync(x => x.Id == 1);

            Assert.True(await rateExists);
        }
    }
}
