namespace DMS.Models
{
    public class PropertyValue
    {
        public int ID { get; set; }
        public int DevicesID { get; set; }
        public Device Devices { get; set; }
        public int PropertyItemID { get; set; }
        public PropertyItem PropertyItem { get; set; }
        public string Value { get; set; }
    }
}
