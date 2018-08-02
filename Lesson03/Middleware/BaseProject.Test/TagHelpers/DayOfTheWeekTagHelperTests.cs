using BaseProject.Intrastructure;
using FluentAssertions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BaseProject.Test.TagHelpers
{
    public class MyMockedDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now { get; set; }
    }
    public class DayOfTheWeekTagHelperTests
    {
        [Fact]
        public void TagHelper_ShouldShowCurrentDayOfTheWeek()
        {
            var now = DateTime.Now;
            foreach (var day in Enumerable.Range(0, 6).Select(i => now.AddDays(i)))
            {
                // Assemble
                var mockDateTimeProvider = new MyMockedDateTimeProvider();
                mockDateTimeProvider.Now = day;
                TagHelper myTagHelper = new DayOfTheWeekTagHelper(mockDateTimeProvider);
                TagHelperContext context = null;
                TagHelperOutput output = new TagHelperOutput(
                    "day-of-week",
                    new TagHelperAttributeList(),
                    (useCachedResult, encoder) =>
                    {
                        var tagHelperContent = new DefaultTagHelperContent();
                        tagHelperContent.SetContent(string.Empty);
                        return Task.FromResult<TagHelperContent>(tagHelperContent);
                    }
                );

                // Act
                myTagHelper.Process(context, output);

                // Assert
                Assert.Contains(mockDateTimeProvider.Now.DayOfWeek.ToString(), output.Content.GetContent());
            }
        }

        [Fact]
        public void TagHelper_ShouldBoldDayOfTheWeek()
        {
            var now = DateTime.Now;
            // Assemble
            var mockDateTimeProvider = new MyMockedDateTimeProvider();
            mockDateTimeProvider.Now = now;
            TagHelper myTagHelper = new DayOfTheWeekTagHelper(mockDateTimeProvider);
            TagHelperContext context = null;
            TagHelperOutput output = new TagHelperOutput(
                "day-of-week",
                new TagHelperAttributeList(),
                (useCachedResult, encoder) =>
                {
                    var tagHelperContent = new DefaultTagHelperContent();
                    tagHelperContent.SetContent(string.Empty);
                    return Task.FromResult<TagHelperContent>(tagHelperContent);
                }
            );

            // Act
            myTagHelper.Process(context, output);

            // Assert
            var content = output.Content.GetContent();
            Assert.Equal("b", output.TagName);
        }
    }
}
