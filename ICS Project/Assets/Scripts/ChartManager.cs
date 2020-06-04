using Seirs;
using Seirs.Models;
using UnityEngine;

public class ChartManager : MonoBehaviour
{
    public void UpdateData(int[] stats)
    {
        //Debug.Log( string.Join("/",stats));
        if (stats[5] <= Globals.Steps-1)
        {
            // Globals.UpdateChartMethod(stats);
        }
    }

    public void Awake()
    {
        AgentSimulationManager.OnChartUpdate += UpdateData;
    }
}
