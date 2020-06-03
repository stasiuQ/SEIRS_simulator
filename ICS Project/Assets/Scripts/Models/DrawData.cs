using System.Collections.Generic;

namespace Seirs.Models
{
    public class DrawData
    {
        private static DrawData Instamce = new DrawData();
        public static DrawData GetInstance => Instamce;
        private DrawData()
        {
            SeriesE = new List<int>();
            SeriesS = new List<int>();
            SeriesI = new List<int>();
            SeriesR = new List<int>();
            SeriesStep = new List<int>();
        }
        public List<int> SeriesS { get; set; }
        public List<int> SeriesE { get; set; }
        public List<int> SeriesI { get; set; }
        public List<int> SeriesR { get; set; }
        public List<int> SeriesStep { get; set; }
        public void clearData()
        {
            SeriesS.Clear();
            SeriesE.Clear();
            SeriesI.Clear();
            SeriesR.Clear();
            SeriesStep.Clear();
        }
    }
}