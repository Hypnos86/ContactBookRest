namespace ContactBook.Models
{
    public class PhoneNumber
    {
        public char PrefixNumber { get; set; }
        public int Number { get; set; }
        public string FullNumber { get { return string.Format("{0} {1}", PrefixNumber, Number) ; } }
    }
}
