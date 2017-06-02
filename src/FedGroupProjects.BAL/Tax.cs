using FedGroupProjects.DTO.Enums;
using System;
using System.Globalization;
using FedGroupProjects.BAL.Interface;

namespace FedGroupProjects.BAL
{
    public class Tax : ICalculator
    {
        public Tax()
        {

        }

        public Tax(int age, decimal annualSalary)
        {
            ValidateAge(age);
            ValidateAnnualSalary(annualSalary);

            _age = age;
            _annualSalary = annualSalary;
        }

        public Tax(int age, decimal annualSalary, Term term)
        {
            ValidateAge(age);
            ValidateAnnualSalary(annualSalary);

            _age = age;
            _annualSalary = annualSalary;
            _term = term;
        }

        private int _age = 18;
        public int Age
        {
            get
            {
                return _age;
            }

            set
            {
                ValidateAge(value);
                _age = value;
            }
        }

        private decimal _annualSalary = 5000;
        public decimal AnnualSalary
        {
            get
            {
                return _annualSalary;
            }

            set
            {
                ValidateAnnualSalary(value);
                _annualSalary = value;
            }
        }

        private Term _term = Term.Annual;
        public Term Term
        {
            get
            {
                return _term;
            }

            set
            {
                _term = value;
            }
        }

        private decimal MonthlySalary => _annualSalary / (int)Term.Monthly;

        private decimal TaxableSalary
        {
            get
            {
                if (_age > 50)
                {
                    return (_annualSalary - ((int)Relief.AboveAge50 * (int)Term.Monthly)) * ((int)Deduction.AboveAge50 / 100m) / (int)_term;
                }

                return (_annualSalary - ((int)Relief.BelowAge51 * (int)Term.Monthly)) * ((int)Deduction.BelowAge51 / 100m) / (int)_term;
            }
        }

        private decimal TaxPercentage
        {
            get
            {
                if (MonthlySalary > 70000)
                {
                    return 30;
                }

                if (MonthlySalary >= 50000)
                {
                    return 25;
                }

                if (MonthlySalary >= 35000)
                {
                    return 15;
                }

                if (MonthlySalary >= 20000)
                {
                    return 9;
                }

                if (MonthlySalary >= 10000)
                {
                    return 7.5m;
                }

                return 5;
            }
        }

        public decimal Calculate()
        {
            return TaxableSalary * (TaxPercentage / 100);
        }

        private void ValidateAge(int age)
        {
            if (age < 18)
            {
                throw new ArgumentException($"You should be {_age} or older to calculate your tax.");
            }
        }

        private void ValidateAnnualSalary(decimal annualSalary)
        {
            if (annualSalary < 5000)
            {
                var currency = RegionInfo.CurrentRegion.CurrencySymbol;

                throw new ArgumentException($"You should earn {currency} {_annualSalary} or more to calculate your tax.");
            }
        }
    }
}
