using UnityEngine;
using UnityEngine.UIElements;

namespace UIElements.Buttons
{
    public class QuitButton : MonoBehaviour
    {
        private Button quitButton;
        void Start()
        {
            quitButton = GetComponent<Button>();
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}