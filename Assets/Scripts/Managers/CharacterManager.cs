using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
using System.Collections;

public class CharacterManager : MonoBehaviour
{
    public GameObject mario;
    public GameObject bowser;
    public GameObject activeCharacter;
    public static PlayerMovement activePlayerMovement;
    private bool faceLeft;
    private PlayerInput marioInput;
    private PlayerInput bowserInput;
    [SerializeField] private InputActionAsset inputActionAsset;

    // Audio snapshots
    public AudioMixer mixer;
    private AudioMixerSnapshot marioSnapshot;
    private AudioMixerSnapshot bowserSnapshot;

    // Bowser powerup limits
    private bool canSwitchToBowser = true;
    private bool isBowser = false;
    private float bowserDuration = 20f;
    private float cooldownDuration = 20f;

    void Awake()
    {
        GameManager.instance.gameRestart.AddListener(GameRestart);
    }

    void Start()
    {
        activePlayerMovement = activeCharacter.GetComponent<PlayerMovement>();

        // Get PlayerInput components
        marioInput = mario.GetComponent<PlayerInput>();
        bowserInput = bowser.GetComponent<PlayerInput>();

        // Get audio snapshots
        marioSnapshot = mixer.FindSnapshot("Default");
        bowserSnapshot = mixer.FindSnapshot("Bowser");

        // Set Mario to be active first
        bowserInput.enabled = false;
        marioInput.enabled = false;
        bowser.SetActive(false);
        marioInput.enabled = true;
        activeCharacter = mario;

        if (marioInput.actions != inputActionAsset)
        {
            marioInput.actions = inputActionAsset;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("b") && activePlayerMovement.onGroundState && canSwitchToBowser)
        {
            SwapCharacter();
        }
    }

    void SwapCharacter()
    {
        // get current position
        Vector3 currPos = activeCharacter.transform.position;

        activeCharacter.SetActive(false);

        // Switch characters    
        if (activeCharacter == mario && canSwitchToBowser)
        {
            activeCharacter = bowser;
            marioInput.enabled = false;
            bowserInput.enabled = true;
            currPos.y += 2.0f;
            bowserSnapshot.TransitionTo(0.5f);
            isBowser = true;
            canSwitchToBowser = false;
            Debug.Log("Bowser Mode!");
            StartCoroutine(BowserModeTimer());
        }
        else
        {
            activeCharacter = mario;
            marioInput.enabled = true;
            bowserInput.enabled = false;
            marioSnapshot.TransitionTo(0.5f);
            isBowser = false;
            Debug.Log("Mario Mode!");
        }
        activeCharacter.transform.position = currPos;

        faceLeft = activePlayerMovement.faceLeftState ? true : false;

        activeCharacter.SetActive(true);

        // Ensure the correct input action asset is used
        if (marioInput.actions != inputActionAsset && activeCharacter == mario)
        {
            marioInput.actions = inputActionAsset;
        }
        if (bowserInput.actions != inputActionAsset && activeCharacter == bowser)
        {
            bowserInput.actions = inputActionAsset;
        }

        // reset ground state
        activePlayerMovement = activeCharacter.GetComponent<PlayerMovement>();
        activePlayerMovement.onGroundState = false;

        // set face direction
        activePlayerMovement.faceLeftState = faceLeft;
        activePlayerMovement.characterSprite.flipX = faceLeft;

        activePlayerMovement.UpdateCharacterReferences(activeCharacter);

        // restart animations
        // Animator animator = activeCharacter.GetComponent<Animator>();
        // animator.enabled = false;
        // animator.Rebind();
        // animator.Update(0);
        // animator.enabled = true;
    }

    IEnumerator BowserModeTimer()
    {
        yield return new WaitForSeconds(bowserDuration);

        if (isBowser)
        {
            SwapCharacter();
            StartCoroutine(BowserCooldown());
        }
    }

    IEnumerator BowserCooldown()
    {
        Debug.Log("Bowser Mode on cooldown: 20s!");
        yield return new WaitForSeconds(cooldownDuration);
        canSwitchToBowser = true;
        Debug.Log("Bowser Mode is available!");
    }

    public void GameRestart()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<PlayerMovement>().GameRestart();
        }
    }
}
