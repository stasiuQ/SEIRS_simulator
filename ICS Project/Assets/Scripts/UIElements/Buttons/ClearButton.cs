using Seirs;
using UnityEngine;

namespace UIElements.Buttons
{
    public class ClearButton : MonoBehaviour {

        public void Clear()
        {
            Globals.ClearMethod();
        }
    }
}