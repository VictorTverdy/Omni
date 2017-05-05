
using Omni.Utilities;
using System;
using UnityEngine;

public class GameValuesConfig : GameValues {

    public static void SetShowLocationPanels(bool b, GameObject go) {
        ShowLocationPanels = b;
        debug("***SetShowLocationPanels from ",go);
    }

    public static void SetCurrentWellType(EnumWellType wellType, GameObject go) {
        CurrentWellType = wellType;
        debug("***SetCurrentWellType__" + wellType + "__from", go);
    }

    public static void SetCloudsAnimation(bool enabled) {
        CloudsAnimation = enabled;
        debug("***Clouds animation is" + enabled + "__from PlayerPrefs");
    }

    public static void SetAndSaveCloudsAnimation(bool enabled, GameObject go = null) {
        CloudsAnimation = enabled;
        PlayerPrefs.SetString(Config.PLAYER_PREFS_CLOUD_ANIMATION, enabled.ToString().ToLower());

        if(go!=null) {
            debug("***SetCloudsAnimation save to___" + enabled + "__from", go);
        }else {
            debug("***SetCloudsAnimation save to___" + enabled);
        }
    }

    public static void SetAndSaveRotationCameraAnimation(bool enabled, GameObject go = null)
    {
        RotationCameraAnimation = enabled;
        PlayerPrefs.SetString(Config.PLAYER_PREFS_ROTATION_CAMERA_ANIMATION, enabled.ToString().ToLower());

        if (go != null)
        {
            debug("***SetAndSaveRotationCameraAnimation save to___" + enabled + "__from", go);
        }
        else
        {
            debug("***SetAndSaveRotationCameraAnimation save to___" + enabled);
        }
    }

    public static void SetAndSaveFadeAnimation(bool enabled, GameObject go = null)
    {
        FadeAnimation = enabled;
        PlayerPrefs.SetString(Config.PLAYER_PREFS_FADE_ANIMATION, enabled.ToString().ToLower());

        if (go != null)
        {
            debug("***SetAndSaveFadeAnimation save to___" + enabled + "__from", go);
        }
        else
        {
            debug("***SetAndSaveFadeAnimation save to___" + enabled);
        }
    }

    public static void SetDebug(bool b, GameObject go) {
        DebugMode = b;
        debug("***SetShowLocationPanels from ",go);
    }


    private static void debug(string message, GameObject go) {   
        if(!DebugMode) return;    
        Debug.Log(message + go.name);
    }
    private static void debug(string message) {   
        if(!DebugMode) return;    
        Debug.Log(message);
    }
}
