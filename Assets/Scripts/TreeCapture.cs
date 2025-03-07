using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCapture : MonoBehaviour
{
    private List<GameplayTree> capturedTrees = new List<GameplayTree>();

    public GameObject parent;

    private int variant = 0;

    void Start()
    {
        variant = parent.GetComponent<MushroomNode>().variant;

        if (variant == 0)
        {
            GetComponent<SphereCollider>().radius = 2.5f;
        } else if (variant == 1)
        {
            GetComponent<SphereCollider>().radius = 5f;
        } else if(variant == 2) 
        {
            GetComponent<SphereCollider>().radius = 1f;
        } else {
            GetComponent<SphereCollider>().radius = 3f;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Tree"))
        {
            GameplayTree otherTree = collider.GetComponent<GameplayTree>();
            otherTree.AddCapture(variant); // TODO replace 0 with mushroom variety
        }
    }

    private void OnDestroyed()
    {
        foreach(GameplayTree tree in capturedTrees)
        {
            tree.Uncapture();
        }
    }
}
