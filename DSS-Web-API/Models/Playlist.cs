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
    
    public partial class Playlist
    {
        public Playlist()
        {
            this.PlaylistItems = new HashSet<PlaylistItem>();
            this.ScenarioItems = new HashSet<ScenarioItem>();
        }
    
        public int PlaylistID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int BrandID { get; set; }
    
        public virtual Brand Brand { get; set; }
        public virtual ICollection<PlaylistItem> PlaylistItems { get; set; }
        public virtual ICollection<ScenarioItem> ScenarioItems { get; set; }
    }
}
