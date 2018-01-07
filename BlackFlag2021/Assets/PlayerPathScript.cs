using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPathScript : MonoBehaviour {
    public static PlayerPathScript instance = null;

    [SerializeField]
    AirplanePath BasicPath, PathToPlayer;

    [SerializeField]
    GameObject PlayerNode, EnemyNode;


    void Awake()
    {
        if(instance == null){
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        PlayerNode.transform.position = PlayerScript.instance.transform.position;
        EnemyNode.transform.position = EnemyFighter.instance.transform.position;
    }


    public void Swap()
    {
        BasicPath.StopPath();
        BasicPath.enabled = false;
        PathToPlayer.enabled = true;
        PathToPlayer.StartPath();
        EnemyFighter.instance.gameObject.SetActive(true);
    }
}
