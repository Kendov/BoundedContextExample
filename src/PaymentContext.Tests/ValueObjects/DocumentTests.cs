using System.Reflection.Metadata;
using PaymentContext.Domain.Enums;
using Xunit;

namespace PaymentContext.Tests.ValueObjects
{
    public class DocumentTests
    {
        [Fact]
        public void ShouldReturnErrorWhenCNPJIsInvalid()
        {
            var doc = new PaymentContext.Domain.ValueObjects.Document("123", EDocumentType.CNPJ);
            Assert.True(doc.Invalid);
            
        }

        [Fact]
        public void ShouldReturnSuccessWhenCNPJIsValid()
        {
            var doc = new PaymentContext.Domain.ValueObjects.Document("15383810000141", EDocumentType.CNPJ);
            Assert.True(doc.Valid);
        }

        [Fact]
        public void ShouldReturnErrorWhenCPFIsInvalid()
        {
            var doc = new PaymentContext.Domain.ValueObjects.Document("123", EDocumentType.CPF);
            Assert.True(doc.Invalid);
        }

        [Fact]
        public void ShouldReturnSuccessWhenCPFIsValid()
        {
            var doc = new PaymentContext.Domain.ValueObjects.Document("02222055400", EDocumentType.CPF);
            Assert.True(doc.Valid);
        }
    }
}