namespace CustomTagHelpers.TagHelpers
{
    using HncTagHelpers.Model;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    [HtmlTargetElement("hnc-select")]
    public class HncSelectTagHelper : TagHelper
    {
        public ModelExpression For { get; set; }

        public string Id { get; set; }

        public string Label { get; set; }

        public string ClassName { get; set; }

        public string Value { get; set; }

        public string IconClassName { get; set; }

        public string Placeholder { get; set; }

        public bool IsMultiSelect { get; set; }

        public List<HncSelectItemModel> Data { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var label = string.IsNullOrEmpty(Label) ? For.Name : Label;
            if (string.IsNullOrEmpty(Placeholder))
            {
                Placeholder = "Select ...";
            }

            var value = string.IsNullOrEmpty(Value) ? Placeholder : Value;
            var multiSelect = IsMultiSelect ? "multi-select" : "";
            var noValue = string.IsNullOrEmpty(Value) ? "no-value" : "";

            if (string.IsNullOrEmpty(Id))
            {
                Id = Guid.NewGuid().ToString();
            }


            var firstHtml = $@"<div class=""hnc-input-custom hnc-select-wrap {multiSelect}"" id={Id}>
                                <div title=""Position"" class=""hnc-label"">{label}</div>
					                <div class=""hnc-select-box"">
						                <div class=""select-box"">
							                <div class=""selected"">
								                <input class=""input-hide d-none"" name=""{For.Name}"" />
								                <div class=""value {noValue}"" data-placeholder=""{Placeholder}"">{value}</div>
								                <div class=""icon-dropdown"">
									            <i class=""fas fa-caret-down""></i>
								            </div>
							            </div>";
            output.Content.SetHtmlContent(firstHtml);

            var optionsHtml = @"<div class=""options"">";
            if(Data != null && Data.Any())
            {
                foreach (var item in Data)
                {
                    optionsHtml += $"<div class=\"select-item\" data-value=\"{item.Value}\" data-text=\"{item.Text}\">{item.Text}</div>";
                }
            }
            optionsHtml += "</div>\r\n\t\t\t\t\t\t</div>";

            output.Content.AppendHtml(optionsHtml);

            var iconClassName = string.IsNullOrEmpty(IconClassName) ? "far fa-address-card" : IconClassName;
            // Thêm Icon
            var icon = $"<div class=\"icon-box\"><i class=\"{iconClassName}\"></i></div></div>\r\n\t\t\t\t</div>";
            output.Content.AppendHtml(icon);
        }
    }
}