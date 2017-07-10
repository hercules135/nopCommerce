using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Nop.Web.Framework.TagHelpers
{
    [HtmlTargetElement("nop-editor", Attributes = ForAttributeName)]
    public class NopInputTagHelper : InputTagHelper
    {
        private const string ForAttributeName = "asp-for";
        private const string DisabledAttributeName = "asp-disabled";
        private const string RequiredAttributeName = "asp-required";
        private const string TemplateAttributeName = "asp-template";
        private const string ItemsAttributeName = "asp-items";

        private readonly IHtmlHelper _htmlHelper;

        [HtmlAttributeName(DisabledAttributeName)]
        public string IsDisabled { set; get; }

        [HtmlAttributeName(RequiredAttributeName)]
        public string IsRequired { set; get; }

        [HtmlAttributeName(TemplateAttributeName)]
        public string Template { set; get; }

        [HtmlAttributeName(ItemsAttributeName)]
        public IList<SelectListItem> Items { set; get; }

        public NopInputTagHelper(IHtmlGenerator generator, IHtmlHelper htmlHelper) : base(generator)
        {
            _htmlHelper = htmlHelper;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.SuppressOutput();

            //disabled attribute
            bool.TryParse(IsDisabled, out bool disabled);
            if (disabled)
            {
                var d = new TagHelperAttribute("disabled", "disabled");
                output.Attributes.Add(d);
            }

            //merge classes
            //var classValue = output.Attributes.ContainsName("class")
            //    ? $"{output.Attributes["class"].Value} form-control"
            //    : "form-control";
            //output.Attributes.SetAttribute("class", classValue);

            //required asterisk
            bool.TryParse(IsRequired, out bool required);
            if (required)
            {
                output.PreElement.SetHtmlContent("<div class='input-group input-group-required'>");
                output.PostElement.SetHtmlContent("<div class=\"input-group-btn\"><span class=\"required\">*</span></div></div>");
            }

            //generate editor
            var viewContextAware
                = _htmlHelper as IViewContextAware;
            viewContextAware?.Contextualize(ViewContext);

            IHtmlContent s = _htmlHelper.Editor(For.Name, Template, Items != null && Items.Any() ? new { SelectList = Items } : null);
            string htmlOutput;
            using (var writer = new StringWriter())
            {
                s.WriteTo(writer, HtmlEncoder.Default);
                htmlOutput = writer.ToString();
            }
            output.Content.SetHtmlContent(htmlOutput);
        }
    }
}