namespace Omni.Utilities
{
	public enum UIListEnum
	{
		None = -1,
		LoadingData = 0,
		Tutorial,
        GeneralHud,
        WorldHud,       
		HightLevelHud,
		WellPathHud,
		DrillingToolHud
	}

	public enum AssetType
	{
		None = -1,
		DrillTower = 0,
		MachineRoom,
		SupliesRoom,
		GasolineRoom,
		Facilities
	}

	//0 visible, 1 invisible, 2 arriving
	public enum AssetStatus
	{
		None = -1,
		Visible = 0,
		Invisible,
		PendingToArrive
	}

	public enum TypeCamera
	{
		None = -1,
        WorldLevel,
        HeightLevel,
        NormalView,
        TruckView,
        WellHeads,
        SpecialWellHeads,
        FpsView
	}

    public enum EnumWellType
    {
        None = -1,
        Default,
        Drilling,
        WorkOver,
        Abandoned
    }

    public enum EnumDrillingMachinePieces
    {
        None = -1,
        DrillingJar,
        MudMotor,
        DrillingHead
    }

	public enum GameViewState
	{
		FieldView,
		InAnAsset,
		IsInFps
	}

	public enum InsideFieldLocation
	{
		None = -1,
		WellHeads,
		Wireline,
		Hse,
		DrillingTower,
		SelectedInsideAsset
	}

    public enum EnumGameViews {
        None,
        WellBars,
        Well_FieldTopView,
        Well_HeadWellsView,
        Well_LabView,
        Well_HSEView,
        Well_TowerView,
        Well_FPSView
    }

	public enum WellCategory
	{
		Jobs,
		Wells,
		Personnel,
		Assets
	}

    public enum EnumScenes {
        None,
        Intro,
        WorldMap,
        Wellbars,
        Game,
        Tutorial
    }
}