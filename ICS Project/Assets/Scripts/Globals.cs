using Seirs.Models;

namespace Seirs{
public static class Globals{
    public static bool State = false;
    public static bool IsCleared = true;
    public static int Steps = 5000;
    public static int AgentNumbers = 320;
    public static int[] Stats { get; set;} = { 6, 0, 0, 0, 0, 0 };
    public static Parameters Parameters = new Parameters
    {
        Radius = 2,
        Mobility = 0.75,
        Concentration = 0.4,
        Infected = 2,
        Beta = 0.9,
        Epsilon = 0.05,
        Mu = 0.005,
        Rho = 0,
        LinearZones = 5,
        ProbWearingMask = 0.5, 
        ProbBeingCourier = 0.05, 
        ProbHomeInZone = 0.2, 
        MobilityCouriers = 3, 
        RadiusMask = 0.5, 
        SpreadingMask = 0
    };

    public static Parameters ParametersEdited = new Parameters
    {
        Radius = 2,
        Mobility = 0.75,
        Concentration = 0.4,
        Infected = 2,
        Beta = 0.9,
        Epsilon = 0.05,
        Mu = 0.005,
        Rho = 0,
        LinearZones = 5,
        ProbWearingMask = 0.5, 
        ProbBeingCourier = 0.05, 
        ProbHomeInZone = 0.2, 
        MobilityCouriers = 3, 
        RadiusMask = 0.5, 
        SpreadingMask = 0
    };

    //create Delegats
    public delegate void StartDelegate();
    public delegate void ClearDelegate();
    public delegate void UpdateChart(int[] stats);
    public delegate void ClearChart();

    //definitions delegats
    public static StartDelegate StartMethod;
    public static ClearDelegate ClearMethod;
    public static UpdateChart UpdateChartMethod;
    public static ClearChart ClearChartMethod;
}
}
