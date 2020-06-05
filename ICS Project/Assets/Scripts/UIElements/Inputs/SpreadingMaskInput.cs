using TMPro;
using UnityEngine;

namespace Seirs.UI.Inputs
{
    public class SpreadingMaskInput : MonoBehaviour
    {
        public TMP_InputField field;

        public void Start()
        {
            field.text = Globals.Parameters.SpreadingMask.ToString();
        }

        public void Set()
        {
            Globals.ParametersEdited.SpreadingMask = double.Parse(field.text);
        }
    }
}