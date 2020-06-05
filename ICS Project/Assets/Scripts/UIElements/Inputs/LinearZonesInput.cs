using TMPro;
using UnityEngine;

namespace Seirs.UI.Inputs
{
    public class LinearZonesInput : MonoBehaviour
    {
        public TMP_InputField field;

        public void Start()
        {
            field.text = Globals.Parameters.LinearZones.ToString();
        }

        public void Set()
        {
            Globals.ParametersEdited.LinearZones = double.Parse(field.text);
        }
    }
}