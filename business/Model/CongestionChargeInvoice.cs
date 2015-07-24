namespace CongestionCharge.Business.Model
{

    public interface ICongestionChargeInvoice
    {
         string Value { get; set; }
         string Description { get; set; }
    }

    public class CongestionChargeInvoice : ICongestionChargeInvoice
    {
        public string Value { get; set; }
        public string Description { get; set; }
    }
}
