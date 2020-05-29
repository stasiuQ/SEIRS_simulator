﻿using Seirs;
using Seirs.Models;
using Seirs.UI.Inputs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class AgentSimulationManager : MonoBehaviour
{
    [DllImport("COVID_DLL.dll", EntryPoint = "send", CallingConvention = CallingConvention.Cdecl)]
    private static extern void SendDLL(int commonInstr, double[] commonParams, double[] commonAgentsState, int[] commonStats);
    [SerializeField] private Transform plane;

    public static event Action<double[], float> OnNextStep;
    public static event Action<int[]> OnChartUpdate;

    public int[] Stats { get; set;} = { 6, 0, 0, 0, 0, 0 };
    public int StepsPerSec { get => stepsPerSec; set { stepsPerSec = value; waitTime = 1.0 / stepsPerSec; }}
    public int AgentsCount { get => agentsCount; set { agentsCount = value; size = (agentsCount * 3) + 1; } }
    public Transform Plane { get => plane; set => plane = value; }

    private int agentsCount;
    private int size = 955;
    private double[] agentState;
    private int stepsPerSec = 20;
    private double waitTime;
    private double currentTime=0;

    private void Start()
    {
        SetDelegates();
    }

    private void SetDelegates()
    {
        Globals.StartMethod = StartPauseSimulation;
        Globals.ClearMethod = ClearSimulation;
        //Globals.PauseMethod = PauseSimulation;
    }

    private void Awake()
    {
        agentState = new double[size];
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
        Debug.Log("StartPauseSimulation");
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
        if (currentTime > waitTime && Globals.State)
        {
            ProceedSimulation();
            currentTime = 0;
        }
    }
    private void CreateSymulation()
    {
        SendDLL(Instruction.CreateSimulation.ToInt(), Globals.Parameters.ToDoubleArray(), agentState, Stats);
        Globals.IsCleared = false;
    }

    public void ClearSimulation()
    {
        SendDLL(Instruction.DeleteSimulation.ToInt(), Globals.Parameters.ToDoubleArray(), agentState, Stats);   // Clearing simulation
        Globals.State = false;
        Globals.IsCleared = true;
    }

    public void ProceedSimulation()
    {
        
        SendDLL(Instruction.ProceedSimulation.ToInt(), Globals.Parameters.ToDoubleArray(), agentState, Stats);
        OnNextStep?.Invoke(agentState, (float)Globals.Parameters.Radius);
        OnChartUpdate?.Invoke(Stats);
    }

    public void ChangeParameters()
    {
        SendDLL(Instruction.ChangeParameters.ToInt(), Globals.Parameters.ToDoubleArray(), agentState, Stats);
    }

}

