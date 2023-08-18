using DMS.Models;

namespace DMS.DTO
{
    public class DevicesDTO
    {
        public int ID { get; set; }
        public string DeviceName { get; set; }
        public string DeviceCategory { get; set; }
        public int DeviceCategoryID { get; set; }

        public DateTime AcquisitionDate { get; set; }
        public string  Memo{ get; set; }
        public Dictionary<string, string> PropertiesValues { get; set; } = new Dictionary<string, string>();
    }


    public class PropertyItemDTO
    {
        public int ID { get; set; }
        public string PropertyDescription { get; set; }
    }

    public class PropertyValueDTO
    {
        public int PropertyID { get; set; }
        public string Value { get; set; }
    }

    public class DeviceViewModel
    {
        public int DeviceID { get; set; }
        public int DeviceCategoryID { get; set; }
        public string DeviceName { get; set; }
        public DateTime AcquisitionDate { get; set; }
        public string Memo { get; set; }

        public List<PropertyItemDTO> AvailableProperties { get; set; } = new List<PropertyItemDTO>();
        public List<string> PropertyValues { get; set; } = new List<string>();
        public List<int> PropertyItemsID { get; set; } = new List<int>();

    }


}
