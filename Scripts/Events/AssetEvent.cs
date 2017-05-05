using Omni.Utilities.EventHandlers;

namespace Omni.Events
{
	public class AssetEvent : CustomEvent
    {
		public static string ON_SELECTED_ASSET = "on_selected_asset";
		public static string ON_BACK_TO_ALL_VIEW = "on_back_to_all_view";
		public static string ON_ACTIVE_WELLHEADS_COLLIDERS = "on_active_wellheads_colliders";

		public AssetEvent(string eventType = "")
        {
            type = eventType;
        }
    }
}




