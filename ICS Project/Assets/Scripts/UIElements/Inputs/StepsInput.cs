using TMPro;
using UnityEngine;

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
            Globals.StepsEdited = int.Parse(field.text);
        }
    }
}