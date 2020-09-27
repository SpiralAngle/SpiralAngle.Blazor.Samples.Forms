using System;
using Models = BlazorFormSample.Shared.GameModels;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BlazorFormSample.Shared.Tests.Utility;
using System.Linq;

namespace BlazorFormSample.Shared.Tests.GameModels
{
    public class AttributeTests
    {
        [Fact]
        public void ModifiedMaximum_NotValid_If_Less_Than_Maximim()
        {
            Models.Attribute attribute = new Models.Attribute
            {
                Name = "Bob",
                Maximum = 2,
                ModifiedMaximum = 1
            };

            IList<System.ComponentModel.DataAnnotations.ValidationResult> result = attribute.ValidateModel();

            Assert.Collection(result, x => Assert.Equal("Modified Maximum must be greater than or equal to Maximum", x.ErrorMessage));
        }

        [Fact]
        public void ModifiedMinimum_NotValid_If_Greater_Than_Minimim()
        {
            Models.Attribute attribute = new Models.Attribute
            {
                Name = "Bob",
                Minimum = 1,
                ModifiedMinimum = 2,
                Maximum = 4,
                ModifiedMaximum = 5
            };

            IList<System.ComponentModel.DataAnnotations.ValidationResult> result = attribute.ValidateModel();

            Assert.Collection(result, x => Assert.Equal("Modified Minimum must be less than or equal to Minimum", x.ErrorMessage));
        }

        [Fact]
        public void Minimum_NotValid_If_Greater_Than_Maximim()
        {
            Models.Attribute attribute = new Models.Attribute
            {
                Name = "Bob",
                Minimum = 1,
                ModifiedMinimum = 4,
                Maximum = 2,
                ModifiedMaximum = 5
            };

            IList<System.ComponentModel.DataAnnotations.ValidationResult> result = attribute.ValidateModel();

            Assert.Collection(result, x => Assert.Collection(x.MemberNames,
                firstMemberName => Assert.Equal("ModifiedMinimum", firstMemberName),
                secondMemberName => Assert.Equal("Minimum", secondMemberName)
                ));
        }

        [Fact]
        public void Name_Is_Required()
        {
            Models.Attribute attribute = new Models.Attribute
            {
            };

            IList<System.ComponentModel.DataAnnotations.ValidationResult> result = attribute.ValidateModel();

            Assert.Collection(result, x => Assert.Contains("Name", x.ErrorMessage));
        }

        [Fact]
        public void ValidModel_Passes()
        {
            Models.Attribute attribute = new Models.Attribute
            {
                Name = "Bob",
                ModifiedMinimum = 2,
                Minimum = 2,
                Maximum = 3,
                ModifiedMaximum = 3
            };

            IList<System.ComponentModel.DataAnnotations.ValidationResult> result = attribute.ValidateModel();

            Assert.Empty(result);
        }
    }
}
