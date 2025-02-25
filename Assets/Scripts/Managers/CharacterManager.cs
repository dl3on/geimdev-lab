using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class CharacterManager : Singleton<CharacterManager>
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
        if (Input.GetKeyDown("b") && activePlayerMovement.onGroundState)
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
        if (activeCharacter == mario)
        {
            activeCharacter = bowser;
            marioInput.enabled = false;
            bowserInput.enabled = true;
            currPos.y += 2.0f;
            bowserSnapshot.TransitionTo(0.5f);
            Debug.Log("Bowser Mode!");
        }
        else
        {
            activeCharacter = mario;
            marioInput.enabled = true;
            bowserInput.enabled = false;
            marioSnapshot.TransitionTo(0.5f);
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

    public void GameRestart()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<PlayerMovement>().GameRestart();
        }
    }
}
