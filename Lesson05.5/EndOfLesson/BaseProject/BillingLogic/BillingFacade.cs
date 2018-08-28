using System;

namespace BaseProject.BillingLogic
{
    public class BillingFacade
    {
        private readonly BillingFactory _billingFactory;

        public BillingFacade(BillingFactory billingFactory)
        {
            _billingFactory = billingFactory;
        }

        public decimal CalculateCost(DateTime start, DateTime end, bool isExpat)
        {
            ICalculator calc = _billingFactory.GetCalculator(end.Year);
            var hoursWorked = calc.CalculateBillableHours(start, end);
            return calc.CalculateBillableCost(hoursWorked, isExpat);
        }
    }
}
