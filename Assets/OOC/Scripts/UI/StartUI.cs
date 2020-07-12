using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace OOC.UI
{
    public class StartUI : MonoBehaviour
    {
        public Button CloseButton;

        private void Start()
        {
            //CloseButton.clicked += CloseButton_clicked;
        }

        public void CloseButton_clicked()
        {

            LoadLevel();
        }

        public void LoadLevel()
        {
            SceneManager.LoadScene(1);
        }
    }
}

