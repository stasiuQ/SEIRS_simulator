using Seirs;
using Seirs.Models;
using UnityEngine;

namespace UIElements.Buttons
{
    public class ClearButton : MonoBehaviour {

        public void Clear()
        {
            if (Globals.Steps < 750)
            {
                Globals.MaxPointDraw = Globals.Steps;
            }
            else
            {
                Globals.MaxPointDraw = 750;
            }
            Globals.ClearMethod();
            Globals.ClearChartMethod();
            //DrawData.GetInstance.clearData();
        }
    }
}