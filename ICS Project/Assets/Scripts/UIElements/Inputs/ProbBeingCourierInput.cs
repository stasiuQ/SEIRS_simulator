using TMPro;
using UnityEngine;

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