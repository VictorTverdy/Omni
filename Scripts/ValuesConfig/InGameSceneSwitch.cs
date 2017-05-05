using Omni.Utilities;

public class InGameSceneSwitch : GameValues {

	public static void GotoViewTower() {
        CurrentGameView = EnumGameViews.Well_TowerView;
        //do stuf
    }
	public static void GotoViewFieldTop() {
        CurrentGameView = EnumGameViews.Well_FieldTopView;
        //do stuf
    }
	public static void GotoViewFPS() {
        CurrentGameView = EnumGameViews.Well_FPSView;
        //do stuf
    }
	public static void GotoViewWellHeads() {
        CurrentGameView = EnumGameViews.Well_HeadWellsView;
        //do stuf
    }
	public static void GotoViewHSE() {
        CurrentGameView = EnumGameViews.Well_HSEView;
        //do stuf
    }
	public static void GotoViewLab() {
        CurrentGameView = EnumGameViews.Well_LabView;
        //do stuf
    }
	public static void GotoViewWellBars() {
        CurrentGameView = EnumGameViews.WellBars;
        //do stuf
    }
}
