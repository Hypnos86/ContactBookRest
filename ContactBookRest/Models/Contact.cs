using System;
using System.Collections.Generic;

namespace ContactBookRest.Models
{
    public class Contact
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        //public List<StringPhoneNumber> Numbers { get; set; }
        public string Numbers { get; set; }

    }

    public class StringPhoneNumber
    {
        public string Number { get; set; }
    }

}
