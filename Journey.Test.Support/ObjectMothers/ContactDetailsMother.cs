using System;
using Journey.Test.Support.Model;
using Kent.Boogaart.KBCsv;

namespace Journey.Test.Support.ObjectMothers
{
    public class ContactDetailsMother
    {
        public string Email { get; set; }
        public string TelephoneNumber { get; set; }
        public bool EmailOptIn { get; set; }
        public bool TelephoneOptIn { get; set; }
        public bool SMSOptIn { get; set; }
        public bool PostOptIn { get; set; }

        public ContactDetailsMother()
        {

        }
        public ContactDetails Build()
        {
            Email = "anything@emailreaction.org";
            TelephoneNumber = "01010101010";
            EmailOptIn = false;
            TelephoneOptIn = false;
            SMSOptIn = false;
            PostOptIn = false;
            return new ContactDetails()
                       {
                           Email = Email,
                           TelephoneNumber = TelephoneNumber,
                           EmailOptIn = EmailOptIn,
                           TelephoneOptIn = TelephoneOptIn,
                           SMSOptIn = SMSOptIn,
                           PostOptIn = PostOptIn,
                       };

        }

        public ContactDetails BuildFromCSV(DataRecord data)
        {
            Email = data["EMAIL"];
            TelephoneNumber = data["TELEPHONENO"];
            EmailOptIn = Convert.ToBoolean(data["EMAILOPTIN"]);
            TelephoneOptIn = Convert.ToBoolean(data["TELOPTIN"]);
            SMSOptIn = Convert.ToBoolean(data["SMSOPTIN"]);
            PostOptIn = Convert.ToBoolean(data["POSTOPTIN"]);
            return new ContactDetails()
                       {
                           Email = Email,
                           TelephoneNumber = TelephoneNumber,
                           EmailOptIn = EmailOptIn,
                           TelephoneOptIn = TelephoneOptIn,
                           SMSOptIn = SMSOptIn,
                           PostOptIn = PostOptIn,
                       };

        }
    }
}