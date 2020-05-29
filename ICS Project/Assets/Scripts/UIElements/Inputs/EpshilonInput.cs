using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Seirs.UI.Inputs
{
    public class EpshilonInput : MonoBehaviour
    {
        public TMP_InputField field;

        public void Start()
        {
            field.text = Globals.Parameters.Epshilon.ToString(); 
        }

        public void Set()
        {
            Globals.ParametersEdited.Epshilon = double.Parse(field.text);
        }
    }
}