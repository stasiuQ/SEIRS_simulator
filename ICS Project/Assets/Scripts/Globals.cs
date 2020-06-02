using Seirs.Models;
using Seirs.Services;

namespace Seirs{
public static class Globals{
    public static bool State = false;
    public static bool IsCleared = true;
    public static int BatchSize = 100;
    public static int Steps = 1000;
    
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
    
    public static IGraphService GraphService { get; set; } = new GraphService();
    
    //create Delegats
    public delegate void StartDelegate();
    public delegate void ClearDeletgate();

    public delegate void DrawChart(DrawData drawData);

    //definitions delegats
    public static StartDelegate StartMethod;
    public static ClearDeletgate ClearMethod;

    public static DrawChart DrawChartMethod;
}
}
