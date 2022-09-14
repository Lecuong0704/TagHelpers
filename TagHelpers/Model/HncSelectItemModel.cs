namespace HncTagHelpers.Model
{
	public class HncSelectItemModel
	{
		public HncSelectItemModel(string text, string value)
		{
			Text = text;
			Value = value;
		}
		public string Text { get; set; }

		public string Value { get; set; }

	}
}
