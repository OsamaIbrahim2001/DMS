namespace DMS.Models
{
    public class Device
    {
        public int ID { get; set; }
        public string DeviceName { get; set; }
        public int DeviceCategoryID { get; set; }
        public DeviceCategory DeviceCategory { get; set; }
        public DateTime AcquisitionDate { get; set; }
        public string Memo { get; set; }
        public List<PropertyValue> PropertyValues { get; set; } = new List<PropertyValue>();
    }
}
