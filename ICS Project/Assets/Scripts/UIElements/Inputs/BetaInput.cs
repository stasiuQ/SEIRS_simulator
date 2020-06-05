using TMPro;
using UnityEngine;

namespace Seirs.UI.Inputs
{
    public class BetaInput : MonoBehaviour
    {
        public TMP_InputField field;

        public void Start()
        {
            field.text = Globals.Parameters.Beta.ToString();
        }

        public void Set()
        {
            Globals.ParametersEdited.Beta = double.Parse(field.text);
        }
    }
}