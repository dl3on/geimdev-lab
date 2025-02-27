using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public LakitusCloud lakitusCloud;

    void Awake()
    {
        GameManager.instance.gameRestart.AddListener(GameRestart);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameRestart()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<EnemyMovement>().GameRestart();
        }

        lakitusCloud.cloudBody.transform.localPosition = new Vector3(8.95f, -0.13f, 0.0f);
    }
}
