using System;

namespace BillingCalculatorLib
{
    public class BillingCalculator2010
    {
        private readonly decimal _hourlyRate;

        public BillingCalculator2010(decimal hourlyRate)
        {
            _hourlyRate = hourlyRate;
        }

        public TimeSpan CalculateBillableHours(DateTime start, DateTime end)
        {
            return end - start;
        }
        public decimal CalculateBillableCost(TimeSpan hoursWorked)
        {
            return hoursWorked.Hours * _hourlyRate + hoursWorked.Minutes / 60 * _hourlyRate;
        }
    }

    public class BillingCalculator2013
    {
        private readonly decimal _hourlyRate;
        private readonly decimal _taxRate;

        public BillingCalculator2013(decimal hourlyRate, decimal taxRate)
        {
            _hourlyRate = hourlyRate;
            _taxRate = taxRate;
        }

        public TimeSpan CalculateBillableHours(DateTime start, DateTime end)
        {
            return end - start;
        }
        public decimal CalculateBillableCost(TimeSpan hoursWorked)
        {
            var cost = hoursWorked.Hours * _hourlyRate + hoursWorked.Minutes / 60 * _hourlyRate;
            return cost + (cost * _taxRate);
        }
    }

    public class BillingCalculator2017
    {
        private readonly decimal _hourlyRate;
        private readonly decimal _taxRate;

        public BillingCalculator2017(decimal hourlyRate, decimal taxRate)
        {
            _hourlyRate = hourlyRate;
            _taxRate = taxRate;
        }

        public TimeSpan CalculateBillableHours(DateTime start, DateTime end)
        {
            return end - start;
        }
        public decimal CalculateContractorBillableCost(TimeSpan hoursWorked)
        {
            var cost = hoursWorked.Hours * _hourlyRate + hoursWorked.Minutes / 60 * _hourlyRate;
            return cost + (cost * _taxRate);
        }
        public decimal CalculateEmployeeBillableCost(TimeSpan hoursWorked)
        {
            var cost = hoursWorked.Hours * _hourlyRate + hoursWorked.Minutes / 60 * _hourlyRate;
            return cost + (cost * _taxRate) + (0.0825m * cost);
        }
    }

    public class BillingCalculator
    {
        private readonly decimal _hourlyRate;
        private readonly decimal _taxRate;

        public BillingCalculator(decimal hourlyRate, decimal taxRate)
        {
            _hourlyRate = hourlyRate;
            _taxRate = taxRate;
        }

        public TimeSpan CalculateBillableHours(DateTime start, DateTime end)
        {
            return end - start;
        }

        public decimal CalculateContractorBillableCost(TimeSpan hoursWorked)
        {
            var cost = hoursWorked.Hours * _hourlyRate + hoursWorked.Minutes / 60 * _hourlyRate;
            return cost + (cost * _taxRate);
        }

        public decimal CalculateEmployeeBillableCost(TimeSpan hoursWorked, bool isExpat)
        {
            var cost = hoursWorked.Hours * _hourlyRate + hoursWorked.Minutes / 60 * _hourlyRate;
            if (isExpat)
                return cost + (cost * _taxRate);
            return cost + (cost * _taxRate) + (0.0825m * cost);
        }
    }

}
