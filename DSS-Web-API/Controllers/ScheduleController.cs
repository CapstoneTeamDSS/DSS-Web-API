﻿using System;
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
            public Scenario scenario { get; set; }
            public int start_time { get; set; }
            public int end_time { get; set; }
        }

        public class Scenario
        {
            public int scenario_id { get; set; }
            public int layout_id { get; set; }
            public string scenario_title { get; set; }
            public int audio_area { get; set; }
            public long? scenario_update_datetime { get; set; }
            public List<ScenarioItem> scenario_items { get; set; }
        }

        public class ScenarioItem
        {
            public int scenario_id { get; set; }
            public int playlist_id { get; set; }
            public long? playlist_update_datetime { get; set; }
            public int display_order_playlist { get; set; }
            public int area_id { get; set; }
            public int visual_type_id { get; set; }
            public List<PlaylistItem> playlist_items { get; set; }
        }

        public class PlaylistItem
        {
            public int playlist_item_id { get; set; }
            public int mediasrc_id { get; set; }
            public int display_order_media { get; set; }
            public int duration { get; set; }
            public string url_media { get; set; }
            public string title_media { get; set; }
            public string extension_media { get; set; }
            public int type_id { get; set; }
            public string security_hash { get; set; }
        }

        public Schedule GetNextScenario(string matchingCode)
        {
            Schedule result = null;
            var currDevice = db.Devices.Select(device=>device).Where(d => d.MatchingCode.CompareTo(matchingCode)==0).FirstOrDefault();
            var queryDateTime = DateTime.Now;
            var currDateOfWeek = 6 - ((int)queryDateTime.DayOfWeek) + 1; //Monday:6; Tuesday: 5, ... Sunday: 0;
            if (currDateOfWeek == 7)
            {
                currDateOfWeek = 0;
            }
            var currTime = 11 - (int)Math.Floor((double)queryDateTime.Hour / 2); //0,1h->11; 2,3h->10,...,22,23h->0
            var dayFilterPoint = (int)Math.Pow(2, currDateOfWeek); //Lấy số mũ theo ngày trong tuần, Mon -> Sun (0-6)
            var timeFilterPoint = (int)Math.Pow(2, currTime); //Lấy số mũ theo time slot \
            var nextSchedule = currDevice.Schedules.Select(schedule => schedule).Where(
                schedule => ((schedule.isEnable == true) && (schedule.DayFilter & dayFilterPoint) == dayFilterPoint) && ((schedule.TimeFilter & timeFilterPoint) == timeFilterPoint)).OrderByDescending(schedule => schedule.Priority).ThenByDescending(schedule => schedule.ScheduleID).FirstOrDefault();
            var currTimeSlot = db.TimeSlots.Select(slot => slot).Where(slot => (slot.StartTime <= queryDateTime.TimeOfDay && slot.EndTime >= queryDateTime.TimeOfDay)).FirstOrDefault();
            if (nextSchedule != null)
            {
                var scenario = db.Scenarios.Find(nextSchedule.ScenarioID, nextSchedule.LayoutID);
                var scenarioItems = scenario.ScenarioItems.Select(a => new ScenarioItem
                {
                    scenario_id = a.ScenarioID,
                    playlist_id = a.Playlist.PlaylistID,
                    display_order_playlist = a.DisplayOrder,
                    area_id = a.AreaID,
                    playlist_update_datetime = ((DateTime) a.Playlist.UpdateDateTime).Ticks,
                    visual_type_id = a.Area.VisualTypeID,
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
                         type_id = b.MediaSrc.TypeID,
                         security_hash = b.MediaSrc.SecurityHash,
                     }).ToList()
                }).ToList();
                DateTime ScenarioDateTime = scenario.UpdateDateTime??DateTime.Now;
                var scenarioObj = new Scenario
                {
                    layout_id = scenario.LayoutID,
                    scenario_id = scenario.ScenarioID,
                    scenario_title = scenario.Title,
                    scenario_items = scenarioItems,
                    audio_area = scenario.AudioArea,
                    scenario_update_datetime = ScenarioDateTime.Ticks,
                };
                result = new Schedule
                {
                    scenario = scenarioObj,
                    start_time = (int)currTimeSlot.StartTime.TotalMilliseconds,
                    end_time = (int)currTimeSlot.EndTime.TotalMilliseconds,
                };
            }
            return result;
        }

        // GET: api/Layout
        public List<DateTime> listDatatime = new List<DateTime>();
        /* public List<Schedule> GetReviewsWithUserByItem(int boxId)
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
        } */
    }
}