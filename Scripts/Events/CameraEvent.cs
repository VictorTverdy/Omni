using Omni.Utilities.EventHandlers;

namespace Omni.Events
{
	public class CameraEvent : CustomEvent {

		public static string ON_SET_CAMERA_ORBIT = "on_set_camera_orbit";
		public static string ON_CAMERA_ORBIT_ACTIVE = "on_camera_orbit_active";

		public CameraEvent(string eventType = "")
        {
            type = eventType;
        }
	}
}
