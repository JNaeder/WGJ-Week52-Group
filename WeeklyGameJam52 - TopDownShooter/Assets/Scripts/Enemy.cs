using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FMODUnity;

public class Enemy : MonoBehaviour {

	public float health;
	public float speed;
	public float damageToPlayer;
	public int points;
	public Transform healthBarGreen;
	public GameObject[] drops;
    public float dropRange;
    public int minDropNum, maxDropNum;
	public GameObject smoke;

    [FMODUnity.EventRef]
    public string enemyDeathSound;

    public GameObject damageVisual;
	Canvas can;

	float knockbackTime;
	float healthPerc, startHealthNum;

	Vector3 enemyDirection;

	Transform target;
	[HideInInspector]
    public GuyController guy;
	Rigidbody2D rB;
	Animator anim;

    // Use this for initialization
    void Start () {
		startHealthNum = health;
        if (FindObjectOfType<GuyController>() != null)
        {
            target = FindObjectOfType<GuyController>().transform;
        }

		GameManager.numberOfEnemiesLeft++;
        guy = FindObjectOfType<GuyController>();
		can = FindObjectOfType<Canvas>();
		rB = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		SetUpHealth();
		FollowPlayer();
		UpdateAnimator();

        
		if(knockbackTime < 0){
			rB.velocity = Vector2.zero;

		} else {
			knockbackTime -= Time.deltaTime;
            
		}
	}

    
	void UpdateAnimator(){
		if (guy != null)
		{
			enemyDirection = guy.transform.position - transform.position;

			float h = enemyDirection.x;
			float v = enemyDirection.y;
			anim.SetFloat("h", h);
			anim.SetFloat("v", v);
		}

	}


	void SetUpHealth(){
		healthPerc = health / startHealthNum;

		Vector3 healthBarScale = healthBarGreen.localScale;
		healthBarScale.x = healthPerc;
		healthBarGreen.localScale = healthBarScale;
	}


	public  void TakeDamage(float damage, float newKnockbackTime, float newKnockbackPower){
		health -= damage;
        ShowDamageVisual(damage);
		knockbackTime = newKnockbackTime;

		Vector3 knockbackDir = guy.transform.position - transform.position;
		knockbackDir.Normalize();
		rB.AddForce(-knockbackDir * newKnockbackPower, ForceMode2D.Impulse);
		if(health <= 0){
			Death();         
		}
	}


    void ShowDamageVisual(float damage) {
        GameObject newDamageObject = Instantiate(damageVisual, transform.position, Quaternion.identity);
		TextMeshProUGUI newDamageText = newDamageObject.GetComponent<TextMeshProUGUI>();
        newDamageText.text = "-" + damage.ToString();
		newDamageText.transform.SetParent(can.transform, true);
		newDamageText.transform.localScale = Vector3.one;

    }


	void Death()
	{
		GameManager.numberOfEnemiesLeft--;
		GameManager.score += points;
        DropItems();
		GameObject newSmoke = Instantiate(smoke, transform.position, Quaternion.identity);
		Destroy(newSmoke, 1f);
        FMODUnity.RuntimeManager.PlayOneShot(enemyDeathSound, transform.position);

        Destroy(gameObject);
		
	}

	void DropItems(){
		int randNum = Random.Range(minDropNum, maxDropNum);
		for (int i = 0; i < randNum; i++){
            Vector3 randomDir = new Vector3(Random.Range(-dropRange, dropRange), Random.Range(-dropRange, dropRange), 0);
           // float randomSpeed = Random.Range(1f, 3f);
            int randomDropNum = Random.Range(0, drops.Length);
            Instantiate(drops[randomDropNum], randomDir + transform.position, Quaternion.identity);

        }

		

	}

	void FollowPlayer(){
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }


	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player"){
			
			guy.TakeDamage(damageToPlayer);
		}
	}
}
