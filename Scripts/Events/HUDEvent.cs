using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Omni.Utilities.EventHandlers;

namespace Omni.Events
{
	public class HUDEvent : CustomEvent {

		public static string ON_SHOW_CURSOR = "on_show_cursor";
		public static string ON_HIDE_INFO_BARS = "on_hide_info_bars";
		public static string ON_CHANGE_TO_FPS_VIEW = "on_change_to_fps_view";
		public static string ON_BACK_TO_OTHER_VIEW = "on_back_to_other_view";
		public static string ON_CHANGE_TO_MAIN_VIEW = "on_change_to_main_view";
		public static string ON_CHANGE_TO_TRUCK_VIEW = "on_change_to_truck_view";
		public static string ON_LOGO_IMAGE_WAS_LOADED = "on_logo_image_was_loaded";
		public static string ON_SHOW_PERFORATION_GRAPH = "on_show_perforation_graph";
		public static string ON_CHANGE_TO_OILFIELD_VIEW = "on_change_to_oilfield_view";
		public static string ON_SHOW_SELECTED_ASSET_INFO = "on_show_selected_asste_info";
		public static string ON_CHANGE_TO_WELLHEADS_VIEW = "on_change_to_wellheads_view";

		//SGE EVENTS
		public static string ON_SGE_SHOW_PANEL = "on_sge_show_panel";
		public static string ON_SGE_CHANGE_SCREEN = "on_sge_change_screen";

        public HUDEvent(string eventType = "")
        {
            type = eventType;
        }
	}
}
