using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeManager : MonoBehaviour
{
    private const int homeSizeIndex = 0;
    [SerializeField] private Home homePrefab;
    [SerializeField] private Transform homesRoot;

    public Home HomePrefab { get => homePrefab; set => homePrefab = value; }
    public Dictionary<int, Home> Homes { get; set; }
    public Transform HomesRoot { get => homesRoot; set => homesRoot = value; }

    public void SetHome(int id, float x, float y, float r)
    {
        Home home;
        if (!Homes.TryGetValue(id, out home))
        {
            home = Instantiate(homePrefab, homesRoot);
            Homes.Add(id, home);
            home.Id = id;
        }
        home.SetPosition(x, y);
        home.SetRadius(r);
    }

    public void OnAddIsolatingHomes(double[] homeState)
    {
        //Debug.Log(agentState.Length);
        var id = 0;
        for (var i = 1; i < homeState.Length; i += 3)
        {
            Debug.Log((float)homeState[i + 2]);
            SetHome(id, (float)homeState[i], (float)homeState[i + 1], (float)homeState[i + 2]);
            id++;
        }
    }

    public void HidePoints(double[] age)
    {

    }

    private void Awake()
    {
        Homes = new Dictionary<int, Home>();
        AgentSimulationManager.OnAddHomes += OnAddIsolatingHomes;
    }
}
