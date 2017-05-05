using Omni.Utilities.EventHandlers;

namespace Omni.Events
{
    public class WorldLevelEvent : CustomEvent
    {
		public static string ON_BACK_TO_WORLD = "on_back_to_world";
        public static string ON_CAMERA_TO_POINT = "on_camera_to_point";
		public static string ON_APPLY_FILTERS_TO_WELL = "on_apply_filters_to_well";
        

        public WorldLevelEvent(string eventType = "")
        {
            type = eventType;
        }
    }
}




