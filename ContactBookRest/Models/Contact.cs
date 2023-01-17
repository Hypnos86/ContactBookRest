using System;
using System.Collections.Generic;

namespace ContactBook.Models
{
    public class Contact
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<PhoneNumber> Numbers { get; set; }

    }
}
