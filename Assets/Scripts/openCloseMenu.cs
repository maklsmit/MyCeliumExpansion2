using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openCloseMenu : MonoBehaviour
{

    public Transform canvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Tab)){

            Pause();

        }
        
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
