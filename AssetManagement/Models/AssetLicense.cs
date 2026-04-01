using System;

namespace AssetManagementApi.Models
{
    public class AssetLicense
    {
        public Guid AssetId { get; set; }
        public Asset? Asset { get; set; }

        public Guid SoftwareLicenseId { get; set; }
        public SoftwareLicense? SoftwareLicense { get; set; }
    }
}
