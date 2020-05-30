using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;

public class ChartManager : MonoBehaviour
{
    public Grid Grid;

    public void OnNextCharStep(int[] stats)
    {
        if (CanAdd() == false)
        {
            PrepareToShow();
            ShowEvent();
        }

        Add();
    }

    public void Awake()
    {
        AgentSimulationManager.OnChartUpdate += OnNextCharStep;
    }
}
