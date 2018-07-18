using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication7.Models;

namespace WebApplication7
{
    public class Schedule
    {   
        public int layoutId { get; set; }
        public string title  { get; set; }
        
        public List<Area> areas { get; set; }
         
    }
}