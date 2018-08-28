namespace BaseProject.BillingLogic
{
    public class BillingFactory
    {
        private readonly decimal _taxRate;
        private readonly decimal _hourlyRate;

        public BillingFactory()
        {
            _taxRate = 0.0223m;
            _hourlyRate = 15.50m;
        }

        public ICalculator GetCalculator(int year)
        {
            if (year < 2010)
            {
                return new BillingAdaptor2010(_hourlyRate);
            }
            else if (year < 2013)
            {
                return new BillingAdaptor2013(_hourlyRate, _taxRate);
            }
            else if (year < 2017)
            {
                return new BillingAdaptor2017(_hourlyRate, _taxRate);
            }
            return new BillingAdaptor(_hourlyRate, _taxRate);
        }
    }
}
