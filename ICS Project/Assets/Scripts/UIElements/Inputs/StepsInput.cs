using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Seirs.UI.Inputs
{
    public class StepsInput : MonoBehaviour
    {
        public TMP_InputField field;

        public void Start()
        {
            field.text = Globals.Steps.ToString(); 
        }

        public void Set()
        {
            Globals.Steps = int.Parse(field.text);
        }
    }
}