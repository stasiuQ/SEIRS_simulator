using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChartManager : MonoBehaviour
{
    public Grid Grid;
    public void OnNextCharStep(int[] stats)
    {    }

    public void Awake()
    {
        AgentSimulationManager.OnChartUpdate += OnNextCharStep;
    }
}
