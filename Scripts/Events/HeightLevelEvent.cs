using Omni.Utilities.EventHandlers;

namespace Omni.Events
{
	public class HeightLevelEvent : CustomEvent {

		public static string ON_SHOW_WELL_INFO = "on_show_well_info";

		public HeightLevelEvent(string eventType = "")
        {
            type = eventType;
        }
	}
}


