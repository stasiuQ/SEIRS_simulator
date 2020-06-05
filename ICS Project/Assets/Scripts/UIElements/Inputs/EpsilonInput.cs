using TMPro;
using UnityEngine;

namespace Seirs.UI.Inputs
{
    public class EpsilonInput : MonoBehaviour
    {
        public TMP_InputField field;

        public void Start()
        {
            field.text = Globals.Parameters.Epsilon.ToString();
        }

        public void Set()
        {
            Globals.ParametersEdited.Epsilon = double.Parse(field.text);
        }
    }
}