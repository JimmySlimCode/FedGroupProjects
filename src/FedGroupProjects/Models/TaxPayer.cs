using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using FedGroupProjects.BAL;
using FedGroupProjects.DTO.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FedGroupProjects.Models
{
    public class TaxPayer
    {
        public TaxPayer()
        {
            _options = new List<Option>
            {
                new Option
                {
                    Id = (int)Term.Annual,
                    Value = Term.Annual.ToString()
                },
                new Option
                {
                    Id = (int)Term.Monthly,
                    Value = Term.Monthly.ToString()
                }
            };
        }

        [Range(18, 100), Display(Name = "Age")]
        public int Age { get; set; }

        [Range(5000, 100000000), Display(Name = "Annual Salary")]
        public decimal AnnualSalary { get; set; }

        [Required, Display(Name = "Term")]
        public Term Term { get; set; }

        private readonly List<Option> _options;

        public IEnumerable<SelectListItem> TermOptions => new SelectList(_options, "Id", "Value");

        public void CalculateTax()
        {
            var tax = new Tax(Age, AnnualSalary, Term);

            Tax = tax.Calculate().ToString("C", new CultureInfo("en-ZA"));
        }

        [Required, Display(Name = "Tax")]
        public string Tax { get; set; }
    }

    public class Option
    {
        public int Id { get; set; }

        public string Value { get; set; }
    }
}
