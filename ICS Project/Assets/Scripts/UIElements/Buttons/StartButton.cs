using UnityEngine;

namespace Seirs.UI.Buttons
{
    public class StartButton : MonoBehaviour
    {
        public void ChangeState()
        {
            Globals.StartMethod();
            Globals.InitChartMethod();
        }
    }
}