using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/*
Instruction for int instructions:
0 - creating a simulation
1 - deleting a simulation
2 - proceeding a simulation
3 - changing parameters


Every common tab has a structure like this:
1st element - size of a tab
and others - partcular values

*/



namespace Interface_tester
{
    class Program
    {
        [DllImport("COVID_DLL.dll", EntryPoint = "send", CallingConvention = CallingConvention.Cdecl)]
        private static extern void SendDLL(int commonInstr, double[] commonParams, double[] commonAgentsState, int[] commonStats);

        static void Main(string[] args)
        {
            int size = 955;   // 3x number of agents + 1, to be calculated from concentration.
            int instructions = 0;
            double[] parameters = new double[] { 11, 1, 100, 2, 0.75, 0.4, 2, 0.9, 0.05, 0.005, 0 };
            double[] agentState = new double[size];
            int[] stats = new int[] { 6, 0, 0, 0, 0, 0 };   // size, S, E, I, R, step

            SendDLL(instructions, parameters, agentState, stats);   // Initializing simulation

            instructions = 2;

            for (int i = 0; i < 100; i++)  // proceeding simulation
            {
                SendDLL(instructions, parameters, agentState, stats);
            }

            instructions = 1;

            SendDLL(instructions, parameters, agentState, stats);

            for (int i = 0; i < 6; i++)
            {
                System.Console.WriteLine(stats[i]);
            }
        }
    }
}
