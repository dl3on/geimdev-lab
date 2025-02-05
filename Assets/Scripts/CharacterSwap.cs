using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.U2D.Animation;

public class CharacterSwap : MonoBehaviour
{
    public GameObject marioPrefab;
    public GameObject bowserPrefab;
    public GameObject activeCharacter;
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown("b"))
        {
            activeCharacter.SetActive(false);

            // Switch characters    
            if (activeCharacter == marioPrefab)
            {
                activeCharacter = bowserPrefab;
                activeCharacter.transform.position = marioPrefab.transform.position;
                Debug.Log("Bowser Mode!");
            }
            else
            {
                activeCharacter = marioPrefab;
                activeCharacter.transform.position = bowserPrefab.transform.position;
                Debug.Log("Mario Mode!");
            }
            activeCharacter.SetActive(true);
        }
    }
}
