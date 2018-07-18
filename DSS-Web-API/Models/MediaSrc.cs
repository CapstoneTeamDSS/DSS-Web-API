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
    
    public partial class MediaSrc
    {
        public MediaSrc()
        {
            this.PlaylistItems = new HashSet<PlaylistItem>();
        }
    
        public int MediaSrcID { get; set; }
        public int BrandID { get; set; }
        public string Title { get; set; }
        public Nullable<bool> Status { get; set; }
        public int TypeID { get; set; }
        public string URL { get; set; }
        public Nullable<System.DateTime> UpdateDatetime { get; set; }
        public Nullable<System.DateTime> CreateDatetime { get; set; }
        public string Description { get; set; }
        public string Extension { get; set; }
    
        public virtual Brand Brand { get; set; }
        public virtual MediaType MediaType { get; set; }
        public virtual ICollection<PlaylistItem> PlaylistItems { get; set; }
    }
}
