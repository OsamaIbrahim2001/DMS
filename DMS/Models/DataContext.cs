using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace DMS.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<PropertyItem> PropertyItems { get; set; }
        public DbSet<DeviceCategory> DeviceCategories { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<PropertyValue> PropertyValues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define models
            modelBuilder.Entity<PropertyItem>();
            modelBuilder.Entity<DeviceCategory>();
            modelBuilder.Entity<Device>();
            modelBuilder.Entity<PropertyValue>();

            // Populate data

            var propertyItems = new List<PropertyItem>
            {
                new PropertyItem { ID = 1, PropertyDescription = "HD" },//0
                new PropertyItem { ID = 3, PropertyDescription = "Processor" },//1
                new PropertyItem { ID = 5, PropertyDescription = "Dimensions" },//2
                new PropertyItem { ID = 6, PropertyDescription = "MAC Address" },//3
                new PropertyItem { ID = 7, PropertyDescription = "IP Address" },//4
                new PropertyItem { ID = 8, PropertyDescription = "Allow USB" },//5
                new PropertyItem { ID = 9, PropertyDescription = "Current User" },//6
                new PropertyItem { ID = 10,PropertyDescription = "Network Speed" },//7
                new PropertyItem { ID = 11,PropertyDescription = "Operating System" },//8
                new PropertyItem { ID = 12, PropertyDescription = "Ports" }//9
            };



            var deviceCategories = new List<DeviceCategory>
            {
                new DeviceCategory
                {
                    ID = 1,
                    CategoryName = "Printer"
                },
                new DeviceCategory
                {
                    ID = 2,
                    CategoryName = "Laptop"
                },
                new DeviceCategory
                {
                    ID = 3,
                    CategoryName = "Switch"
                }
            };




            modelBuilder.Entity<PropertyItem>().HasData(propertyItems);

            modelBuilder.Entity<DeviceCategory>().HasData(deviceCategories);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.EnableSensitiveDataLogging();
        //}
    }
}
