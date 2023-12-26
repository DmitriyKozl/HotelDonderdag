using Hotel.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hotel.BL.Model
{
    public class ContactInfo
    {
        private string _email;
        private string _phone;
        private Address _address;

        public ContactInfo(string email, string phone, Address address)
        {
            Email = email;   // Use the property, not the field
            Phone = phone;   // Use the property, not the field
            Address = address; // Use the property, not the field
        }

        public string Email {
            get { return _email; }
            set {
                if (string.IsNullOrWhiteSpace(value) || !IsValidEmail(value))
                    throw new CustomerException("Invalid email format.");
                _email = value;
            }
        }       
        public string Phone {
            get { return _phone; }
            set { if (string.IsNullOrWhiteSpace(value)) throw new CustomerException("Customer"); _phone = value; } 
        }
        
        public Address Address {
            get { return _address; }
            set { if (value==null) throw new CustomerException("cin"); _address = value; }
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
 
