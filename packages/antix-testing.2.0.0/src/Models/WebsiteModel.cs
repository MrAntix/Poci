namespace Testing.Models
{
    public class WebsiteModel
    {
        public string Address { get; set; }

        public override string ToString()
        {
            return string.Format("WebsiteModel: {0}", Address).Trim();
        }
    }
}