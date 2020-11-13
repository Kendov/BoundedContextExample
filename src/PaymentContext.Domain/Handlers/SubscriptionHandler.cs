using System;
using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : 
        Notifiable, 
        IHandler<CreateBoletoSubscriptionCommand>,
        IHandler<CreatePayPalSubscriptionCommand>
    {

        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository repository, IEmailService service)
        {
            _repository = repository;
            _emailService = service;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            command.Validate();
            if(command.Invalid) return new CommandResult(false, "Unable to finish the subscription");

            // Check if document exists
            if(_repository.DocumentExists(command.Document))
                AddNotification("Document", "Document already exists");
            
            // Check if email exists
            if(_repository.EmailExists(command.Email))
                AddNotification("Email", "Email already exists");

            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, Domain.Enums.EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(
                command.Street,
                command.Number,
                command.Neighborhood,
                command.City,
                command.State,
                command.County, 
                command.ZipCode
            );

            var student = new Student(name, document, email, address);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(
                command.BarCode,
                command.BoletoNumber,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                new Document(command.PayerDocument, command.PayerDocumentType),
                command.Payer,
                address,
                email
            );

            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // validate
            AddNotifications(name, document, email, address, student, subscription, payment);
            if(Invalid)
                return new CommandResult(false, "Unable to finish the subscription");

            _repository.CreateSubscription(student);

            _emailService.Send(student.Name.ToString(), student.Email.Address, "Welcome", "Your subscription was created");

            return new CommandResult(true, "Successful Subscription");
        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {

            // Check if document exists
            if(_repository.DocumentExists(command.Document))
                AddNotification("Document", "Document already exists");
            
            // Check if email exists
            if(_repository.EmailExists(command.Email))
                AddNotification("Email", "Email already exists");

            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, Domain.Enums.EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(
                command.Street,
                command.Number,
                command.Neighborhood,
                command.City,
                command.State,
                command.County, 
                command.ZipCode
            );

            var student = new Student(name, document, email, address);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PayPalPayment(
                command.TransactionCode,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                new Document(command.PayerDocument, command.PayerDocumentType),
                command.Payer,
                address,
                email
            );

            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            AddNotifications(name, document, email, address, student, subscription, payment);
            
            if(Invalid)
                return new CommandResult(false, "Unable to finish the subscription");

            _repository.CreateSubscription(student);

            _emailService.Send(student.Name.ToString(), student.Email.Address, "Welcome", "Your subscription was created");

            return new CommandResult(true, "Successful Subscription");
        }
    }
}