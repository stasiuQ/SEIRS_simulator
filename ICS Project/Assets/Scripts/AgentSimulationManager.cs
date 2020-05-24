using Seirs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AgentSimulationManager : MonoBehaviour
{
    [DllImport("COVID_DLL.dll", EntryPoint = "send", CallingConvention = CallingConvention.Cdecl)]
    private static extern void SendDLL(int commonInstr, double[] commonParams, double[] commonAgentsState, int[] commonStats);
    [SerializeField] private Transform plane;

    public static event Action<double[], float> OnNextStep;
    public static event Action<int[]> OnChartUpdate;

    public double[] Parameters { get => parameters; set { parameters = value; SetPlaneScale(); } }
    public int[] Stats { get => stats; private set => stats = value; }
    public int StepsPerSec { get => stepsPerSec; set { stepsPerSec = value; waitTime = 1.0 / stepsPerSec; }}
    public int AgentsCount { get => agentsCount; set { agentsCount = value; size = (agentsCount * 3) + 1; } }
    public Transform Plane { get => plane; set => plane = value; }

    private int agentsCount;
    private const int radiusIndex = 3;
    private double[] parameters = { 11, 1, 100, 2, 0.75, 0.4, 2, 0.9, 0.05, 0.005, 0 };
    private int size = 955;
    private double[] agentState;
    private int[] stats = { 6, 0, 0, 0, 0, 0 };
    private bool doRun = false;
    private int stepsPerSec = 20;
    private double waitTime;
    private double currentTime=0;

    private void Awake()
    {
        agentState = new double[size];
        waitTime = 1.0/ stepsPerSec;
        SetPlaneScale();
    }

    private void SetPlaneScale()
    {
        var scale = (float)parameters[2] / 10f + 0.5f;
        plane.localScale = new Vector3(scale, 1, scale);
    }

    public void PauseSimulation()
    {
        doRun = false;
    }

    public void ContinueSimulation()
    {
        doRun = true;
    }

    private void Start()
    {
        StartSimulation();
    }
    private void Update()
    {
        // proceed simulation after waitTime
        currentTime += Time.deltaTime;
        if (currentTime > waitTime && doRun)
        {
            ProceedSimulation();
            currentTime = 0;
        }
    }

    public void StartSimulation()
    {
        SendDLL((int)Instruction.CreateSimulation, parameters, agentState, stats);   // Initializing simulation
        doRun = true;
    }

    public void ProceedSimulation()
    {
        SendDLL((int)Instruction.ProceedSimulation, parameters, agentState, stats);
        OnNextStep?.Invoke(agentState, (float)parameters[radiusIndex]);
        OnChartUpdate?.Invoke(stats);
    }
    public enum Instruction
    {
        CreateSimulation = 0,
        DeleteSimulation = 1,
        ProceedSimulation = 2,
        ChangeParameters = 3
    }
}
