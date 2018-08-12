//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication7.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Brand
    {
        public Brand()
        {
            this.AspNetUsers = new HashSet<AspNetUser>();
            this.Devices = new HashSet<Device>();
            this.Locations = new HashSet<Location>();
            this.MediaSrcs = new HashSet<MediaSrc>();
            this.Resolutions = new HashSet<Resolution>();
            this.Scenarios = new HashSet<Scenario>();
        }
    
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public string CreateDateTime { get; set; }
        public string Description { get; set; }
        public bool isActive { get; set; }
    
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<MediaSrc> MediaSrcs { get; set; }
        public virtual ICollection<Resolution> Resolutions { get; set; }
        public virtual ICollection<Scenario> Scenarios { get; set; }
    }
}
