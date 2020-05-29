using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Seirs.UI.Inputs
{
    public class MobilityInput : MonoBehaviour
    {
        public TMP_InputField field;

        public void Start()
        {
            field.text = Globals.Parameters.Mobility.ToString(); 
        }

        public void Set()
        {
            Globals.ParametersEdited.Mobility = double.Parse(field.text);
        }
    }
}