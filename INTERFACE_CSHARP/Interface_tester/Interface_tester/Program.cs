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
4 - initialize masks
5 - initiazlize couriers
6 - initiazlize homes
7 - proceeding a simulation with homes


Every common tab has a structure like this:
1st element - size of a tab
and others - partcular values

*/



namespace Interface_tester
{
    class Program
    {
        [DllImport("COVID_DLL.dll", EntryPoint = "send", CallingConvention = CallingConvention.Cdecl)]
        private static extern void SendDLL(int commonInstr, double[] commonParams, double[] commonAgentsState, int[] commonStats, double[] commonHomes);

        static void Main(string[] args)
        {
            double[] parameters = new double[] { 18, 1, 100, 2, 0.75, 0.4, 2, 0.9, 0.05, 0.005, 0, 5, 0.5, 0.05, 0.2, 3, 0.5, 0  };

            int agentStateSize = 4 * (int)((parameters[2] * parameters[2] * parameters[5]) / (Math.PI * parameters[3] * parameters[3])) + 1; // 3x number of agents + 1, 955
            double[] agentState = new double[agentStateSize];
            agentState[0] = (double)agentStateSize;

            int sizeHomes = 3 * (int)(parameters[11] * parameters[11]) + 1;  // 3 * (Square of linear zones density, max number of homes in model [number of homes])  + 1 [size fo table]
            double[] homes = new double[sizeHomes];
            homes[0] = (double)sizeHomes;

            int[] stats = new int[] { 6, 0, 0, 0, 0, 0 };   // size, S, E, I, R, step

            int instructions = 0;
            SendDLL(instructions, parameters, agentState, stats, homes);   // Initializing simulation

            instructions = 6;
            SendDLL(instructions, parameters, agentState, stats, homes);   // Initializing simulation

            for (int i = 0; i < sizeHomes; i++)
            {
                Console.WriteLine(homes[i]);
            }

            instructions = 7;

            for (int i = 0; i < 1000; i++)  // proceeding simulation
            {
                SendDLL(instructions, parameters, agentState, stats, homes);
            }

            for (int i = 0; i < sizeHomes; i++)
            {
                Console.WriteLine(homes[i]);
            }

            instructions = 1;

            SendDLL(instructions, parameters, agentState, stats, homes);

            Console.ReadLine();

        }
    }
}
