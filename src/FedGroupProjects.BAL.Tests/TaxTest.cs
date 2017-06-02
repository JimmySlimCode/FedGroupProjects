using FedGroupProjects.DTO.Enums;
using NUnit.Framework;

namespace FedGroupProjects.BAL.Tests
{
    [TestFixture]
    public class TaxTest
    {
        private Tax _tax;

        [SetUp]
        public void SetupTax()
        {
            const int age = 26;
            const decimal annualSalary = 540000;

            _tax = new Tax(age, annualSalary);
        }

        [Test]
        public void GetDefaultAge()
        {
            var result = new Tax().Age;

            Assert.AreEqual(18, result);
        }

        [Test]
        public void GetDefaultAnnualSalary()
        {
            var result = new Tax().AnnualSalary;

            Assert.AreEqual(5000, result);
        }

        [Test]
        public void GetDefaultTerm()
        {
            var result = new Tax().Term;

            Assert.AreEqual(Term.Annual, result);
        }

        [Test]
        public void CalculateDefaultTax()
        {
            var result =  new Tax().Calculate();

            Assert.AreEqual(-950m, result);
        }

        [Test]
        public void CalculateAnnualTax()
        {
            _tax.Term = Term.Annual;

            var result = _tax.Calculate();

            Assert.AreEqual(77400m, result);
        }

        [Test]
        public void CalculateMonthlyTax()
        {
            _tax.Term = Term.Monthly;

            var result = _tax.Calculate();

            Assert.AreEqual(6450m, result);
        }
    }
}
