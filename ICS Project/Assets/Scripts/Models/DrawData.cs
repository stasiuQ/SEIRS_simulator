using System;
using System.Collections.Generic;

namespace Seirs.Models
{
    public class DrawData
    {
        private static DrawData Instamce = new DrawData();
        public static DrawData GetInstance => Instamce;
        private DrawData()
        {
            SeriesE = new int[Globals.Steps];
            SeriesS = new int[Globals.Steps];
            SeriesI = new int[Globals.Steps];
            SeriesR = new int[Globals.Steps];
            SeriesStep = new int[Globals.Steps];
        }
        public int[] SeriesS { get; set; }
        public int[] SeriesE { get; set; }
        public int[] SeriesI { get; set; }
        public int[] SeriesR { get; set; }
        public int[] SeriesStep { get; set; }
        public void clearData()
        {
            Array.Clear(SeriesS, 0, SeriesE.Length);
            Array.Clear(SeriesE, 0, SeriesE.Length);
            Array.Clear(SeriesI, 0, SeriesE.Length);
            Array.Clear(SeriesR, 0, SeriesE.Length);
            Array.Clear(SeriesStep, 0, SeriesE.Length);
          }
    }
}