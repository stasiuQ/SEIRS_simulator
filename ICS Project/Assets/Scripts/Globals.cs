using Seirs.Models;
using Seirs.Services;

namespace Seirs{
public static class Globals{
    public static bool State = false;
    public static bool IsCleared = true;

    /// <summary>
    /// How many data will show on chart.
    /// </summary>
    public static int BatchSize = 100;

    // public static double[] Parameters = { 11, 1, 100, 2, 0.75, 0.4, 2, 0.9, 0.05, 0.005, 0 };
    public static Parameters Parameters = new Parameters
    {
        Dt = 1,
        Infected = 2,
        Radius = 0.75,
        Mobility = 0.4,
        Concentration = 2,
        Beta = 0.9,
        Epshilon = 0.05,
        Mu = 0.005,
        Rho = 0
    };

    public static Parameters ParametersEdited = new Parameters
    {
        Dt = 1,
        Infected = 2,
        Radius = 0.75,
        Mobility = 0.4,
        Concentration = 2,
        Beta = 0.9,
        Epshilon = 0.05,
        Mu = 0.005,
        Rho = 0
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
