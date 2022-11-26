using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Initial
{
	class User
	{
		public string Email { get; set; }
		public void ChargeForSubscription()
		{
			var braintreeId = new BraintreeGem().Find(Email).BraintreeId;
			new BraintreeGem().Charge(braintreeId, 10m);
		}

		public User CreateAsCustomer()
		{
			return new BraintreeGem().CreateCustomer(Email);
		}
	}

	class Refund
    {
		public void Process()
        {
			var transactionId = new BraintreeGem().FindTransaction("order id");
			new BraintreeGem().Refund(transactionId, 10m);

		}
    }


	//Nuget is much better then calling API directly
	//If if wanna change which payment provider nuget we use, we still gonna open up User (shot gun surgery)
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
