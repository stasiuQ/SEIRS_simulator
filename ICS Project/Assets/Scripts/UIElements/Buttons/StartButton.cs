using UnityEngine;
using UnityEngine.UI;

namespace Seirs.UI.Buttons
{
    public class StartButton : MonoBehaviour {

        public void ChangeState()
        {
            Globals.StartMethod();
        }

    }
}