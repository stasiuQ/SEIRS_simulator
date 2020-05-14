using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Interface_tester
{
    class Program
    {
        [DllImport("COVID_DLL.dll", EntryPoint = "send", CallingConvention = CallingConvention.StdCall)]
        private static extern void SendDLL(bool[] commonInstr, double[] commonParams, double[] commonAgentsState, int[] commonStats);

        static void Main(string[] args)
        {
            int size = 955;
            bool[] instructions = new bool[] {true, false, false, false };
            double[] parameters = new double[] {11, 1, 100, 2, 0.75, 0.4, 2, 0.9, 0.05, 0.005, 0};
            double[] agentState = new double[size];
            int[] stats = new int[] {6, 0, 0, 0, 0, 0};

            SendDLL(instructions, parameters, agentState, stats);   // Initializing simulation
            instructions[0] = false;
            instructions[2] = true;

            for (int i = 0; i < 100; i++)  // proceeding simulation
            {
                SendDLL(instructions, parameters, agentState, stats);
            }

            instructions[2] = false;
            instructions[1] = true;
            SendDLL(instructions, parameters, agentState, stats);

            for (int i = 0; i < 6; i++)
            {
                System.Console.WriteLine(stats[i]);
            }

        }
    }
}
