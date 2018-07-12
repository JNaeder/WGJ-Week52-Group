using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using TMPro;

public class GuyController : MonoBehaviour {
	public int health = 5;
    public float speed = 5f;
	public Ammo[] ammo;
	public Transform gun;
    public Transform gunMuzzle;

	public TextMeshProUGUI currentAmmoNum;
	public Image ammoImage, mainPlayerUI;
	public Sprite[] guyHealthImage;

	public GameObject crosshair;

	int currentAmmoIndex;
	public int[] ammoNum;


    Camera cam;
    public SpriteRenderer guySP, gunSP;
	public Transform gunTrans;

    [Range(80, 100)]
    public float angleCorrection;
    [Range(-10, 10)]
    public float angleAdjustment;

    public float screenOffset;

    [FMODUnity.EventRef]
    public string gunFireSound, shootNoAmmo, changeWeapon, playerHit;

	public Animator canvasAnim;

    float h;
    float v;
	bool isThrowing;


    GameManager gM;
	Animator anim;
	GameObject crossHairCursor;

    // Use this for initialization
    void Start () {
        cam = Camera.main;
        gM = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();

		crossHairCursor = Instantiate(crosshair, transform.position, Quaternion.identity) as GameObject;

	}
	
	// Update is called once per frame
	void Update () {
        
        Shooting();
        ChoosingWeapon();
		UpdateUI();
		SetAimTarget();
	}

    void FixedUpdate()
    {
        Movement();
    }


	public void SetAimTarget(){
		Cursor.visible = false;
		Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 lookPos = cam.ScreenToWorldPoint(mousePos);
		crossHairCursor.transform.position = lookPos;

	}


    void ChoosingWeapon() {

		if(Input.GetKeyDown(KeyCode.Space)){
            FMODUnity.RuntimeManager.PlayOneShot(changeWeapon);
			currentAmmoIndex++;
			if(currentAmmoIndex >= ammo.Length){
				currentAmmoIndex = 0;
			}

		}      
    }


    





    void Shooting() {


                if (Input.GetButtonDown("Fire1"))
                {


            if (ammoNum[currentAmmoIndex] > 0)
            {
				isThrowing = true;
                FMODUnity.RuntimeManager.PlayOneShot(gunFireSound);
                GameObject bullet = Instantiate(ammo[currentAmmoIndex].bulletToSpawn, gunMuzzle.position, gunMuzzle.rotation);
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                bulletScript.SetDamage(ammo[currentAmmoIndex].damage, ammo[currentAmmoIndex].bulletSpeed, ammo[currentAmmoIndex].knockbackTime, ammo[currentAmmoIndex].knockbackPower);
                ammoNum[currentAmmoIndex]--;
            }
            else {
                FMODUnity.RuntimeManager.PlayOneShot(shootNoAmmo);
            }
                }


		if(Input.GetButtonUp("Fire1")){
			isThrowing = false;

		}
            }

    void UpdateAnimator() {
        anim.SetFloat("speed", Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v)));
		anim.SetBool("isThrowing", isThrowing);

    }


    void Movement() {
		//Move Player
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        UpdateAnimator();


        transform.position += new Vector3(h, v, 0) * Time.deltaTime * speed * (1/Time.timeScale);

		
		



		//RotateGun
		Vector3 gunScale = gunTrans.localScale;
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 lookPos = cam.ScreenToWorldPoint(mousePos);
        lookPos = lookPos - gunTrans.position;
        float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        gunTrans.rotation = Quaternion.Euler(0, 0, angle - 90f);


        float xScreenLimit = Screen.width / 2;
        float yScreenLimit = Screen.height / 3;

        //Debug.Log(Input.mousePosition + " " + xScreenLimit + " " + yScreenLimit);


		if(mousePos.x < xScreenLimit && mousePos.y > yScreenLimit + screenOffset && mousePos.y < Screen.height - yScreenLimit){
            // Left
			gunScale.x = -1;
            gunSP.sortingOrder = 20;
            anim.SetFloat("mouseX", -1);
            anim.SetFloat("mouseY", 0);
        } else if(mousePos.x > xScreenLimit && mousePos.y > yScreenLimit + screenOffset && mousePos.y < Screen.height - yScreenLimit) {
            // Right
			gunScale.x = 1;
            gunSP.sortingOrder = 20;
            anim.SetFloat("mouseX", 1);
            anim.SetFloat("mouseY", 0);
        } else if(mousePos.y < yScreenLimit + screenOffset)
        {
            // Down
            
            gunSP.sortingOrder = 20;
            anim.SetFloat("mouseX", 0);
            anim.SetFloat("mouseY", -1);
        } else if(mousePos.y > Screen.height - yScreenLimit ) {
            // Up
            
            gunSP.sortingOrder = -50;
            anim.SetFloat("mouseX", 0);
            anim.SetFloat("mouseY", 1);
        }

		gunTrans.localScale = gunScale;

    }


	void UpdateUI(){
		currentAmmoNum.text = ammoNum[currentAmmoIndex].ToString();
		ammoImage.sprite = ammo[currentAmmoIndex].UIIcon;
		mainPlayerUI.sprite = guyHealthImage[health];
        

	}



	public void TakeDamage(float newDamage){
		//anim.Play("GetHurt");
        FMODUnity.RuntimeManager.PlayOneShot(playerHit);
		canvasAnim.Play("Canvas_GetHit");
		health -= (int)newDamage;
        if (health <= 0) {
            Death();

        }
	}

    public void Death() {
        gM.SetHighScore();
        gM.ShowDeathScreen();
		Destroy(crossHairCursor);
        Cursor.visible = true;
        mainPlayerUI.sprite = guyHealthImage[0];
        Destroy(gameObject);


    }
}
