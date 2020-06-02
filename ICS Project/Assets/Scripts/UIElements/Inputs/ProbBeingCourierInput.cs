using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Seirs.UI.Inputs
{
    public class ProbBeingCourierInput : MonoBehaviour
    {
        public TMP_InputField field;

        public void Start()
        {
            field.text = Globals.Parameters.ProbBeingCourier.ToString(); 
        }

        public void Set()
        {
            Globals.ParametersEdited.ProbBeingCourier = double.Parse(field.text);
        }
    }
}