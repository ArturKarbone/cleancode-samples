using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            public JobSite(Location location)
            {
                Location = location;
            }

            public Location Location { get; set; }
            //optional
            public Contact? Contact { get; set; }

            //Violates tell don't ask

            public string ContactName =>
                Contact?.Name ?? "No Name";

            public string ContactPhone =>
                Contact?.Phone ?? "No Phone";
        }

        internal class Location { }
        internal class Contact
        {

            public Contact(string name, string phone)
            {
                Name = name;
                Phone = phone;
            }

            public string Name { get; set; }
            public string Phone { get; set; }
        }
    }

    namespace Step2
    {
        internal class JobSite
        {
            public JobSite(Location location, IContact? contact = null)
            {
                Location = location;
                Contact = contact ?? new NullContact();
            }

            public Location Location { get; set; }
            //no more optional (rather replaced with Null Object now)
            public IContact Contact { get; set; }

            //more obvious and direct
            //no more tell don't ask violation
            //but have to keep two APIs in sync, but it worht it
            //Refactoring: replace conditional with polymorphysm (just send messages instead of ifs)
            public string ContactName =>
                Contact.Name;

            public string ContactPhone =>
                Contact.Phone;

            public void EmailContact(string emailBody) =>
                Contact.DeliverPersonalizedEmail(emailBody);
        }

        internal class Location { }

        public interface IContact
        {
            string Name { get; }
            string Phone { get; }
            void DeliverPersonalizedEmail(string emailBody);
        }

        internal class Contact : IContact
        {
            public Contact(string name, string phone)
            {
                Name = name;
                Phone = phone;
            }

            public string Name { get; set; }
            public string Phone { get; set; }

            public void DeliverPersonalizedEmail(string emailBody)
            {
                //deliver email
            }
        }

        internal class NullContact : IContact
        {

            public string Name => "No Name";

            public string Phone => "No Phone";

            public void DeliverPersonalizedEmail(string emailBody)
            {

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
                var contactMock = new Mock<IContact>();
                var jobSite = new JobSite(new Location(), contactMock.Object);
                jobSite.EmailContact("some email text");
                contactMock.Verify(x => x.DeliverPersonalizedEmail("some email text"), Times.Once);
            }
        }
    }
}
