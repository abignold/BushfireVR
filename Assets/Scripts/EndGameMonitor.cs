using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameMonitor : MonoBehaviour
{

    private GroundFire[] fires;
    private int firesRemaining = 0;
    private bool endGame = false;
    private float endGameWaitTime = 1f;
    private float endGameAtTime = 0;


    void Start()
    {
        fires = FindObjectsOfType<GroundFire>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!endGame)
        {
            endGame = checkFires();
            if (endGame)
            {
                AudioSource win = GetComponent<AudioSource>();
                win.Play();
                this.endGameAtTime = Time.time + endGameWaitTime;
            }
        }

        if (endGame)
        {
            doEndGameUpdate();
        }


    }

    private void doEndGameUpdate()
    {
        if (Time.time > (this.endGameAtTime))
        {
            SceneManager.LoadScene("MenuTestScene", LoadSceneMode.Single);
        }
    }

    bool checkFires()
    {
        int firesLit = fires.Length;
        foreach (GroundFire fire in fires)
        {
            if (fire.isFireStopped)
            {
                firesLit--;
            }
        }

        firesRemaining = firesLit;
        return firesRemaining == 0;
    }
}
