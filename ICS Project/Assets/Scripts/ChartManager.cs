using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Seirs;
using Seirs.Models;
using UnityEngine;

public class ChartManager : MonoBehaviour
{
    DrawData data = DrawData.GetInstance;
    public void UpdateData(int[] stats)
    {
        Debug.Log( string.Join("/",stats));
        
        if (stats[5] <= Globals.Steps)
        {
            Globals.UpdateChartMethod(stats);
            data.SeriesS[stats[5]] = stats[1];
            data.SeriesE[stats[5]] = stats[2];
            data.SeriesI[stats[5]] = stats[3];
            data.SeriesR[stats[5]] = stats[4];
            data.SeriesStep[stats[5]] = stats[5];
            
        }
    }

    public void Awake()
    {
        AgentSimulationManager.OnChartUpdate += UpdateData;
    }
}
