using Domain.Common;
using PhoneNumbers;
using System;

namespace Domain.Entities
{
    public class Customer: AuditableEntity
    {
        public Customer
        (
            string firstname,
            string lastname,
            DateTime dateOfBirth,
            string phoneNumber,
            string email,
            string bankAccountNumber
        )
        {
            Firstname = firstname;
            Lastname = lastname;
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber;
            Email = email;
            BankAccountNumber = bankAccountNumber;

            //Activate();
        }


        //public Customer(long id) => Id = id;


        public long Id { get; set; }

        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        //public ulong PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public string BankAccountNumber { get; private set; }


        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
                //var e164PhoneNumber = "+44 117 496 0123";
                //var nationalPhoneNumber = "2024561111";
                //var smsShortNumber = "83835";
                PhoneNumber phoneNumber = null;

                try
                {
                    phoneNumber = phoneNumberUtil.Parse(value, null);
                    //PhoneNumber swissNumberProto = phoneUtil.parse(swissNumberStr, "CH");
                }
                catch (NumberParseException e)
                {
                    throw new Exception("NumberParseException was thrown: " + e.Message);
                }

                var isValid = phoneNumberUtil.IsValidNumber(phoneNumber);
                


                if (isValid)
                {
                    _phoneNumber = value;
                }
            }
        }


        //public void Inactivate()
        //{
        //    Status = Status.Inactive;
        //}

        //public void Update(string firstName, string lastName, string email)
        //{
        //    Name = new Name(firstName, lastName);
        //    Email = new Email(email);
        //}
    }
}
