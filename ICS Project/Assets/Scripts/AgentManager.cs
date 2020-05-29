using Seirs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Seirs
{
    public class AgentManager : MonoBehaviour

    {
        private const int agentSizeIndex = 0;
        [SerializeField] private Agent agentPrefab;
        [SerializeField] private Transform agentsRoot;

        public Agent AgentPrefab { get => agentPrefab; set => agentPrefab = value; }
        public Dictionary<int, Agent> Agents { get; set; }
        public Transform AgentsRoot { get => agentsRoot; set => agentsRoot = value; }

        public void SetAgent(int id, float x, float y, float r, State st)
        {
            Agent agent;
            if(!Agents.TryGetValue(id, out agent))
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
            var id = 0;
            for (var i = 1; i < agentState.Length; i += 3)
            {
                SetAgent(id, (float)agentState[i], (float)agentState[i + 1], r, (State)agentState[i + 2]);
                id++;
            }
        }

        private void Awake()
        {
            Agents = new Dictionary<int, Agent>();
            AgentSimulationManager.OnNextStep += OnNextSimStep;
        }

    }
}
