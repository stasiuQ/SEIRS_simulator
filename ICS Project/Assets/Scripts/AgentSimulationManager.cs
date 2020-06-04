using Seirs;
using Seirs.Models;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
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
    [SerializeField] public InputField steps;
	
    public static event Action<double[], float> OnNextStep;
    // public static event Action<int[]> OnNextStepChart;
    public static event Action OnDestroyAgents;
    public static event Action<double[]> OnAddHomes;
    public static event Action OnDestroyHomes;
    public static event Action<int[]> OnChartUpdate;
    public int StepsPerSec { get => stepsPerSec; set { stepsPerSec = value; waitTime = 1.0 / stepsPerSec; }}
    public int AgentsCount { get => agentsCount; set { agentsCount = value; size = (agentsCount * 3) + 1; } }
    public Transform Plane { get => plane; set => plane = value; }
    public Toggle Isolation { get => isolation; set => isolation = value; }
    public Toggle Couriers { get => couriers; set => couriers = value; }
    public Toggle Masks { get => masks; set => masks = value; }

    private int agentsCount;
    private int size = 0;
    private double[] agentState;
    private int stepsPerSec = 20;
    private double waitTime;
    private double currentTime=0;
    private double[] homes;
    private bool isIsolation;
    public int simulationStep = 0;
    private int homesSize = 0;

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
        waitTime = 1.0/ stepsPerSec;
        SetPlaneScale();
    }

    private void SetPlaneScale()
    {
        var scale = (float)Globals.Parameters.Size / 10f + 0.5f;
        plane.localScale = new Vector3(scale, 1, scale);
    }

    public void StartPauseSimulation()
    {
        Globals.Parameters = Globals.ParametersEdited;
        Globals.Steps = Globals.StepsEdited;

//        Debug.Log("StartPauseSimulation");
        if(Globals.IsCleared)
        {    
            CreateSymulation();
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

        if (simulationStep < Globals.Steps)
        {
            ProceedSimulation();
            simulationStep++;
        }

        // await Task.Delay(10);
        currentTime = 0;
    }
    private void CreateSymulation()
    {
        homesSize = (int)Globals.Parameters.LinearZones * (int)Globals.Parameters.LinearZones * 3 + 1;
        homes = new double[homesSize];
        homes[0] = homesSize;
        size = 3 * (int)((Globals.Parameters.Size * Globals.Parameters.Size * Globals.Parameters.Concentration) / (Math.PI * Globals.Parameters.Radius * Globals.Parameters.Radius)) + 1;
        agentState = new double[size];
        agentState[0] = size;

        isIsolation = false;
        SendDLL(Instruction.CreateSimulation.ToInt(), Globals.Parameters.ToDoubleArray(), agentState, Globals.Stats, homes);
        if (isolation.isOn)
        {
            SendDLL(Instruction.InitializeHomes.ToInt(), Globals.Parameters.ToDoubleArray(), agentState, Globals.Stats , homes);
            OnAddHomes?.Invoke(homes);
            isIsolation = true;
        }
        if (couriers.isOn)
        {
            SendDLL(Instruction.InitializeCouriers.ToInt(), Globals.Parameters.ToDoubleArray(), agentState, Globals.Stats, homes);
        }
        if (masks.isOn)
        {
            SendDLL(Instruction.InitializeMasks.ToInt(), Globals.Parameters.ToDoubleArray(), agentState, Globals.Stats, homes);
        }
		
		isolation.enabled = false;
        couriers.enabled = false;
        masks.enabled = false;
        Globals.IsCleared = false;
    }

    public void ClearSimulation()
    {
        SendDLL(Instruction.DeleteSimulation.ToInt(), Globals.Parameters.ToDoubleArray(), agentState, Globals.Stats, homes);   // Clearing simulation
        OnDestroyAgents?.Invoke();
        if (isIsolation)
        {
            OnDestroyHomes?.Invoke();
        }
        Globals.CurrentStep = 0;
        Globals.State = false;
        Globals.IsCleared = true;
        isolation.enabled = true;
        couriers.enabled = true;
        masks.enabled = true;
        simulationStep = 0;

    }

    public void ProceedSimulation()
    {
        if (isIsolation)
        {
            SendDLL(Instruction.ProceedSimulationWithHomes.ToInt(), Globals.Parameters.ToDoubleArray(), agentState, Globals.Stats, homes);
        }
        else
        {
            SendDLL(Instruction.ProceedSimulation.ToInt(), Globals.Parameters.ToDoubleArray(), agentState, Globals.Stats, homes);
        }


        Globals.UpdateChartMethod();

        OnNextStep?.Invoke(agentState, (float)Globals.Parameters.Radius);
        // OnChartUpdate?.Invoke(Globals.Stats);
        //OnNextStepChart?.Invoke(Globals.Stats);
    }

    public void ChangeParameters()
    {
        SendDLL(Instruction.ChangeParameters.ToInt(), Globals.Parameters.ToDoubleArray(), agentState, Globals.Stats, homes);
    }

    public void ChangeSpeed(Single value)
    {
        stepsPerSec = (int) value;
        waitTime = 1.0 / stepsPerSec;
    }

}

