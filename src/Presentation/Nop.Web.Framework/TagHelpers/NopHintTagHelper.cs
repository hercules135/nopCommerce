using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Nop.Web.Framework.TagHelpers
{
    [HtmlTargetElement("div", Attributes = NopValueAttributeName)]
    public class NopHintTagHelper : TagHelper
    {
        private const string NopValueAttributeName = "nop-value";

        [HtmlAttributeName(NopValueAttributeName)]
        public string Value { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("title", Value);
            output.Attributes.SetAttribute("class", "ico-help");
            output.Content.SetHtmlContent("<i class='fa fa-question-circle'></i>");
        }
    }
}