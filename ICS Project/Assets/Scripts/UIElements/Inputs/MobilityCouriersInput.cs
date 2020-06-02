using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Seirs.UI.Inputs
{
    public class MobilityCouriersInput : MonoBehaviour
    {
        public TMP_InputField field;

        public void Start()
        {
            field.text = Globals.Parameters.MobilityCouriers.ToString(); 
        }

        public void Set()
        {
            Globals.ParametersEdited.MobilityCouriers = double.Parse(field.text);
        }
    }
}