using System;

namespace Omni.Entities
{
    [Serializable]
    public class AssetListInfo
    {
        public AssetList[] AssetLists;
    }

    [Serializable]
	public class AssetList
	{
		public AssetList()
		{
           
		}      
        public int Status;  //0 visible, 1 invisible, 2 arriving       
        public string GameObjectName;
        public float posX;
        public float posY;
        public float posZ;       
        public string CurrentOperator;
        public string CurrentWelNameNumber;
        public string PrimaryCompletionDate;
        public string SecondaryCompletionDate;
        public string Basin;
        public string Formation;
        public string SubFormation;
        public string TechnologyApplication;
        public string SystemFailures;
        public string UserComments;
        public string Latitude;
        public string Longitude;
        public string BottomLatitude;
        public string BottomLongitude;
        public string WellStatus;
        public string OriginalOperator;
        public string OriginalWellNameNumber;
        public string StateFile;
        public string State;
        public string County;
        public string Field;
        public string Section;
        public string Township;
        public string Range;
        public string DirectionalWellboreType;
        public string CoordinateSystem;
        public string LateralAzimuth;
        public string AverageLateralTVD;
        public string UpperPerforationLimit;
        public string LowerPerforationLimit;
        public string TotalMeasuredDepth;
        public string StimulationServiceCompany;
        public string JobRate;
        public string StimulationZones;
        public string TotalTreatmentFluidPumped;
        public string TotalProppantPumped;
        public string CompletionSystem;
        public string StimulationFluid;
        public string AnnularIsolation;
        public string PrimaryProppant;
        public string FirstReportedAPIGravity;
        public string CompletedLateralLenght;
        public string SurfaceElevation;
        public string TDVSS;
        public string CumulativeOilProduction;
        public string MonthlyOilProduction;
        public string CumulativeGasProduction;
        public string MonthlyGasProduction;
        public string CumulativeWaterProduction;
        public string MonthlyWaterProduction;  
    }
 

}