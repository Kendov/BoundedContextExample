using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories;

namespace PaymentContext.Tests.Mocks
{
    public class FakeStudentRepository : IStudentRepository
    {
        public void CreateSubscription(Student student)
        {
            
        }

        public bool DocumentExists(string document)
        {
            if(document == "12345678912")
                return true;
            return false;
        }

        public bool EmailExists(string email)
        {
            if(email == "email@email.com")
                return true;
            return false;
        }
    }
}