namespace DMS.Models
{
    public class DeviceCategory
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }
        public List<Device> Devices { get; set; } = new List<Device>();
        public List<PropertyItem> Properties { get; set; } = new List<PropertyItem>();
    }
}
