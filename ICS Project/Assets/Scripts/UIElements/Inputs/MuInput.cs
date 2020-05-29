using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Seirs.UI.Inputs
{
    public class MuInput : MonoBehaviour
    {
        public TMP_InputField field;

        public void Start()
        {
            field.text = Globals.Parameters.Mu.ToString(); 
        }

        public void Set()
        {
            Globals.ParametersEdited.Mu = double.Parse(field.text);
        }
    }
}