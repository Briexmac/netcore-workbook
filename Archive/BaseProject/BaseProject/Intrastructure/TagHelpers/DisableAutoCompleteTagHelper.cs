using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProject.Intrastructure.TagHelpers
{
    [HtmlTargetElement("input", Attributes = AutocompleteOff, TagStructure = TagStructure.NormalOrSelfClosing)]
    public class DisableAutoCompleteTagHelper : TagHelper
    {
        private const string AutocompleteOff = "autocomplete-off";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Remove(context.AllAttributes.First(a => a.Name == AutocompleteOff));
            output.Attributes.Add(new TagHelperAttribute("autocomplete", "off"));
        }
    }
}
