using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraBoss : MonoBehaviour {



	float distToPlayer;

	public float health;

	public GameObject bullet;
	public float distThreshold;
	public float fireRate;
	public LayerMask layerMask;

	public LineRenderer lineRend;
	public GameObject damageVisual, healthBar;

	bool isAttacking, isActive;

	GuyController guy;
	Canvas can;
	SpriteRenderer sP;
	GameManager gM;

	// Use this for initialization
	void Start () {
		guy = FindObjectOfType<GuyController>();
		can = FindObjectOfType<Canvas>();
		sP = GetComponentInChildren<SpriteRenderer>();
		gM = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {

		if (isActive)
		{

			FollowPlayer();
			if (guy != null)
			{
				distToPlayer = Vector3.Distance(transform.position, guy.transform.position);
			}
			if (distToPlayer < distThreshold)
			{
				lineRend.enabled = true;
				if (!isAttacking)

				{
					StartCoroutine("ShootAtPlayer");
				}
			}
			else
			{
				CancelInvoke();
				lineRend.enabled = false;
				isAttacking = false;
			}

		}

	}

	void ShowDamageVisual(float damage)
    {
        GameObject newDamageObject = Instantiate(damageVisual, transform.position, Quaternion.identity);
        TextMeshProUGUI newDamageText = newDamageObject.GetComponent<TextMeshProUGUI>();
        newDamageText.text = "-" + damage.ToString();
        newDamageText.transform.SetParent(can.transform, true);
        newDamageText.transform.localScale = Vector3.one;

    }

	public void TakeDamage(float damage)
    {
		if (isActive)
		{
			health -= damage;
			ShowDamageVisual(damage);
			if (health <= 0)
			{
				Deactivate();
			}
		}
    }


	public void Deactivate(){
		isActive = false;
		lineRend.enabled = false;
		healthBar.SetActive(false);
		sP.color = Color.black;
	}

	public void Activate(){
		isActive = true;
		healthBar.SetActive(true);
		sP.color = Color.grey;
	}
    



	void FollowPlayer(){
		if (guy != null)
		{
			Vector3 newPos = guy.transform.position - transform.position;
			float angle = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
		}

	}


	IEnumerator ShootAtPlayer(){
		isAttacking = true;
		GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);
        Bullet instBullet = newBullet.GetComponent<Bullet>();
        instBullet.SetDamage(1, 5, 0, 0);
		yield return new WaitForSeconds(1/fireRate);
		isAttacking = false;


	}





}
