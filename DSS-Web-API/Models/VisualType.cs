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
    
    public partial class VisualType
    {
        public VisualType()
        {
            this.Areas = new HashSet<Area>();
            this.Playlists = new HashSet<Playlist>();
        }
    
        public int VisualTypeID { get; set; }
        public string TypeName { get; set; }
    
        public virtual ICollection<Area> Areas { get; set; }
        public virtual ICollection<Playlist> Playlists { get; set; }
    }
}