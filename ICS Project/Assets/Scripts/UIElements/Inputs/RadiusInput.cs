using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Seirs.UI.Inputs
{
    public class RadiusInput : MonoBehaviour
    {
        public TMP_InputField field;

        public void Start()
        {
            field.text = Globals.Parameters.Radius.ToString(); 
        }

        public void Set()
        {
            Globals.ParametersEdited.Radius = double.Parse(field.text);
        }
    }
}