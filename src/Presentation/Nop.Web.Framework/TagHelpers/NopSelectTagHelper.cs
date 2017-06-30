using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Nop.Web.Framework.TagHelpers
{
    [HtmlTargetElement("nop-select")]
    public class NopSelectTagHelper : SelectTagHelper
    {
        public NopSelectTagHelper(IHtmlGenerator generator) : base(generator)
        {
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var classValue = output.Attributes.ContainsName("class")
                                ? $"{output.Attributes["class"].Value} form-control"
                                : "form-control";
            output.Attributes.SetAttribute("class", classValue);
            output.TagName = "select";
            output.TagMode = TagMode.StartTagAndEndTag;

            base.Process(context, output);
        }
    }
}