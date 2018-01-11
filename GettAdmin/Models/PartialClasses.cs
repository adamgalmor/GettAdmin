using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GettAdmin
{
    public class PartialClasses
    {
        [MetadataType(typeof(DriverMetadata))]
        public partial class Drivers
        {
        }

        [MetadataType(typeof(RiderMetadata))]
        public partial class Riders
        {
        }
    }
}