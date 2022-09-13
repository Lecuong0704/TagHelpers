namespace CustomTagHelpers.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    [HtmlTargetElement("hnc-textarea")]
    public class HncTextareaTagHelper : TagHelper
    {
        private readonly IHtmlHelper _htmlHelper;
        public HncTextareaTagHelper(IHtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }

        public ModelExpression For { get; set; }

        public string Label { get; set; }

        public string ClassName { get; set; }

        public string IconClassName { get; set; }

        public string Placeholder { get; set; }

        public int Rows { get; set; }

        /// <summary>
        /// ViewContext
        /// </summary>
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            var firstHtml = $"<div class=\"hnc-input-custom hnc-textarea-wrap\">";
            output.Content.SetHtmlContent(firstHtml);

            var labelName = string.IsNullOrEmpty(Label) ? For.Name : Label;
            var label = $"<div title=\"{labelName}\" class=\"hnc-label\">{labelName}</div>";
            output.Content.AppendHtml(label);

            // Thêm Icon
            var inputBox = "<div class=\"hnc-input-box\">";
            output.Content.AppendHtml(inputBox);

            var htmlAttributes = new Dictionary<string, object>();
            htmlAttributes.Add("rows", Rows == 0 ? 3 : Rows);
            var classNames = string.IsNullOrEmpty(ClassName) ? "" : ClassName;
            htmlAttributes.Add("class", $"hnc-textarea {classNames}");

            if (!string.IsNullOrEmpty(Placeholder))
            {
                htmlAttributes.Add("placeholder", Placeholder);
            }

            //contextualize IHtmlHelper
            var viewContextAware = _htmlHelper as IViewContextAware;
            viewContextAware?.Contextualize(ViewContext);

            var htmlOutput = _htmlHelper.TextArea(For.Name,  htmlAttributes );
            output.Content.AppendHtml(htmlOutput);

            var iconClassName = !string.IsNullOrEmpty(IconClassName) ? IconClassName : "fas fa-align-left";
            var icon = $"<div class=\"icon-box\"> <i class=\"{iconClassName}\"></i></div></div>";
            output.Content.AppendHtml(icon);

            var lastHtml = "</div>";
            output.Content.AppendHtml(lastHtml);

        }
    }
}