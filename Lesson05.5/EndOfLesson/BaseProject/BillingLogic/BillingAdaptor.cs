using System;
using BillingCalculatorLib;

namespace BaseProject.BillingLogic
{
    public interface ICalculator
    {
        TimeSpan CalculateBillableHours(DateTime start, DateTime end);

        decimal CalculateBillableCost(TimeSpan hoursWorked, bool isExpat);
    }


    public class BillingAdaptor2010 : ICalculator
    {
        private readonly BillingCalculator2010 _billingLib;

        public BillingAdaptor2010(decimal hourlyRate)
        {
            _billingLib = new BillingCalculatorLib.BillingCalculator2010(hourlyRate);
        }

        public TimeSpan CalculateBillableHours(DateTime start, DateTime end)
        {
            return _billingLib.CalculateBillableHours(start, end);
        }

        public decimal CalculateBillableCost(TimeSpan hoursWorked, bool isExpat)
        {
            return _billingLib.CalculateBillableCost(hoursWorked);
        }
    }

    public class BillingAdaptor2013 : ICalculator
    {
        private readonly BillingCalculator2013 _billingLib;

        public BillingAdaptor2013(decimal hourlyRate, decimal taxRate)
        {
            _billingLib = new BillingCalculatorLib.BillingCalculator2013(hourlyRate, taxRate);
        }

        public TimeSpan CalculateBillableHours(DateTime start, DateTime end)
        {
            return _billingLib.CalculateBillableHours(start, end);
        }

        public decimal CalculateBillableCost(TimeSpan hoursWorked, bool isExpat)
        {

            return _billingLib.CalculateBillableCost(hoursWorked);
        }
    }

    public class BillingAdaptor2017 : ICalculator
    {
        private readonly BillingCalculator2017 _billingLib;

        public BillingAdaptor2017(decimal hourlyRate, decimal taxRate)
        {
            _billingLib = new BillingCalculatorLib.BillingCalculator2017(hourlyRate, taxRate);
        }

        public TimeSpan CalculateBillableHours(DateTime start, DateTime end)
        {
            return _billingLib.CalculateBillableHours(start, end);
        }

        public decimal CalculateBillableCost(TimeSpan hoursWorked, bool isExpat)
        {
            return _billingLib.CalculateEmployeeBillableCost(hoursWorked);
        }
    }

    public class BillingAdaptor : ICalculator
    {
        private readonly BillingCalculator _billingLib;

        public BillingAdaptor(decimal hourlyRate, decimal taxRate)
        {
            _billingLib = new BillingCalculatorLib.BillingCalculator(hourlyRate, taxRate);
        }

        public TimeSpan CalculateBillableHours(DateTime start, DateTime end)
        {
            return _billingLib.CalculateBillableHours(start, end);
        }

        public decimal CalculateBillableCost(TimeSpan hoursWorked, bool isExpat)
        {
            return _billingLib.CalculateEmployeeBillableCost(hoursWorked, isExpat);
        }
    }
}
