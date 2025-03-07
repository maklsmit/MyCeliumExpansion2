using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomNode : MushroomHolder
{
    public float height = 0;
    // public float[] growthTime = new float[]{15, 10, 20};

    private float startTime;
    public float growth = 0f;
    public float Growth {get => growth;}

    // public int[] VariantSpores = new int[]{5, 10, 15};

    public Variant[] variants;

    private bool initialGrowth = false;

    private GameplayManager gameplayManager;

    public int variant = 0;

    void Start()
    {
        gameplayManager = GameObject.FindWithTag("GameplayManager").GetComponent<GameplayManager>();

        children = new List<MushroomNode>();

        Vector3 pos = transform.position;
        pos.y = Terrain.activeTerrain.SampleHeight(transform.position) + 1.0f;
        transform.position = pos;

        startTime = Time.time;

        UpdateScale();
    }

    // This is the entry point to set what type of mushroom this is, do it right after the Instantiate call
    public void Configure(int variant)
    {
        this.variant = variant;
        for(int i = 0, count = variants.Length; i < count; i++)
        {
            variants[i].visual.SetActive(i == variant);
        }
    }

    void Update()
    {
        if(growth < 1f)
        {
            float deltaTime = Time.time - startTime;
            growth = deltaTime / variants[variant].growthTime;
            if(growth >= 1f)
            {
                growth = 1f;
            }
            UpdateScale();
        }
        else
        {
            if(!initialGrowth)
            {
                initialGrowth = true;
                StartCoroutine(ProduceSpores());
                // Some visual effect about being grown
            }
        }
    }

    private IEnumerator ProduceSpores()
    {
        WaitForSeconds delay = new WaitForSeconds(0.5f);
        while(this.isActiveAndEnabled)
        {
            gameplayManager.AddSpores(variant, variants[variant].sporeProduction); // TODO Replace 0 with the spore variant
            yield return delay;

        }
    }

    public bool Grown()
    {
        return growth >= 1f;
    }

    private void UpdateScale()
    {
        float newScale = Mathf.Pow(Mathf.Lerp(0.25f, 1f, growth), 2);
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }
}

[System.Serializable]
public class Variant
{
    public GameObject visual;
    public float growthTime;
    public int sporeProduction;
}