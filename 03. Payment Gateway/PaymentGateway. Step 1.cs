using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Step1
{
    class User
    {

        public string Email { get; set; }

    }

    class Refund
    {
        public void Process()
        {
            var transactionId = new BraintreeGem().FindTransaction("order id");
            new BraintreeGem().Refund(transactionId, 10m);

        }
    }



    //Depend upon abstraction

    //Provider pattern

    //Change in one place if we wanna change payment provider

    //I controll that API contract

    class PaymentGateway
    {
        BraintreeGem Braintree { get; };

        public PaymentGateway(BraintreeGem braintree)
        {
            Braintree = braintree;
        }

        public void ChargeForSubscription(Step1.User user)
        {
            var braintreeId = Braintree.Find(user.Email).BraintreeId;
            Braintree.Charge(braintreeId, 10);
        }

        public User CreateAsCustomer(Step1.User user)
        {
            return Braintree.CreateCustomer(user.Email);
        }

        public User Find(Step1.User user)
        {          
            return Braintree.Find(user.Email);
        }

        public User CreateCustomer(Step1.User user)
        {
            return Braintree.CreateCustomer(user.Email);
        }

        public string FindTransaction(string braintreeId)
        {
            return Braintree.FindTransaction(braintreeId)s;
        }

        public void Refund(string orderId, decimal amount)
        {
            var transactionId = Braintree.FindTransaction(orderId);
            Braintree.Refund(transactionId, 10m);
        }
    }

    class BraintreeGem
    {
        public User Find(string email)
        {
            return new User();
        }

        public User CreateCustomer(string email)
        {
            return new User();
        }

        public string FindTransaction(string braintreeId)
        {
            return string.Empty;
        }

        public void Refund(string transactionId, decimal amount)
        {
            return string.Empty;
        }

        public void Charge(string braintreeId, decimal amount)
        {
            return string.Empty;
        }

        public class User
        {
            public string BraintreeId { get; set; }
        }

    }
}
