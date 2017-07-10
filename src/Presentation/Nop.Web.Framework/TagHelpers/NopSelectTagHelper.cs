using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Nop.Web.Framework.TagHelpers
{
    [HtmlTargetElement("nop-select", Attributes = ForAttributeName)]
    public class NopSelectTagHelper : SelectTagHelper
    {
        private const string ForAttributeName = "asp-for";

        public NopSelectTagHelper(IHtmlGenerator generator) : base(generator)
        {
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var formControlClass = "form-control";

            //merge classes
            var classValue = output.Attributes.ContainsName("class")
                ? $"{output.Attributes["class"].Value} {formControlClass}"
                : formControlClass;
            output.Attributes.SetAttribute("class", classValue);

            //tag details
            output.TagName = "select";
            output.TagMode = TagMode.StartTagAndEndTag;

            base.Process(context, output);
        }
    }
}