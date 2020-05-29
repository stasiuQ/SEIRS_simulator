using UnityEngine;
using UnityEngine.UI;

namespace Seirs.UI.Buttons
{
    public class ClearButton : MonoBehaviour {

        public void Clear()
        {
            Globals.ClearMethod();
        }
    }
}