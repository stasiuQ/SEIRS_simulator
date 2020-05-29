using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Seirs.UI.Inputs
{
    public class DtInput : MonoBehaviour
    {
        public TMP_InputField field;

        public void Start()
        {
            field.text = Globals.Parameters.Dt.ToString(); 
        }

        public void Set()
        {
            Globals.ParametersEdited.Dt = double.Parse(field.text);
        }
    }
}