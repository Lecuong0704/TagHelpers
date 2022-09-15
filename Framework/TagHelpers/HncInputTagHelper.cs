namespace CustomTagHelpers.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    [HtmlTargetElement("hnc-input")]
    public class HncInputTagHelper : TagHelper
    {
        private readonly IHtmlHelper _htmlHelper;
        public HncInputTagHelper(IHtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }

        public ModelExpression For { get; set; }

        public string Label { get; set; }

        public string ClassName { get; set; }

        public string Value { get; set; }

        public string IconClassName { get; set; }

        public string Placeholder { get; set; }

        /// <summary>
        /// ViewContext
        /// </summary>
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var firstHtml = $"<div class=\"hnc-input-custom hnc-input-wrap {ClassName}\">";
            output.Content.SetHtmlContent(firstHtml);
            // Thêm Label
            var labelName = !string.IsNullOrEmpty(Label) ? Label : For.Name;
            var label = $"<div title=\"{labelName}\" class=\"hnc-label\">{labelName}</div>";

            var isCheckbox = For.ModelExplorer.ModelType.Name == "Boolean";
            output.Content.AppendHtml(label);

            var isCheckboxClassName = isCheckbox ? "is-checkbox" : "";

            var inputBox = $"<div class=\"hnc-input-box {isCheckboxClassName}\">";
            output.Content.AppendHtml(inputBox);

            //container for additional attributes
            var htmlAttributes = new Dictionary<string, object>();
            var classNames = string.IsNullOrEmpty(ClassName) ? "" : ClassName;

            htmlAttributes.Add("class", $"hnc-input {classNames}");

            if (!string.IsNullOrEmpty(Placeholder))
            {
                htmlAttributes.Add("placeholder", Placeholder);
            }

            //contextualize IHtmlHelper
            var viewContextAware = _htmlHelper as IViewContextAware;
            viewContextAware?.Contextualize(ViewContext);

            var htmlOutput = _htmlHelper.Editor(For.Name, null, new { htmlAttributes });

            output.Content.AppendHtml(htmlOutput);
            if (!isCheckbox)
            {
                var iconClassName = !string.IsNullOrEmpty(IconClassName) ? IconClassName : "fas fa-align-left";
                output.Content.AppendHtml($"<div class=\"icon-box\"> <i class=\"{iconClassName}\"></i></div>");
            }
            else
            {
                var checkmark = $"<span class=\"checkmark\"></span>";
                output.Content.AppendHtml(checkmark);
            }
            var lastHtml = "</div></div>";
            output.Content.AppendHtml(lastHtml);

        }
    }
}