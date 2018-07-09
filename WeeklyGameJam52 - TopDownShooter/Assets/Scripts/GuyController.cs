using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class GuyController : MonoBehaviour {
	public int health = 5;
    public float speed = 5f;
	public Ammo[] ammo;
	public Transform gun;
    public Transform gunMuzzle;

	public Text ammo1num, ammo2num, ammo3num, healthNum;
	public Image[] ammoBGHighlight;

	public GameObject crosshair;

	int currentAmmoIndex;
	public int[] ammoNum;

	Ammo currentAmmo;

    Camera cam;
    public SpriteRenderer guySP, gunSP;
	public Transform gunTrans;

    public float specialCap, specialStartNum;
    float  specialBarPerc;
    public Image specialBarFront, specialReaduButton;
    public  bool isSpecialTime;
    public float slowDownTime;

    [Range(80, 100)]
    public float angleCorrection;
    [Range(-10, 10)]
    public float angleAdjustment;

    public float screenOffset;

    [FMODUnity.EventRef]
    public string gunFireSound;


    float h;
    float v;
    float h_raw;
    float v_raw;



    GameManager gM;
    Animator anim;
	GameObject crossHairCursor;

    // Use this for initialization
    void Start () {
        cam = Camera.main;
        gM = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();

		crossHairCursor = Instantiate(crosshair, transform.position, Quaternion.identity) as GameObject;

		currentAmmo = ammo[0];
		UpdateAmmoHighlight(0);
        specialStartNum = specialCap;
        specialCap = 0;
	}
	
	// Update is called once per frame
	void Update () {
        
        Shooting();
        ChoosingWeapon();
		UpdateUI();
        Special();
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

    void Special() {
        Mathf.Round(specialCap);
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            if (!isSpecialTime)
            {
                Debug.Log("Special!");
                isSpecialTime = true;
            }

        }


        if (isSpecialTime) {
            if (specialCap > 0)
            {
                specialCap -= Time.deltaTime;


                Time.timeScale = 1 / slowDownTime;
            }
            else {
                Time.timeScale = 1;
                isSpecialTime = false;

            }

        }
    }


    void ChoosingWeapon() {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
			currentAmmoIndex = 0;
			UpdateAmmoHighlight(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {

			currentAmmoIndex = 1;
			UpdateAmmoHighlight(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {

			currentAmmoIndex = 2;
			UpdateAmmoHighlight(2);
        }


    }


    





    void Shooting() {


                if (Input.GetButtonDown("Fire1"))
                {


			    if (ammoNum[currentAmmoIndex] > 0)
                    {
                FMODUnity.RuntimeManager.PlayOneShot(gunFireSound); 
				    GameObject bullet = Instantiate(ammo[currentAmmoIndex].bulletToSpawn , gunMuzzle.position, gunMuzzle.rotation);
                        Bullet bulletScript = bullet.GetComponent<Bullet>();
				        bulletScript.SetDamage(ammo[currentAmmoIndex].damage, ammo[currentAmmoIndex].bulletSpeed);
				ammoNum[currentAmmoIndex]--;
                    }
                }
            }

    void UpdateAnimator() {
        anim.SetFloat("speed", Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v)));


    }


    void Movement() {
		//Move Player
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        h_raw = Input.GetAxisRaw("Horizontal");
        v_raw = Input.GetAxisRaw("Vertical");

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
		ammo1num.text = ammoNum[0].ToString();
		ammo2num.text = ammoNum[1].ToString();
		ammo3num.text = ammoNum[2].ToString();

        healthNum.text = health.ToString();

        if (Mathf.Round(specialCap) <= specialStartNum)
        {
            specialBarPerc = specialCap / specialStartNum;
            Vector3 barScale = specialBarFront.transform.localScale;
            barScale.y = specialBarPerc;
            specialBarFront.transform.localScale = barScale;
        }
        if (Mathf.Round(specialCap) >= specialStartNum)
        {
            specialReaduButton.color = Color.red;
        }
        else {
            specialReaduButton.color = Color.grey;
        }

	}

	void UpdateAmmoHighlight(int ammoIndex){
		for (int i = 0; i < ammoBGHighlight.Length; i++){
			ammoBGHighlight[i].gameObject.SetActive(false);
		}

		ammoBGHighlight[ammoIndex].gameObject.SetActive(true);

	}

	public void TakeDamage(float newDamage){
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
        Destroy(gameObject);


    }
}
