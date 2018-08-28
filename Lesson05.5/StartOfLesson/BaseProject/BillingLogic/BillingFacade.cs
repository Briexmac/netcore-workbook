namespace BaseProject.BillingLogic
{
    public class BillingFacade
    {
        public BillingFacade()
        {
            var taxRate = 0.0223m;
            var hourlyRate = 15.50m;
            var billingLib2010 = new BillingCalculatorLib.BillingCalculator2010(hourlyRate);
            var billingLib2013 = new BillingCalculatorLib.BillingCalculator2013(hourlyRate, taxRate);
            var billingLib2017 = new BillingCalculatorLib.BillingCalculator2017(hourlyRate, taxRate);
            var billingLibCurrent = new BillingCalculatorLib.BillingCalculator(hourlyRate, taxRate);
        }
    }
}
