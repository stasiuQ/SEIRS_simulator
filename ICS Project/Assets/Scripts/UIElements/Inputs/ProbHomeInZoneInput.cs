using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Seirs.UI.Inputs
{
    public class ProbHomeInZoneInput : MonoBehaviour
    {
        public TMP_InputField field;

        public void Start()
        {
            field.text = Globals.Parameters.ProbHomeInZone.ToString(); 
        }

        public void Set()
        {
            Globals.ParametersEdited.ProbHomeInZone = double.Parse(field.text);
        }
    }
}