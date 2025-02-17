using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.U2D.Animation;

public class CharacterSwap : MonoBehaviour
{
    public GameObject mario;
    public GameObject bowser;
    public GameObject activeCharacter;
    public static PlayerMovement activePlayerMovement;
    private bool faceLeft;
    void Start()
    {
        activePlayerMovement = activeCharacter.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown("b") && activePlayerMovement.onGroundState)
        {
            // get current position
            Vector3 currPos = activeCharacter.transform.position;

            activeCharacter.SetActive(false);

            // Switch characters    
            if (activeCharacter == mario)
            {
                activeCharacter = bowser;
                currPos.y += 2.0f;
                Debug.Log("Bowser Mode!");
            }
            else
            {
                activeCharacter = mario;
                Debug.Log("Mario Mode!");
            }
            activeCharacter.transform.position = currPos;

            //faceLeft = activePlayerMovement.faceLeftState ? true : false;
            activeCharacter.SetActive(true);

            // reset ground state
            activePlayerMovement = activeCharacter.GetComponent<PlayerMovement>();
            activePlayerMovement.onGroundState = false;

            activePlayerMovement.UpdateCharacterReferences(activeCharacter);

            // restart animations
            // Animator animator = activeCharacter.GetComponent<Animator>();
            // animator.enabled = false;
            // animator.Rebind();
            // animator.Update(0);
            // animator.enabled = true;
            //activePlayerMovement.characterSprite.flipX = faceLeft;
        }
    }
}
