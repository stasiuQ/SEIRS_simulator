using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Seirs
{
    public class PointManager : MonoBehaviour
    {
        private const int pointSizeIndex = 0;
        [SerializeField] private Agent pointPrefab;
        [SerializeField] private Transform pointsRoot;
        public Agent PointPrefab { get => pointPrefab; set => pointPrefab = value; }
        public Dictionary<int, Agent> Points { get; set; }
        public Transform PointsRoot { get => pointsRoot; set => pointsRoot = value; }

        public void SetPoint(int id, float x, float y, float r, State st)
        {
            Agent point;
            if (!Points.TryGetValue(id, out point))
            {
                point = Instantiate(pointPrefab, pointsRoot);
                Points.Add(id, point);
                point.Id = id;
            }
            point.SetPosition(x, y);
            point.SetRadius(r);
            point.SetColor(st);
        }

        public void OnNextPoint(double[] agentState, float r)
        {
            //Debug.Log(agentState.Length);
            var id = 0;
            for (var i = 1; i < agentState.Length; i += 3)
            {
                SetPoint(id, (float)agentState[i], (float)agentState[i + 1], r, (State)agentState[i + 2]);
                id++;
            }
        }

        public void HidePoints(double[] age)
        {

        }

        public void DestroyAllPoints()
        {
            foreach (var p in Points)
            {
                Destroy(p.Value.gameObject);
            }
            Points.Clear();
        }

        private void Awake()
        {
            Points = new Dictionary<int, Agent>();
            AgentSimulationManager.OnNextStep += OnNextPoint;
            AgentSimulationManager.OnDestroyAgents += DestroyAllPoints;
        }
    }
}
