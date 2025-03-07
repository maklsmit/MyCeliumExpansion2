using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomRoot : MushroomHolder
{
    public MushroomNode rootShroom;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public abstract class MushroomHolder : MonoBehaviour
{
    public List <MushroomNode> children;
}
