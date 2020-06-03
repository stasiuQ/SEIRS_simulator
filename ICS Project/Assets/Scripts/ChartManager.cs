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
        data.SeriesS.Add(stats[1]);
        data.SeriesE.Add(stats[2]);
        data.SeriesI.Add(stats[3]);
        data.SeriesR.Add(stats[4]);
        data.SeriesStep.Add(stats[5]);
        Globals.DrawChartMethod(data);
    }

    public void Awake()
    {
        AgentSimulationManager.OnChartUpdate += UpdateData;
    }
}
