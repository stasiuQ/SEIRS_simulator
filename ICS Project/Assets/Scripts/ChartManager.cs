using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Seirs;
using UnityEngine;

public class ChartManager : MonoBehaviour
{
    public void OnNextCharStep(int[] stats)
    {
       Globals.GraphService.Add(stats);
    }

    public void Awake()
    {
        AgentSimulationManager.OnChartUpdate += OnNextCharStep;
    }
}
