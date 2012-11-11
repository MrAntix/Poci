namespace Testing.Models
{
    public class EmailModel
    {
        public string Type { get; set; }
        public string Address { get; set; }

        public override string ToString()
        {
            return string.Format("EmailModel: {0} ({1})", Address, Type).Trim();
        }
    }
}