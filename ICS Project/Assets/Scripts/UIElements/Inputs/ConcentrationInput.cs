using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Seirs.UI.Inputs
{
    public class ConcentrationInput : MonoBehaviour
    {
        public TMP_InputField field;

        public void Start()
        {
            field.text = Globals.Parameters.Concentration.ToString(); 
        }

        public void Set()
        {
            Globals.ParametersEdited.Concentration = double.Parse(field.text);
        }
    }
}