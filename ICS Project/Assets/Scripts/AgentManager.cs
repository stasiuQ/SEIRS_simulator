using Seirs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Seirs
{
    public class AgentManager : MonoBehaviour
    {
        [SerializeField] private Agent agentPrefab;
        public Agent AgentPrefab { get => agentPrefab; set => agentPrefab = value; }
        public Dictionary<int, Agent> Agents { get; set; }

        public void SetAgent(int id, float x, float y, float r, State st)
        {
            Agent agent;
            if(!Agents.TryGetValue(id, out agent))
            {
                agent = Instantiate(agentPrefab);    
                Agents.Add(id, agent);   
                agent.Id = id;
            }
            agent.SetPosition(x, y);
            agent.SetRadius(r);
            agent.SetColor(st);
        }

        private void Awake()
        {
            Agents = new Dictionary<int, Agent>();
        }

        private void Start()
        {
            SetAgent(1, 0, 0, 1, State.S);
            SetAgent(2, 1, 1, 1.5f, State.E);
            SetAgent(3,-2, -2, 1, State.I);
        }
    }
}
