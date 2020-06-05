using System.Collections.Generic;
using UnityEngine;

namespace Seirs
{
    public class AgentManager : MonoBehaviour

    {
        private const int agentSizeIndex = 0;
        [SerializeField] private Agent agentPrefab;
        [SerializeField] private Transform agentsRoot;

        public Agent AgentPrefab
        {
            get => agentPrefab;
            set => agentPrefab = value;
        }

        public Dictionary<int, Agent> Agents { get; set; }

        public Transform AgentsRoot
        {
            get => agentsRoot;
            set => agentsRoot = value;
        }

        public void SetAgent(int id, float x, float y, float r, State st)
        {
            Agent agent;
            if (!Agents.TryGetValue(id, out agent))
            {
                agent = Instantiate(agentPrefab, agentsRoot);
                Agents.Add(id, agent);
                agent.Id = id;
            }

            agent.SetPosition(x, y);
            agent.SetRadius(r);
            agent.SetColor(st);
        }

        public void OnNextSimStep(double[] agentState, float r)
        {
            //Debug.Log(agentState.Length);
            //Debug.Log( string.Join("/",agentState));
            var id = 0;
            for (var i = 1; i < agentState.Length; i += 3)
            {
                SetAgent(id, (float) agentState[i], (float) agentState[i + 1], r, (State) agentState[i + 2]);
                id++;
            }
        }

        public void HidePoints(double[] age)
        {
        }

        public void DestroyAllAgents()
        {
            foreach (var agent in Agents) Destroy(agent.Value.gameObject);
            Agents.Clear();
        }

        private void Awake()
        {
            Agents = new Dictionary<int, Agent>();
            AgentSimulationManager.OnNextStep += OnNextSimStep;
            AgentSimulationManager.OnDestroyAgents += DestroyAllAgents;
        }
    }
}