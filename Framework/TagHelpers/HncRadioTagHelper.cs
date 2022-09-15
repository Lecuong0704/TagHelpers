namespace CustomTagHelpers.TagHelpers
{
    using HncTagHelpers.Model;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    [HtmlTargetElement("hnc-radio")]
    public class HncRadioTagHelper : TagHelper
    {
        public ModelExpression For { get; set; }

        public string? Id { get; set; }

        public string? Label { get; set; }

        public string? ClassName { get; set; }

        private string Value { get; set; }

        public List<HncSelectItemModel> Data { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var label = string.IsNullOrEmpty(Label) ? For.Name : Label;
            

            if (For.Model != null && !string.IsNullOrWhiteSpace(For.Model.ToString()) && Data != null && Data.Any())
            {
                Value = For.Model.ToString();
            }


            if (string.IsNullOrEmpty(Id))
            {
                Id = Guid.NewGuid().ToString();
            }

            var firstHtml = $@"<div class=""hnc-input-custom hnc-radio-wrap {ClassName}"">
					                <div class=""hnc-label"">Radio</div>";
            output.Content.SetHtmlContent(firstHtml);

            var optionsHtml = @"<div class=""radios"">";
            if (Data != null && Data.Any())
            {
                var index = 0;
                foreach (var item in Data)
                {
                    var isSelected = item.Value == Value;
                    var isChecked = isSelected ? "checked" : "";
                    optionsHtml += $@"<div class=""radio-item"">
                                            <label class=""radio-label"" for=""radio-id-{Id}-{index}"">
								                <input class=""radio-input"" type=""radio"" id=""radio-id-{Id}-{index}"" name=""{For.Name}"" value=""{item.Value}"" {isChecked}>
								                <span class=""checkmark""></span>
								                {item.Text}
							                </label>
						                </div>";
                    index++;
                }
            }
            optionsHtml += "</div>";
            output.Content.AppendHtml(optionsHtml);

            var lastHtml = $@"<input class=""input-hide d-none"" value="""" /></ div>";
            output.Content.AppendHtml(lastHtml);
        }
    }
}