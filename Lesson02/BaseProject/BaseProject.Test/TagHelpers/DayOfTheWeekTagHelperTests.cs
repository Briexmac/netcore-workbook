using BaseProject.Intrastructure;
using FluentAssertions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BaseProject.Test.TagHelpers
{
    public class DayOfTheWeekTagHelperTests
    {
        [Fact]
        public void TagHelper_ShouldShowCurrentDayOfTheWeek()
        {
            // Assemble
            TagHelper myTagHelper = new DayOfTheWeekTagHelper();
            TagHelperContext context = null;
            TagHelperOutput output = null;

            // Act
            myTagHelper.Process(context, output);

            // Assert

        }
    }
}
