using Seirs;
using Seirs.Models;
using UnityEngine;

namespace UIElements.Buttons
{
    public class ClearButton : MonoBehaviour {

        public void Clear()
        {
            Globals.ClearMethod();
            Globals.ClearChartMethod();
            //DrawData.GetInstance.clearData();
        }
    }
}