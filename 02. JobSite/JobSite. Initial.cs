using Moq;
using Shouldly;

namespace JobSite
{
    namespace Initial
    {
        /// <summary>
        /// A physical location where work is done, especially construction work.
        /// </summary>
        /// <translation>
        /// Рабочее место/локация
        /// </translation>
        internal class JobSite
        {
            public JobSite(Location location, Contact? contact = null)
            {
                Location = location;
                Contact = contact;
            }

            public Location Location { get; set; }
            //optional
            public Contact? Contact { get; set; }

            //Violates tell don't ask

            public string ContactName =>
                Contact?.Name ?? "No Name";

            public string ContactPhone =>
                Contact?.Phone ?? "No Phone";

            public void EmailContact(string emailBody) =>
               Contact?.DeliverPersonalizedEmail(emailBody);
        }

        internal class Location { }
        public class Contact
        {

            public Contact(string name, string phone)
            {
                Name = name;
                Phone = phone;
            }

            public string Name { get; set; }
            public string Phone { get; set; }

            public virtual void DeliverPersonalizedEmail(string emailBody)
            {
                //deliver email
            }
        }

        public class JobSiteTests
        {
            public class WhenContactProvided
            {
                [Fact]
                void should_return_contact_phone()
                {
                    var jobSite = new JobSite(new Location(), new Contact("John Smith", "777-5555"));
                    jobSite.ContactPhone.ShouldBe("777-5555");
                }

                [Fact]
                void should_return_contact_name()
                {
                    var jobSite = new JobSite(new Location(), new Contact("John Smith", "777-5555"));
                    jobSite.ContactName.ShouldBe("John Smith");
                }
            }

            public class WhenContactNOtProvided
            {
                [Fact]
                void should_return_contact_phone()
                {
                    var jobSite = new JobSite(new Location());
                    jobSite.ContactPhone.ShouldBe("No Phone");
                }

                [Fact]
                void should_return_contact_name()
                {
                    var jobSite = new JobSite(new Location());
                    jobSite.ContactName.ShouldBe("No Name");
                }
            }

            [Fact]
            void should_call_DeliverPersonalizedEmail()
            {
                var contactMock = new Mock<Contact>("Name", "Phone");
                var jobSite = new JobSite(new Location(), contactMock.Object);
                jobSite.EmailContact("some email text");
                contactMock.Verify(x => x.DeliverPersonalizedEmail("some email text"), Times.Once);
            }
        }
    }
}
