namespace DMS.Models
{
    public class PropertyItem
    {
        public int ID { get; set; }
        public string PropertyDescription { get; set; }
        public List<PropertyValue> PropertyValues { get; set; } = new List<PropertyValue>();
        public List<DeviceCategory> DeviceCategories { get; set; } = new List<DeviceCategory>();
    }
}
