using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Seirs.UI.Inputs
{
    public class InfectedInput : MonoBehaviour
    {
        public TMP_InputField field;

        public void Start()
        {
            field.text = Globals.Parameters.Infected.ToString(); 
        }

        public void Set()
        {
            Globals.ParametersEdited.Infected = double.Parse(field.text);
        }
    }
}