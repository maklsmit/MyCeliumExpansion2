using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GameplayTree : MonoBehaviour
{
    public GameObject normal;
    public GameObject corrupt;
    public GameObject log;

    public VisualEffect dust;

    private int captureCount = 0;
    private int captureVariety = 0;

    public int state = 0; // 0 = normal, 1 = corrupted, 2 = log
    public float captureTime = 10f;
    public float logTime = 10f;

    public float fallTime = 1f;

    public int sporeReward = 200;

    void Start()
    {
        UpdateState();
    }

    public void AddCapture(int captureVariety)
    {
        captureCount++;
        this.captureVariety = captureVariety;

        if (captureVariety == 0)
        {
            captureTime = 10f;
            logTime = 10f;
            fallTime = 1f;
        } else if (captureVariety == 1)
        {
            captureTime = 30f;
            logTime = 30f;
            fallTime = 1f;
        } else if (captureVariety == 2)
        {
            captureTime = 4f;
            logTime = 4f;
            fallTime = 1f;
        } else {
            captureTime = 5f;
            logTime = 5f;
            fallTime = 1f;
        }
        
        if (captureCount == 1)
        {
            StartCoroutine(Capture());
        }
    }

    public void Uncapture()
    {
        captureCount--;
    }

    private void UpdateState()
    {
        normal.SetActive(state == 0);
        corrupt.SetActive(state == 1);
        log.SetActive(state == 2);
    }

    private IEnumerator Capture()
    {
        if(state == 0)
        {
            yield return new WaitForSeconds(captureTime);
            state = 1;
            UpdateState();
        }

        if(captureCount != 0)
        {
            yield return new WaitForSeconds(logTime);
            state = 2;
            GetComponent<Collider>().enabled = false;
            StartCoroutine(Collapse());
        }
    }

    private IEnumerator Collapse()
    {
        GetComponent<BaseAudioManager>().Play("Fall");
        // rotate log down
        Vector3 originalRotation = corrupt.transform.rotation.eulerAngles;
        Vector3 position = corrupt.transform.position;

        float timeElapsed = 0f;
        while(timeElapsed < fallTime)
        {
            timeElapsed += Time.deltaTime;
            corrupt.transform.SetPositionAndRotation(position, Quaternion.Euler(new Vector3(originalRotation.x, originalRotation.y, Mathf.Lerp(0f, -90f, timeElapsed / fallTime))));
            yield return null;
        }

        UpdateState();
        dust.Play();
        GameObject.FindGameObjectWithTag("GameplayManager").GetComponent<GameplayManager>().AddSpores(captureVariety, sporeReward);
        // play particle effect
    }
}
