using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    private bool spacePressed = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!spacePressed)
            {
                spacePressed = true;
                SceneManager.LoadSceneAsync("GameplayScene");
            }
        }        
    }
}
