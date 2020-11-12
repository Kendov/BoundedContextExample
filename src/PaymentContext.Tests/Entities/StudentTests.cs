using System;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.ValueObjects;
using Xunit;

namespace PaymentContext.Tests.Entities
{
    public class StudentTests
    {
        private readonly Student _student;
        private readonly Subscription _subscription;
        private readonly Email _email;
        private readonly Document _document;
        private readonly Name _name;
        private readonly Address _address;

        public StudentTests()
        {
            _name = new Name("zurik", "dipnov");
            _document = new Document("22211144400", Domain.Enums.EDocumentType.CPF);
            _email = new Email("zurik@email.com");
            _address = new Address("street1", "022", "neig1", "city1", "state1", "country1", "0000001");

            _student = new Student(_name, _document, _email, _address);
            
            _subscription = new Subscription();
        }

        [Fact]
        public void ShoudReturnErrorWhenActiveSubscription()
        {
            var payment = new PayPalPayment(
                "12345678",
                DateTime.Now,
                DateTime.Now.AddDays(2),
                10,
                10,
                _document,
                "zurikCorp",
                _address,
                _email
                );

            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);

            Assert.False(_student.Invalid);
            
        }

        public void ShoudReturnErrorWhenSubscriptionHasNoPayment()
        {
            _student.AddSubscription(_subscription);

            Assert.False(_student.Invalid);

            
        }

        [Fact]
        public void ShoudReturnSuccessWhenNoActiveSubscription()
        {
            var payment = new PayPalPayment(
                "12345678",
                DateTime.Now,
                DateTime.Now.AddDays(2),
                10,
                10,
                _document,
                "zurikCorp",
                _address,
                _email
            );
            
            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);

            Assert.True(_student.Valid);
            
        }
    }
}