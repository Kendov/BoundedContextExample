using System;

namespace PaymentContext.Shared.Entities
{
    public class Entity {

        public Entity(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; private set; }
    }
}