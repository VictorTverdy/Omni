using Omni.Utilities.EventHandlers;

namespace Omni.Events
{
    public class TrainingEvent : CustomEvent
    {

        public static string open_menu = "open_menu";
        public static string close_menu = "close_menu";
        public static string auto_menu = "auto_menu";
        public static string restart = "restart";
        public static string open_trainig_chapters = "OpenTrainigChapters";

        public TrainingEvent(string eventType = "")
        {
            type = eventType;
        }
    }
}
