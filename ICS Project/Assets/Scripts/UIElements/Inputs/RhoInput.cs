using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Seirs.UI.Inputs
{
    public class RhoInput : MonoBehaviour
    {
        public TMP_InputField field;

        public void Start()
        {
            field.text = Globals.Parameters.Rho.ToString(); 
        }

        public void Set()
        {
            Globals.ParametersEdited.Rho = double.Parse(field.text);
        }
    }
}