using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChartManager : MonoBehaviour
{

    public void OnNextCharStep(int[] stats)
    {
        // add new stats for the chart
    }

    public void Awake()
    {
        AgentSimulationManager.OnChartUpdate += OnNextCharStep;
    }
}
