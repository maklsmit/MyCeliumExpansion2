using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public int spore1 = 0;
    public int spore2 = 0;
    public int spore3 = 0;
    public int spore4 = 0;

    public gameState state;
    private PopUpScreens popUp;

    void Start()
    {
        state = gameState.Level1;

        popUp = GameObject.FindWithTag("PopUps").GetComponent<PopUpScreens>();
    }

    public int GetSpore1()
    {
        return spore1;
    }

    public int GetSpore2()
    {
        return spore2;
    }

    public int GetSpore3()
    {
        return spore3;
    }

    public int GetSpore4()
    {
        return spore4;
    }

    public void SetSpore1(int addition)
    {
        spore1 = addition;
    }

    public void SetSpore2(int addition)
    {
        spore2 = addition;
    }

    public void SetSpore3(int addition)
    {
        spore3 = addition;
    }

    public void SetSpore4(int addition)
    {
        spore4 = addition;
    }

    public void AddSpores(int variant, int count)
    {
        if(variant == 0)
        {
            spore1 += count;
        }
        else if(variant == 1)
        {
            spore2 += count;
        }
        else if(variant == 2)
        {
            spore3 += count;
        }
        else if(variant == 3)
        {
            spore4 += count;
        }
        popUp.UpdateSporeCounter();
    }

    public void IncreaseLevel()
    {
        if (state != gameState.Level4)
        {
            state += 1;
        }
    }

    public enum gameState
    {
        Level1,
        Level2,
        Level3,
        Level4
    }
}

