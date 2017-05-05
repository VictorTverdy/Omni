using Omni.Utilities;

public class GameValues{
    private static bool showLocationPanels = true;
    private static bool debugMode = true;
    public static bool isInFPSMode = false;
    public static bool disableOrbit = false;
    public static bool isMouseReleased = false;
    private static EnumGameViews currentGameView;
    private static EnumWellType currentWellType;
    public static float RaycastLenghtFPS = 4.5f;
    public static float RaycastLenghtTopView =100;
    public static EnumScenes lastScene;

    //Stores if the users want to see the cloud animation
    private static bool cloudsAnimation;
    public static bool CloudsAnimation {get { return cloudsAnimation; } protected set {  cloudsAnimation = value;}}

    private static bool rotationCameraAnimation;
    public static bool RotationCameraAnimation { get { return rotationCameraAnimation; } protected set { rotationCameraAnimation = value; } }

    private static bool fadeAnimation;
    public static bool FadeAnimation { get { return fadeAnimation; } protected set { fadeAnimation = value; } }

    public static EnumGameViews CurrentGameView { get {return currentGameView; } protected set { currentGameView = value;}}
    public static bool ShowLocationPanels {get { return showLocationPanels; } protected set {  showLocationPanels = value;}}
    public static bool DebugMode { get { return debugMode;} protected set {debugMode = value;}}
    public static EnumWellType CurrentWellType { get { return currentWellType;} protected set {currentWellType = value;}}
}
