using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(FirstName, 3, "Name.FirstName", "Must have at least 3 characters")
                .HasMinLen(LastName, 3, "Name.LastName", "Must have at least 3 characters")
                .HasMaxLen(FirstName, 40, "Name.FirstName", "Must have 40 characters or less")
                .HasMaxLen(LastName, 40, "Name.LastName", "Must have 40 characters or less")
            );
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
    }
}