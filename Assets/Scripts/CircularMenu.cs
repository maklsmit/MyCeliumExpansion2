using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircularMenu : MonoBehaviour
{

    public List<MenuButton> buttons = new List<MenuButton>();
    private Vector2 Mouseposition;
    private Vector2 fromVector2M = new Vector2(0.5f, 1.0f);
    private Vector2 centerCircle = new Vector2(0.5f, 0.5f);
    private Vector2 toVector2M;

    public Transform canvas;

    public int menuItems;
    public int currentMenuItem;
    private int oldMenuItem;

    private Player Player;
    private GameplayManager gameplayManager;

    private int variant = 0;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Player>();
        gameplayManager = GameObject.FindWithTag("GameplayManager").GetComponent<GameplayManager>();
        menuItems = buttons.Count;
        foreach(MenuButton button in buttons){
            button.sceneImage.color = button.NormalColor;
        }
        buttons[0].enabled = true;
        buttons[1].enabled = false;
        buttons[2].enabled = false;
        buttons[3].enabled = false;

        currentMenuItem = 0;
        oldMenuItem = 0;

    }

    // Update is called once per frame
    void Update()
    {   
        if(canvas.gameObject.activeInHierarchy == true){

            getCurrentMenuItem();
            if(Input.GetMouseButtonDown(0)){
                ButtonAction();
            }
            else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)){
                keyAction();
            }

        }

    }

    public void getCurrentMenuItem(){

        Mouseposition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);

        toVector2M = new Vector2 (Mouseposition.x / Screen.width, Mouseposition.y / Screen.height);

        float angle = (Mathf.Atan2(fromVector2M.y-centerCircle.y, fromVector2M.x - centerCircle.x) - Mathf.Atan2(toVector2M.y-centerCircle.y, toVector2M.x - centerCircle.x)) * Mathf.Rad2Deg;

        if(angle < 0){
            angle += 360;
        }

        Debug.Log(angle);

        //currentMenuItem = (int) (angle / (360 / menuItems));

        if((angle >= 0 && angle <= 45) || angle > 315 && angle < 360){

            currentMenuItem = 0;
        }

        else if(angle > 45 && angle <= 135){

            currentMenuItem = 1;

        }

        else if(angle > 135 && angle <= 225){

            currentMenuItem = 2;

        }

        else if(angle > 225 && angle <= 315){

            currentMenuItem = 3;

        }

        if(currentMenuItem != oldMenuItem){

            buttons[oldMenuItem].sceneImage.color = buttons[oldMenuItem].NormalColor;
            oldMenuItem = currentMenuItem;
            buttons[currentMenuItem].sceneImage.color = buttons[currentMenuItem].HighlightedColor;

        }

    }

    public void ButtonAction(){

        buttons[currentMenuItem].sceneImage.color = buttons[currentMenuItem].PressedColor;
        if(currentMenuItem == 0 && buttons[0].enabled){

            print("Option 1");
            variant = 0;
            if (gameplayManager.GetSpore1() >= 200)
            {
                Player.SpawnMushroomFunctionality(variant);
            }
        }
        else if(currentMenuItem == 1 && buttons[1].enabled){

            print("Option 2");
            variant = 1;
            if (gameplayManager.GetSpore2() >= 200)
            {
                Player.SpawnMushroomFunctionality(variant);
            }
        }
        else if(currentMenuItem == 2 && buttons[2].enabled){

            print("Option 3");
            variant = 2;
            if (gameplayManager.GetSpore3() >= 200)
            {
                Player.SpawnMushroomFunctionality(variant);
            }
        }
        else if(currentMenuItem == 3 && buttons[3].enabled){

            print("Option 4");
            variant = 3;
            if (gameplayManager.GetSpore4() >= 200)
            {
                Player.SpawnMushroomFunctionality(variant);
            }
        }

        Pause();
        

    }

    public void keyAction(){

        if(Input.GetKeyDown(KeyCode.W) && buttons[0].enabled){

            buttons[0].sceneImage.color = buttons[0].PressedColor;
            print("Option 1");
            variant = 0;
            if (gameplayManager.GetSpore1() >= 200)
            {
                Player.SpawnMushroomFunctionality(variant);
            }
        }

        else if(Input.GetKeyDown(KeyCode.D) && buttons[1].enabled){

            buttons[1].sceneImage.color = buttons[1].PressedColor;
            print("Option 2");
            variant = 1;
            if (gameplayManager.GetSpore2() >= 200)
            {
                Player.SpawnMushroomFunctionality(variant);
            }
        }

        else if(Input.GetKeyDown(KeyCode.S) && buttons[2].enabled){

            buttons[2].sceneImage.color = buttons[2].PressedColor;
            print("Option 3");
            variant = 2;
            if (gameplayManager.GetSpore3() >= 200)
            {
                Player.SpawnMushroomFunctionality(variant);
            }
        }

        else if(Input.GetKeyDown(KeyCode.A) && buttons[3].enabled){

            buttons[3].sceneImage.color = buttons[3].PressedColor;
            print("Option 4");
            variant = 3;
            if (gameplayManager.GetSpore3() >= 200)
            {
                Player.SpawnMushroomFunctionality(variant);
            }
        }

        Pause();

    }

    public void Pause(){

        if(canvas.gameObject.activeInHierarchy == false){

            canvas.gameObject.SetActive(true);
            Time.timeScale = 0;
            print("Menu open");

        }

        else{

            canvas.gameObject.SetActive(false);
            Time.timeScale = 1;
            print("Menu closed");

        }

    }

}

[System.Serializable]
public class MenuButton
{
    public string name;
    public Image sceneImage;
    public Color NormalColor = Color.white;
    public Color HighlightedColor = Color.grey;
    public Color PressedColor = Color.gray;
    public bool enabled;
}