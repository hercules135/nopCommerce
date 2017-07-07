using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Nop.Web.Framework.TagHelpers
{
    [HtmlTargetElement("input", Attributes = ForAttributeName)]
    public class NopInputTagHelper : InputTagHelper
    {
        private const string ForAttributeName = "asp-for";
        private const string DisabledAttributeName = "asp-disabled";
        private const string RequiredAttributeName = "asp-required";

        [HtmlAttributeName(DisabledAttributeName)]
        public string IsDisabled { set; get; }

        [HtmlAttributeName(RequiredAttributeName)]
        public string IsRequired { set; get; }

        public NopInputTagHelper(IHtmlGenerator generator) : base(generator)
        {

        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //disabled attribute
            bool.TryParse(IsDisabled, out bool disabled);
            if (disabled)
            {
                var d = new TagHelperAttribute("disabled", "disabled");
                output.Attributes.Add(d);
            }

            //merge classes
            var classValue = output.Attributes.ContainsName("class")
                ? $"{output.Attributes["class"].Value} form-control"
                : "form-control";
            output.Attributes.SetAttribute("class", classValue);

            //required asterisk
            bool.TryParse(IsRequired, out bool required);
            if (required)
            {
                output.PreElement.SetHtmlContent("<div class='input-group input-group-required'>");
                output.PostElement.SetHtmlContent("<div class=\"input-group-btn\"><span class=\"required\">*</span></div></div>");
            }

            //editor template
            var modelTypeName = For.Metadata.ModelType.Name;

            base.Process(context, output);
        }
    }
}