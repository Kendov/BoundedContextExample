using System;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;
using Xunit;

namespace PaymentContext.Tests.Handlers
{
    public class SubscriptionHandlerTests
    {
        [Fact]
        public void ShouldReturnErrorWhenDocumentExists()
        {
        //Given
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();

            command.FirstName = "zurik";
            command.LastName = "pavlov";
            command.Document = "12345678912";
            command.Email = "email@email.com";
            command.BarCode = "123456879";
            command.BoletoNumber = "1235456789";
            command.PaymentNumber = "123456789";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddDays(2);
            command.Total = 20;
            command.TotalPaid = 20;
            command.Payer = "zurikCorp";
            command.PayerDocument = "123456789";
            command.PayerDocumentType = Domain.Enums.EDocumentType.CPF;
            command.Street = "street";
            command.Number = "number";
            command.Neighborhood = "neigborhood";
            command.City = "city";
            command.State = "state";
            command.County = "county";
            command.ZipCode = "20022100";
            command.PayerEmail = "email@email.com";
        //When
            handler.Handle(command);
        //Then
            Assert.True(handler.Invalid);
        }

        [Fact]
        public void ShouldReturnSucessAddingValidDocument()
        {
        //Given
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();

            command.FirstName = "zurik";
            command.LastName = "pavlov";
            command.Document = "21987654321";
            command.Email = "email2@email.com";
            command.BarCode = "123456879";
            command.BoletoNumber = "1235456789";
            command.PaymentNumber = "123456789";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddDays(2);
            command.Total = 20;
            command.TotalPaid = 20;
            command.Payer = "zurikCorp";
            command.PayerDocument = "123456789";
            command.PayerDocumentType = Domain.Enums.EDocumentType.CPF;
            command.Street = "street";
            command.Number = "number";
            command.Neighborhood = "neigborhood";
            command.City = "city";
            command.State = "state";
            command.County = "county";
            command.ZipCode = "20022100";
            command.PayerEmail = "email2@email.com";
        //When
            handler.Handle(command);
        //Then
            Assert.True(handler.Valid);
        }
    }
}