using System.Collections.Generic;
using System.Threading.Tasks;
using Seirs.Models;

namespace Seirs.Services
{
    public class GraphService : IGraphService
    {
        private BatchStats _displayBatch;
        private BatchStats _currentBatch;

        public GraphService()
        {
            _currentBatch = new BatchStats();
            _displayBatch = new BatchStats();
        }

        public void Add(int[] stats)
        {
            if (_currentBatch.CanAdd() == false)
            {
                PrepareToShow();
                ShowEvent(CreateSeries(_displayBatch));
            }

            _currentBatch.Add(stats);
        }

        private void PrepareToShow()
        {
            _displayBatch = _currentBatch;
            _currentBatch = new BatchStats();
        }

        private void  ShowEvent(DrawData drawData)
        {
            Globals.DrawChartMethod(drawData);
        }

        private DrawData CreateSeries(BatchStats stats)
        {
            var list = stats.ToEnumerable();
            
            var seriesS = new List<int>();
            var seriesE = new List<int>();
            var seriesI = new List<int>();
            var seriesR = new List<int>();
            var seriesStep = new List<int>();
            
            foreach (var item in list)
            {
                seriesS.Add(item[1]);
                seriesE.Add(item[2]);
                seriesI.Add(item[3]);
                seriesR.Add(item[4]);
                seriesStep.Add(item[5]);
            }
            
            return new DrawData
            {
                SeriaE = seriesE,
                SeriaI = seriesI,
                SeriaR = seriesR,
                SeriaS = seriesS,
                SeriaStep = seriesStep
            };

        }
    }
}