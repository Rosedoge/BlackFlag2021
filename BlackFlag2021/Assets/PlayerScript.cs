using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerScript : MonoBehaviour {
    float speed = 10;


    public int Ammo = 500;
    public GameObject GunLight;
    public GameObject Bullet;

    public Image speedBar;
    public static PlayerScript instance = null;
    public int Health = 1000;
    [SerializeField]
    GameObject DangerObject;
    GameObject FlashTriggerer = null;

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


    // Use this for initialization
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update() {

#region Speed
        float move = speed * Time.deltaTime;
        transform.Translate(Vector3.forward * move);
        
        Mathf.Clamp(speed, 1, 20);
        if (Input.GetKey(KeyCode.W))
        {
            speed += .5f;
            if (speed >= 20)
            {
                speed = 20;
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
           
            speed -= .5f;
            if (speed < 0.1f)
            {
                speed = 0.1f;
            }
        }

        Vector3 temp = new Vector3(speed / 20, 1, 1);

        if(temp.x > 1)
        {
            temp.x = 1;
        }
        if (temp.x < 0.1)
        {
            temp.x = 0.1f;
        }
        speedBar.GetComponent<RectTransform>().localScale = temp;
        if(speed <= 2)
        {
            GetComponent<Rigidbody>().useGravity = true;

        }
        else
        {
            GetComponent<Rigidbody>().useGravity = false;
        }

        #endregion Speed

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            InvokeRepeating("Shoot", 0, 0.05f);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            CancelInvoke();
            GunLight.SetActive(false);
        }
    }


    void Shoot()
    {
        if (GunLight.activeSelf)
        {
            GunLight.SetActive(false);
        }
        else
        {
            GunLight.SetActive(true);
        }
        Ammo -= 1;
        GameObject temp = Instantiate(Bullet, transform.position, transform.rotation);
        temp.GetComponent<Rigidbody>().AddForce(transform.forward * 5000);
        Destroy(temp, 5f);

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
