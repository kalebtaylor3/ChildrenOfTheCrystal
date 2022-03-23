using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dimension : MonoBehaviour
{

    private static Dimension instance;
    public static Dimension Instance { get { return instance; } }


    private void Awake()
    {
        instance = this;
    }

    public enum Dimensions
    {
        Red,
        Blue,
        Green,
        Yellow,
        Main,
    };
    
    [SerializeField]
    GameObject[] hints;

    public Dimensions currentDimension = Dimensions.Main;

    [SerializeField]
    GameObject[] dimensionList;

    [HideInInspector]
    public bool inDimension = false;

    public PostProcessingControll postEffects;

    public PlayerMove[] playersForLayers;


    [SerializeField]
    public GameObject redRune;
    public GameObject greenRune;
    public GameObject blueRune;
    public GameObject yellowRune;

    [SerializeField]
    public GameObject redRuneGrey;
    public GameObject greenRuneGrey;
    public GameObject blueRuneGrey;
    public GameObject yellowRuneGrey;

    public GameObject redEffect;
    public GameObject blueEffect;
    public GameObject greenEffect;
    public GameObject yellowEffect;

    [SerializeField]
    private bool hasRed = false;
    [SerializeField]
    private bool hasBlue = false;
    [SerializeField]
    private bool hasGreen = false;
    [SerializeField]
    private bool hasYellow = false;


    // Start is called before the first frame update
    void Start()
    {
        postEffects.ChangeDimension(Dimensions.Main);
        dimensionList[(int)Dimensions.Main].SetActive(true);

        redRune.SetActive(false);
        greenRune.SetActive(false);
        blueRune.SetActive(false);
        yellowRune.SetActive(false); // yes I know this is bad code

        redRuneGrey.SetActive(false);
        greenRuneGrey.SetActive(false);
        blueRuneGrey.SetActive(false);
        yellowRuneGrey.SetActive(false);

    }

    private void Update()
    {
        if (!inDimension)
        {
            if (Input.GetKeyDown(KeyCode.Z))
                Strength();
            if (Input.GetKeyDown(KeyCode.X))
                Green();
            if (Input.GetKeyDown(KeyCode.N))
                SuperSpeed();
            if (Input.GetKeyDown(KeyCode.M))
                Yellow();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Z))
                DisableAll(true);
            if (Input.GetKeyDown(KeyCode.X))
                DisableAll(true);
            if (Input.GetKeyDown(KeyCode.N))
                DisableAll(true);
            if (Input.GetKeyDown(KeyCode.M))
                DisableAll(true);
        }

        Physics.IgnoreLayerCollision(3, 9);
        Physics.IgnoreLayerCollision(3, 10);
        Physics.IgnoreLayerCollision(3, 11);
        Physics.IgnoreLayerCollision(3, 12);
    }

    void DisableEffects()
    {
        blueEffect.SetActive(false);
        redEffect.SetActive(false);
        greenEffect.SetActive(false);
        yellowEffect.SetActive(false);
    }

    public void Strength()
    {
        if (hasRed)
        {
            DisableEffects();
            redEffect.SetActive(true);
            inDimension = true;
            playersForLayers[0].animator.SetTrigger("Red");
            playersForLayers[0].aSource.clip = playersForLayers[0].dimensionSound;
            playersForLayers[0].aSource.volume = 1;
            playersForLayers[0].aSource.Play();
            StartCoroutine(WaitForAnimation(playersForLayers[0], 1));
        }
    }

    void GiveStrength()
    {
        DisableAll(false);
        dimensionList[(int)Dimensions.Red].SetActive(true);
        inDimension = true;
        hints[(int)Dimensions.Red].SetActive(false);
        postEffects.ChangeDimension(Dimensions.Red);
        playersForLayers[1].SetLayerRecursively(playersForLayers[1].gameObject, 3);

        redRune.SetActive(true);
        greenRune.SetActive(false);
        blueRune.SetActive(false);
        yellowRune.SetActive(false);

        currentDimension = Dimensions.Red;
    }

    public void SuperSpeed()
    {
        if (hasBlue)
        {
            DisableEffects();
            blueEffect.SetActive(true);
            inDimension = true;
            playersForLayers[1].animator.SetTrigger("Blue");
            playersForLayers[1].aSource.clip = playersForLayers[1].dimensionSound;
            playersForLayers[1].aSource.volume = 1;
            playersForLayers[1].aSource.Play();
            StartCoroutine(WaitForAnimation(playersForLayers[1], 2));
        }
    }

    void GiveSpeed()
    {
        DisableAll(false);
        dimensionList[(int)Dimensions.Blue].SetActive(true);
        inDimension = true;
        hints[(int)Dimensions.Blue].SetActive(false);
        postEffects.ChangeDimension(Dimensions.Blue);
        playersForLayers[0].SetLayerRecursively(playersForLayers[0].gameObject, 3);

        redRune.SetActive(false);
        greenRune.SetActive(false);
        blueRune.SetActive(true);
        yellowRune.SetActive(false);

        currentDimension = Dimensions.Blue;
    }

    public void Yellow()
    {
        if (hasYellow)
        {

            DisableEffects();
            yellowEffect.SetActive(true);
            inDimension = true;
            playersForLayers[1].animator.SetTrigger("Yellow");
            playersForLayers[1].aSource.clip = playersForLayers[1].dimensionSound;
            playersForLayers[1].aSource.volume = 1;
            playersForLayers[1].aSource.Play();
            StartCoroutine(WaitForAnimation(playersForLayers[1], 4));
        }
    }

    void GiveYellow()
    {
        DisableAll(false);
        dimensionList[(int)Dimensions.Yellow].SetActive(true);
        inDimension = true;
        hints[(int)Dimensions.Yellow].SetActive(false);
        postEffects.ChangeDimension(Dimensions.Yellow);
        playersForLayers[0].SetLayerRecursively(playersForLayers[0].gameObject, 3);

        redRune.SetActive(false);
        greenRune.SetActive(false);
        blueRune.SetActive(false);
        yellowRune.SetActive(true);

        currentDimension = Dimensions.Yellow;
    }

    public void Green()
    {
        if (hasGreen)
        {
            DisableEffects();
            greenEffect.SetActive(true);
            inDimension = true;
            playersForLayers[0].animator.SetTrigger("Green");
            playersForLayers[0].aSource.clip = playersForLayers[0].dimensionSound;
            playersForLayers[0].aSource.volume = 1;
            playersForLayers[0].aSource.Play();
            StartCoroutine(WaitForAnimation(playersForLayers[0], 3));
        }
    }

    void GiveGreen()
    {
        dimensionList[(int)Dimensions.Green].SetActive(true);
        inDimension = true;
        hints[(int)Dimensions.Green].SetActive(false);
        postEffects.ChangeDimension(Dimensions.Green);
        playersForLayers[1].SetLayerRecursively(playersForLayers[1].gameObject, 3);

        redRune.SetActive(false);
        greenRune.SetActive(true);
        blueRune.SetActive(false);
        yellowRune.SetActive(false);

        currentDimension = Dimensions.Green;
    }

    public void DisableAll(bool normal)
    {

        if (normal)
            DisableEffects();

        for (int i = 0; i < dimensionList.Length; i++)
        {
            dimensionList[i].SetActive(false);
        }

        for (int i = 0; i < hints.Length; i++)
        {
            hints[i].SetActive(true);
        }
        postEffects.ChangeDimension(Dimensions.Main);
        dimensionList[(int)Dimensions.Main].SetActive(true);
        playersForLayers[0].SetLayerRecursively(playersForLayers[0].gameObject, 13);
        playersForLayers[1].SetLayerRecursively(playersForLayers[1].gameObject, 13);
        inDimension = false;

        redRune.SetActive(false);
        greenRune.SetActive(false);
        blueRune.SetActive(false);
        yellowRune.SetActive(false);

        currentDimension = Dimensions.Main;
    }

    IEnumerator WaitForAnimation(PlayerMove player, int D)
    {
        player.canMove = false;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        yield return new WaitForSeconds(0.8f);
        switch (D)
        {
            case 1:
                GiveStrength();
                break;
            case 2:
                GiveSpeed();
                break;
            case 3:
                GiveGreen();
                break;
            case 4:
                GiveYellow();
                break;
        }
        yield return new WaitForSeconds(1.2f);
        player.canMove = true;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
    }

    public void HasRed()
    {
        hasRed = true;
        redRuneGrey.SetActive(true);
    }

    public void HasGreen()
    {
        hasGreen = true;
        greenRuneGrey.SetActive(true);
    }
    public void HasBlue()
    {
        hasBlue = true;
        blueRuneGrey.SetActive(true);
    }
    public void HasYellow()
    {
        hasYellow = true;
        yellowRuneGrey.SetActive(true);
    }
}
