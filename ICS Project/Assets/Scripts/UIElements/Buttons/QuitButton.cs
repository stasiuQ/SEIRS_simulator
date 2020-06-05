using UnityEngine;
using UnityEngine.UIElements;

namespace UIElements.Buttons
{
    public class QuitButton : MonoBehaviour
    {
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}