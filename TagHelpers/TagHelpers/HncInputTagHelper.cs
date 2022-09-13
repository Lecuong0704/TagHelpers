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
            var firstHtml = $"<div class=\"hnc-input-custom hnc-input-wrap\">";
            output.Content.SetHtmlContent(firstHtml);
            // Thêm Label
            var labelName = !string.IsNullOrEmpty(Label) ? Label : For.Name;
            var label = $"<div title=\"{labelName}\" class=\"hnc-label\">{labelName}</div>";
            output.Content.AppendHtml(label);
            if(For.ModelExplorer.ModelType.Name != "Boolean")
            {
                // Thêm Icon
                var icon = "<div class=\"hnc-input-box\">";
                output.Content.AppendHtml(icon);
            }

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

            var htmlOutput = _htmlHelper.Editor(For.Name, null , new { htmlAttributes });

            output.Content.AppendHtml(htmlOutput);
            if (For.ModelExplorer.ModelType.Name != "Boolean")
            {
                var iconClassName = !string.IsNullOrEmpty(IconClassName) ? IconClassName : "fas fa-align-left";
                output.Content.AppendHtml($"<div class=\"icon-box\"> <i class=\"{iconClassName}\"></i></div></div>");
            }
            var lastHtml = "</div>";
            output.Content.AppendHtml(lastHtml);
            
        }
    }
}