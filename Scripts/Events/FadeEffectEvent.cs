using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Omni.Utilities.EventHandlers;

namespace Omni.Events
{
	public class FadeEffectEvent : CustomEvent {

		public static string ON_START_FADE_IN_EFFECT = "on_start_fade_in_effect";
		public static string ON_START_FADE_OUT_EFFECT = "on_start_fade_out_effect";

		public static string ON_FADE_ANIMATION_COMPLETED = "on_fade_animation_complete";
		public static string ON_FADE_IN_ANIMATION_FINISHED = "on_fade_in_animation_finished";

		public static string ON_FADE_ANIMATION_STATE_COMPLETED = "on_fade_animation_state_complete";

        public FadeEffectEvent(string eventType = "")
        {
            type = eventType;
        }
	}
}
