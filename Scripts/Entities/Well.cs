using System;
namespace Omni.Entities {
    [Serializable]
    public class WellsConfig
    {
        public Well[] WellInfo;
    }

    [Serializable]
    public struct Well
    {
        public string ActualNameNumber;
        public string MonthlyOilProdBbls;
        public string MonthlyWaterProdBbls;
        public string MonthlyGasProdMcf;
        public string ApiN;
        public string ActualOperator;
        public string Section;
        public string Formation;
        public string Township;
        public string Range;
        public string Basin;
        public string Field;
        public string County;
        public string State;
        public string TotalMeasuredDepth;
        public string Latitude;
        public string Longitude;
        public string X;
        public string Y;
        public string Z;

    }
}
