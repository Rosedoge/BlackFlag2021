using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    float speed = 10;

    public int Health = 1000;
    [SerializeField]
    GameObject DangerObject;
    GameObject FlashTriggerer = null;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        float move = speed * Time.deltaTime;
        float ver = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * move);
        transform.Translate(Vector3.right * hor * move);
    }

    //  StartCoroutine(Flasher);
    IEnumerator Flasher()
    {
        for (int i = 0; i < 5; i++)
        {
            DangerObject.SetActive(true);
            yield return new WaitForSeconds(.1f);
            DangerObject.SetActive(false);
            yield return new WaitForSeconds(.1f);
        }
        FlashTriggerer = null;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject != FlashTriggerer)
        {
            StartCoroutine(Flasher());
            FlashTriggerer = other.gameObject;
        }

    }
    private void OnCollisionEnter(Collision other) {

        if (other.gameObject.tag.Contains("Building"))
        {
            Health -= 100;
        }
    }


}
