using Seirs;
using Seirs.Models;
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class AgentSimulationManager : MonoBehaviour
{
    [DllImport("COVID_DLL.dll", EntryPoint = "send", CallingConvention = CallingConvention.Cdecl)]
    private static extern void SendDLL(int commonInstr, double[] commonParams, double[] commonAgentsState, int[] commonStats, double[] commonHomes);
    [SerializeField] private Transform plane;
    [SerializeField] private Toggle isolation;
    [SerializeField] private Toggle couriers;
    [SerializeField] private Toggle masks;

    public static event Action<double[], float> OnNextStep;
    public static event Action<double[]> OnAddHomes;
    public static event Action<int[]> OnChartUpdate;

    public int[] Stats { get; set;} = { 6, 0, 0, 0, 0, 0 };
    public int StepsPerSec { get => stepsPerSec; set { stepsPerSec = value; waitTime = 1.0 / stepsPerSec; }}
    public int AgentsCount { get => agentsCount; set { agentsCount = value; size = (agentsCount * 3) + 1; } }
    public Transform Plane { get => plane; set => plane = value; }
    public Toggle Isolation { get => isolation; set => isolation = value; }
    public Toggle Couriers { get => couriers; set => couriers = value; }
    public Toggle Masks { get => masks; set => masks = value; }

    private int agentsCount;
    private int size = 955;
    private double[] agentState;
    private int stepsPerSec = 20;
    private double waitTime;
    private double currentTime=0;
    private double[] homes;
    private bool isIsolation;

    private void Start()
    {
        SetDelegates();
    }

    private void SetDelegates()
    {
        Globals.StartMethod = StartPauseSimulation;
        Globals.ClearMethod = ClearSimulation;
    }

    private void Awake()
    {
        agentState = new double[size];
        waitTime = 1.0/ stepsPerSec;
        homes = new double[(int)Globals.Parameters.LinearZones* (int)Globals.Parameters.LinearZones * 3 + 1];
        SetPlaneScale();
    }

    private void SetPlaneScale()
    {
        var scale = (float)Globals.Parameters.Size / 10f + 0.5f;
        plane.localScale = new Vector3(scale, 1, scale);
    }

    public void StartPauseSimulation()
    {
        Debug.Log("StartPauseSimulation");
        if(Globals.IsCleared)
        {
            CreateSymulation();
            Globals.Parameters = Globals.ParametersEdited;
        }

        if(!Globals.State)
        {
            ChangeParameters();
            ProceedSimulation();
        }
        Globals.State = !Globals.State;
    }

    private void Update()
    {
        // proceed simulation after waitTime
        currentTime += Time.deltaTime;
        if (!(currentTime > waitTime) || !Globals.State) return;
        
        ProceedSimulation();
        currentTime = 0;
    }
    private void CreateSymulation()
    {
        isIsolation = false;
        SendDLL(Instruction.CreateSimulation.ToInt(), Globals.Parameters.ToDoubleArray(), agentState, Stats, homes);
        if (isolation.isOn)
        {
            SendDLL(Instruction.InitializeHomes.ToInt(), Globals.Parameters.ToDoubleArray(), agentState, Stats, homes);
            OnAddHomes?.Invoke(homes);
            isIsolation = true;
        }
        if (couriers.isOn)
        {
            SendDLL(Instruction.InitializeCouriers.ToInt(), Globals.Parameters.ToDoubleArray(), agentState, Stats, homes);
        }
        if (masks.isOn)
        {
            SendDLL(Instruction.InitializeMasks.ToInt(), Globals.Parameters.ToDoubleArray(), agentState, Stats, homes);
        }
        Globals.IsCleared = false;
    }

    public void ClearSimulation()
    {
        SendDLL(Instruction.DeleteSimulation.ToInt(), Globals.Parameters.ToDoubleArray(), agentState, Stats, homes);   // Clearing simulation
        Globals.State = false;
        Globals.IsCleared = true;
    }

    public void ProceedSimulation()
    {
        if (isIsolation)
        {
            SendDLL(Instruction.ProceedSimulationWithHomes.ToInt(), Globals.Parameters.ToDoubleArray(), agentState, Stats, homes);
        }
        else
        {
            SendDLL(Instruction.ProceedSimulation.ToInt(), Globals.Parameters.ToDoubleArray(), agentState, Stats, homes);
        }
        OnNextStep?.Invoke(agentState, (float)Globals.Parameters.Radius);
        //OnAddHomes?.Invoke(homes);
        OnChartUpdate?.Invoke(Stats);
    }

    public void ChangeParameters()
    {
        SendDLL(Instruction.ChangeParameters.ToInt(), Globals.Parameters.ToDoubleArray(), agentState, Stats, homes);
    }

    public void ChangeSpeed(Single value)
    {
        stepsPerSec = (int) value;
        waitTime = 1.0 / stepsPerSec;
    }

}

