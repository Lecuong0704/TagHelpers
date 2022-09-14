using HncTagHelpers.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HncTagHelpers.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            IsMale = true;
            Positions = new List<HncSelectItemModel>();
            Positions.Add(new HncSelectItemModel("Giám đốc", "1"));
            Positions.Add(new HncSelectItemModel("Trưởng phòng", "2"));
            Positions.Add(new HncSelectItemModel("Nhân viên IT", "3"));
            Positions.Add(new HncSelectItemModel("Kế toán trưởng", "4"));
            Positions.Add(new HncSelectItemModel("Thủ quỹ", "5"));
            Positions.Add(new HncSelectItemModel("Nhân viên kinh doanh", "6"));
            Positions.Add(new HncSelectItemModel("Nhân viên chăm sóc khách hàng", "7"));
            Positions.Add(new HncSelectItemModel("Nhân viên giao hàng", "8"));
            Detail = "Cường,Chiến,Hiếu,Kiên";
        }

        public void OnGet()
        {

        }
        public string Name { get; set; }

        public string Detail { get; set; }

        public decimal Amount { get; set; }

        public bool IsMale { get; set; }

        public List<HncSelectItemModel> Positions { get; set; }

        public string Position { get; set; }
    }

}