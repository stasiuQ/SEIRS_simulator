using TMPro;
using UnityEngine;

namespace Seirs.UI.Inputs
{
    public class ProbWearingMaskInput : MonoBehaviour
    {
        public TMP_InputField field;

        public void Start()
        {
            field.text = Globals.Parameters.ProbWearingMask.ToString();
        }

        public void Set()
        {
            Globals.ParametersEdited.ProbWearingMask = double.Parse(field.text);
        }
    }
}