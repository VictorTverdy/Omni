using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Omni.Utilities.EventHandlers;

namespace Omni.Events
{
	public class AssetUIEvent : CustomEvent {

		public static string ON_ENABLE_COLLIDER = "on_enable_collider";
		public static string ON_CHANGE_TO_INSIDE_VIEW= "on_change_to_inside_view";
		public static string ON_CHANGE_TO_WELLHEADS_VIEW = "on_change_to_WellHeads_drill_view";

		public AssetUIEvent(string eventType = "")
        {
            type = eventType;
        }
	}
}
