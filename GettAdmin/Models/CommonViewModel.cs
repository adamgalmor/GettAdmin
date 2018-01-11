using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GettAdmin
{
    public partial class CommonViewModel
    {
        public Drivers DriverModel { get; set; }
        public Riders RiderModel { get; set; }
        public Orders OrderModel { get; set; }
    }
}