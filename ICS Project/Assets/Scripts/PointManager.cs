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

        public void OnNextPoint(int[] stats)
        {
            //Debug.Log(agentState.Length);
            var id = 0;
            var r = 2;
            for (var i = 1; i < 5; i ++)
            {
                SetPoint(id, (float)stats[5], stats[i], r, (State)stats[i-1]);
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
            // AgentSimulationManager.OnNextStepChart += OnNextPoint;
            AgentSimulationManager.OnDestroyAgents += DestroyAllPoints;
        }
    }
}
