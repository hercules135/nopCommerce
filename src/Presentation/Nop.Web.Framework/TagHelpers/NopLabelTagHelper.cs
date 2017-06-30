using System;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Nop.Core;
using Nop.Core.Infrastructure;
using Nop.Services.Localization;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Web.Framework.TagHelpers
{
    [HtmlTargetElement("nop-label", Attributes = ForAttributeName)]
    public class NopLabelTagHelper : TagHelper
    {
        private const string ForAttributeName = "for";
        private const string DisplayHintAttributeName = "display-hint";

        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression For { get; set; }

        [HtmlAttributeName(DisplayHintAttributeName)]
        public bool DisplayHint { get; set; } = true;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var classValue = output.Attributes.ContainsName("class")
                                ? $"{output.Attributes["class"].Value} label-wrapper"
                                : "label-wrapper";

            output.TagName = "div";
            output.Attributes.SetAttribute("class", classValue);
            output.TagMode = TagMode.StartTagAndEndTag;

            if (For != null)
            {
                var metadata = For.Metadata;

                var labelValue = metadata.DisplayName ?? metadata.PropertyName ?? "";
                var labelContent = $"<label for='{For.Name}' class='control-label'>{labelValue}</label>";
                output.Content.AppendHtml(labelContent);

                object value;
                if (metadata.AdditionalValues.TryGetValue("NopResourceDisplayNameAttribute", out value))
                {
                    var resourceDisplayName = value as NopResourceDisplayNameAttribute;
                    if (resourceDisplayName != null && DisplayHint)
                    {
                        var langId = EngineContext.Current.Resolve<IWorkContext>().WorkingLanguage.Id;
                        var hintResource = EngineContext.Current.Resolve<ILocalizationService>()
                            .GetResource(resourceDisplayName.ResourceKey + ".Hint", langId, returnEmptyIfNotFound: true,
                                logIfNotFound: false);

                        if (!String.IsNullOrEmpty(hintResource))
                        {
                            var hintContent = $"<div title='{hintResource}' class='ico-help'><i class='fa fa-question-circle'></i></div>";
                            output.Content.AppendHtml(hintContent);
                        }
                    }
                }
            }
        }
    }
}