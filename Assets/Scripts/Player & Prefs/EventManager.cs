﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour {

    // Flag for current level
    private bool[] status = { true, false, false, false, false, false, false, false, false };
    private int currentLevel = 0;
    public int numberOfCarps;
    public GameObject player;
    public GameObject playerSimulator;

    void Start () {

        // Set up level selections to unselected
        // Option A = 0, option B = 1, option Default = 2
        // -1 is unassigned
        PlayerPrefs.SetInt("Level1", -1);
        PlayerPrefs.SetInt("Level2", -1);
        PlayerPrefs.SetInt("Level3", -1);
        PlayerPrefs.SetInt("Level4", -1);
        PlayerPrefs.SetInt("Level5", -1);
        PlayerPrefs.SetInt("Level6", -1);
        PlayerPrefs.SetInt("Level7", -1);
        PlayerPrefs.SetInt("Level8", -1);
        PlayerPrefs.SetInt("Level9", -1);
    }
	  
    public void moveToNextButtons(GameObject vecinity, GameObject nextVecinity, int selectedOption) {
        int levelSelected = currentLevel + 1;
        int optionSelected = selectedOption;
        string levelKey = "Level" + levelSelected.ToString();
        print(levelKey + ": " + optionSelected);
        PlayerPrefs.SetInt(levelKey, optionSelected);

        BlobingTypeA[] blobsA = vecinity.gameObject.transform.parent.GetComponentsInChildren<BlobingTypeA>();
        BlobingTypeB[] blobsB = vecinity.gameObject.transform.parent.GetComponentsInChildren<BlobingTypeB>();
        foreach (BlobingTypeA item in blobsA)
        {
            Destroy(item);
        }

        foreach (BlobingTypeB item in blobsB)
        {
            Destroy(item);
        }
        vecinity.SetActive(false);
        vecinity.SetActive(false);
        vecinity.GetComponent<GeneralButtonsSound>().stopAudio();

        if (status[currentLevel] && !status[numberOfCarps-1]) {
            nextVecinity.SetActive(true);
            status[currentLevel] = false;
            status[++currentLevel] = true;
        }
        else if(status[numberOfCarps-1]) {
            StartCoroutine(nextLevel());
        }
    }

    IEnumerator nextLevel() {
        yield return new WaitForSeconds(2);
        this.player.GetComponent<PlayerBehavior>().goToEnd();
    }
}
