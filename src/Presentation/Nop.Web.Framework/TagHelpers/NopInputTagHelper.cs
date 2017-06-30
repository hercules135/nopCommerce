using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Nop.Web.Framework.TagHelpers
{
    [HtmlTargetElement("nop-input")]
    public class NopInputTagHelper : InputTagHelper
    {
        private const string RequiredAttributeName = "required";

        [HtmlAttributeName(RequiredAttributeName)]
        public string Required { get; set; } = "";

        public NopInputTagHelper(IHtmlGenerator generator) : base(generator)
        {
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var classValue = output.Attributes.ContainsName("class")
                                ? $"{output.Attributes["class"].Value} form-control"
                                : "form-control";
            output.Attributes.SetAttribute("class", classValue);
            output.TagName = "input";
            output.TagMode = TagMode.SelfClosing;

            bool.TryParse(Required, out bool required);
            if (required)
            {
                output.PreElement.SetHtmlContent("<div class='input-group input-group-required'>");
                output.PostElement.SetHtmlContent("<div class=\"input-group-btn\"><span class=\"required\">*</span></div></div>");
            }

            base.Process(context, output);
        }
    }
}