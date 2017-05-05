using UnityEngine;
using System.Collections;

namespace Omni.Utilities
{
	public class Config 
	{
		public static string URL_BASE;
		public static string URL_BASE_REST;
		public static string URL_BASE_ASSET;
		public static string URL_BASE_ASSETBUNDLES;

		public static string LOGO_IMAGE_PATH;
		public static string LOGO_IMAGE_ENTERPRISE_PATH;

		public static string LOGO_IMAGE_AT_LOCAL_DISK;

        //PLAYER PREFS
        public static string PLAYER_PREFS_FADE_ANIMATION = "fadeAnimation";
        public static string PLAYER_PREFS_CLOUD_ANIMATION = "cloudAnimation";
        public static string PLAYER_PREFS_ROTATION_CAMERA_ANIMATION = "rotationCameraAnimation";

        public static void Configure()
		{
			//Check Runtime Platforms
			if(Application.platform == RuntimePlatform.WindowsEditor 
			   || Application.platform == RuntimePlatform.OSXEditor)
			{
				URL_BASE = "http://localhost:8008/";
			}
			else
			{
				URL_BASE = "http://8.8.8.8:80/";
			}

			URL_BASE_REST = "rest-path";

			LOGO_IMAGE_PATH = "Sprites/";
			LOGO_IMAGE_ENTERPRISE_PATH = "asset_omni_logo_ofs_pro";

			LOGO_IMAGE_AT_LOCAL_DISK = "file://" + Application.streamingAssetsPath + "/client_logo.png";

            ConfigurePlayerPrefs();

        }

        private static void ConfigurePlayerPrefs()
        {
            //CLOUD ANIMATION
            if (PlayerPrefs.HasKey(Config.PLAYER_PREFS_CLOUD_ANIMATION))
            {
                GameValuesConfig.SetCloudsAnimation(PlayerPrefs.GetString(Config.PLAYER_PREFS_CLOUD_ANIMATION) == "true");
            }
            else
            {
                GameValuesConfig.SetCloudsAnimation(true);
            }

            // CAMERA ROTATION ANIMATION
            if (PlayerPrefs.HasKey(Config.PLAYER_PREFS_ROTATION_CAMERA_ANIMATION))
            {
                GameValuesConfig.SetAndSaveRotationCameraAnimation(PlayerPrefs.GetString(Config.PLAYER_PREFS_ROTATION_CAMERA_ANIMATION) == "true");
            }
            else
            {
                GameValuesConfig.SetAndSaveRotationCameraAnimation(true);
            }

            // FADE ANIMATION
            if (PlayerPrefs.HasKey(Config.PLAYER_PREFS_FADE_ANIMATION))
            {
                GameValuesConfig.SetAndSaveFadeAnimation(PlayerPrefs.GetString(Config.PLAYER_PREFS_FADE_ANIMATION) == "true");
            }
            else
            {
                GameValuesConfig.SetAndSaveFadeAnimation(true);
            }
        }
	}
}