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
    
    public partial class Area
    {
        public Area()
        {
            this.ScenarioItems = new HashSet<ScenarioItem>();
        }
    
        public int AreaID { get; set; }
        public int LayoutID { get; set; }
        public string AreaCode { get; set; }
        public string URL { get; set; }
        public Nullable<int> VisualTypeID { get; set; }
    
        public virtual Layout Layout { get; set; }
        public virtual VisualType VisualType { get; set; }
        public virtual ICollection<ScenarioItem> ScenarioItems { get; set; }
    }
}
