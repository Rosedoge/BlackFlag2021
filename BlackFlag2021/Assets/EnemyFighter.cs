using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFighter : MonoBehaviour {
    public static EnemyFighter instance = null;
    public Transform target;
    public float speed;
    public int Health = 1000;
    public GameObject Explosion;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    bool triggered = false;
	// Use this for initialization
	void Start () {
        target = PlayerScript.instance.transform;
	}
	
	// Update is called once per frame
	void Update () {
        float move = speed * Time.deltaTime;
        transform.Translate(Vector3.forward * move);

        Vector3 targetDir = target.position - transform.position;
        float step = speed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 0.05f, 0.0F);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);

        if (Health <= 0)
        {
            Instantiate(Explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.tag.Contains("Player") || !collision.gameObject.tag.Contains("PBullet"))
        {
            Instantiate(Explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag.Contains("PBullet"))
        {
            Health -= 75;
            Destroy(collision.gameObject);
        }
    }
}
