using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProject.Intrastructure
{
    [HtmlTargetElement("day-of-week", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class DayOfTheWeekTagHelper : TagHelper
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public DayOfTheWeekTagHelper(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "b";
            output.Content.SetContent(_dateTimeProvider.Now.DayOfWeek.ToString());
        }
    }
}
