using DMS.Models;

namespace DMS.DTO
{
    public class DeviceCategoriesDTO
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }
        public List<string> Properities { get; set; } = new List<string>();
    }
    public class DeviceCategoryViewModel
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }
        public List<PropertyItem> AvailableProperties { get; set; } = new List<PropertyItem>();
        public List<int> SelectedPropertyIds { get; set; }
    }

}
