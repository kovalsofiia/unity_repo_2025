using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public InputField usernameInput;
    public Button startButton;
    public Text warningText;


    void Start()
    {
        warningText.text = "";
        startButton.onClick.AddListener(OnStartClicked);
    }

    public void OnStartClicked()
    {
        string username = usernameInput.text.Trim();

        if (string.IsNullOrEmpty(username))
        {
            warningText.text = "Please enter your login.";
        }
        else
        {
            PlayerPrefs.SetString("username", username);
            Debug.Log("Clicked");
            SceneManager.LoadScene("MenuLab5");
        }
    }
}
