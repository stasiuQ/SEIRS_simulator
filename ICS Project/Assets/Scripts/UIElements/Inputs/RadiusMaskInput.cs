using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Seirs.UI.Inputs
{
    public class RadiusMaskInput : MonoBehaviour
    {
        public TMP_InputField field;

        public void Start()
        {
            field.text = Globals.Parameters.RadiusMask.ToString(); 
        }

        public void Set()
        {
            Globals.ParametersEdited.RadiusMask = double.Parse(field.text);
        }
    }
}