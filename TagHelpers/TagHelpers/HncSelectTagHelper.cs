namespace CustomTagHelpers.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    [HtmlTargetElement("hnc-tags")]
    public class HncSelectTagHelper : TagHelper
    {
        public ModelExpression For { get; set; }

        public string? Label { get; set; }

        public string? ClassName { get; set; }

        private string? Value { get; set; }

        public string? IconClassName { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var label = string.IsNullOrEmpty(Label) ? For.Name : Label;

            var tagsHtml = "";

            if (!string.IsNullOrEmpty(For.Model.ToString()))
            {
                Value = For.Model.ToString();
                var arr = Value.Split(",").ToList();
                if(arr != null && arr.Any())
                {
                    var newArr = arr.Where(x=> !string.IsNullOrEmpty(x)).ToList();
                    var index = 0;
                    foreach (var item in newArr)
                    {
                        tagsHtml += $@"<div class=""tag-item"" id=""tag-item-{index}"">
				                    <div class=""tag-value"">{item}</div>
				                    <div class=""tag-delete-btn""><i class=""far fa-times-circle"" onclick=""deleteTag(this)"" ></i></div>
			                    </div>";
                        index++;
                    }
                }
            }

            var firstHtml = $@"<div class=""hnc-input-custom hnc-tags-wrap {ClassName}"">
					                <div title=""{label}"" class=""hnc-label"">{label}</div>
					                <div class=""hnc-tags-box"">
						                <div class=""tags-box"">
                                            {tagsHtml}
							                <input class=""input-tags-set-value"" />
						                </div>
						                <input class=""input-hide d-none""  value=""{Value}"" />";
            output.Content.SetHtmlContent(firstHtml);

            var iconClassName = string.IsNullOrEmpty(IconClassName) ? "far fa-address-card" : IconClassName;
            // Thêm Icon
            var icon = $"<div class=\"icon-box\"><i class=\"{iconClassName}\"></i></div>";
            output.Content.AppendHtml(icon);

            var lastHtml = "</div>\r\n\t\t\t\t</div>";
            output.Content.AppendHtml(lastHtml);
        }
    }
}