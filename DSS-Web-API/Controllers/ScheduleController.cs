using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class ScheduleController : ApiController
    {
        private DSSEntities db = new DSSEntities();


        public class Schedule
        {
            public int schedule_id { get; set; }
            public int scenario_id { get; set; }
            public int layout_id{ get; set; }
            public string schedule_title { get; set; }
            public DateTime start_time { get; set; }
            public DateTime? end_time { get; set; }
            public int? times_to_play { get; set; }
            public List<ScenarioItem> scenario_items { get; set; }
        }

        public class ScenarioItem
        {
            public int scenario_item_id { get; set; }
            public int playlist_id { get; set; }
            public int display_order_playlist { get; set; }
            public int area_id { get; set; }
            public List<PlaylistItem> playlist_items { get; set; }
        }

        public class PlaylistItem
        {
            public int playlist_item_id { get; set; }
            public int mediasrc_id { get; set; }
            public int display_order_media { get; set; }
            public string duration { get; set; }
            public string url_media { get; set; }
            public string title_media { get; set; }
            public string extension_media { get; set; }
            public int type_id { get; set; }
            
        }
        
        // GET: api/Layout
        public List<DateTime> listDatatime = new List<DateTime>();
        public List<Schedule> GetReviewsWithUserByItem(int boxId)
        {
            var box = db.Boxes.Find(boxId);
            var scenarios = box.Devices.SelectMany(device => device.DeviceScenarios).Select(a => a.Scenario);

            var devicescenario = box.Devices.SelectMany(device => device.DeviceScenarios);
            List<Schedule> schedules = new List<Schedule>();
            foreach (var item in devicescenario)
            {
                var scenario = db.Scenarios.Find(item.ScenarioID, item.LayoutID);
                var ScenarioTitle = scenario.Title;
                var LayoutId = scenario.LayoutID;
                var ScenarioItems = scenario.ScenarioItems.Select(a => new ScenarioItem
                {
                    
                    playlist_id = a.Playlist.PlaylistID,
                    display_order_playlist = a.DisplayOrder,
                    area_id = a.AreaID,
                    playlist_items = a.Playlist
                     .PlaylistItems
                     .Select(b => new PlaylistItem
                     {
                         playlist_item_id = b.PlaylistItemID,
                         mediasrc_id = b.MediaSrc.MediaSrcID,
                         display_order_media = b.DisplayOrder,
                         duration = b.Duration,
                         url_media = b.MediaSrc.URL,
                         title_media = b.MediaSrc.Title,
                         extension_media = b.MediaSrc.Extension,
                         type_id = b.MediaSrc.TypeID
                     }).ToList()
                }).ToList();
                schedules.Add(new Schedule
                {
                    schedule_id = item.DeviceScenationID,
                    scenario_id = item.ScenarioID,
                    start_time = item.StartTime, 
                    end_time = item.EndTime,
                    times_to_play = item.TimesToPlay,
                    schedule_title = ScenarioTitle,
                    layout_id = LayoutId,
                    scenario_items = ScenarioItems
                });
            }
            return schedules;
        }
    }
}