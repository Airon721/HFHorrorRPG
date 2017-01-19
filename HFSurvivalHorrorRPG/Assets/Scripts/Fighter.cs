using UnityEngine;
using System.Collections;

public class Fighter : MonoBehaviour {

	//public GameObject opponent;
	public int damage;
	public double impactTime;
	public bool hit = false;
	public double range;
	public int maxHealth;
	public int Health;
	public bool Special_attack;
	public int[] spAttackDmg;
	public int[] spAttackStun;
	//controls animations on death 
	public bool started;
	public bool ended;
	public float escapeTime = 10;
	public float countDown;
	public bool attacking = false;
	public Animator anim;
	public GM gameManager;
	public float fightTime;
	public GameObject Spear;
	private Arrow arrow;
	private bool fhauf = false;


	// Use this for initialization
	void Start () 
	{
		Health = maxHealth;
		ended = false;
		anim = GetComponent<Animator>();
		Spear = GameObject.FindGameObjectWithTag ("Spear");
		arrow = Spear.GetComponent<Arrow> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (attacking && fightTime > 0) {
			fightTime = fightTime - Time.deltaTime;
		} else if (attacking && fightTime <= 0) {
			attacking = false;
		}
		if (!fhauf) {
			arrow.AddForce ();
			fhauf = true;
		}
		die ();
	}

	public void Get_Hit(int damage)
	{
		Health -= damage;
		if (Health < 0) 
		{
			Health =0;
		}
	}

	/*void impact(int stun, double scaledDam)
	{
	   if (opponent != null && GetComponent<Animation>().IsPlaying(attack.name)&&!hit) 
		{
			if(GetComponent<Animation>()[attack.name].time>impactTime&&
			   GetComponent<Animation>()[attack.name].time < 0.9*GetComponent<Animation>()[attack.name].length)
			{
				SetCombatCountdown();
				
				opponent.GetComponent<mob>().getHit((int)(damage*scaledDam));
				opponent.GetComponent<mob>().GetStun(stun);
				hit = true;
			}
		}
	}*/

	/*public void SetCombatCountdown ()
	{
		countDown = escapeTime+2;
		CancelInvoke("CombatCountDown");
		InvokeRepeating("CombatCountDown",0,1);
	}*/

	public void resetAttack()
	{
		hit = false;
		attacking = false;
	}


	/*bool InRange()
	{
		if (Vector3.Distance(opponent.transform.position, transform.position)<= range) 
		{
			return true;
		}
		return false;
	}*/

	public bool isDead()
	{
		//return true when char is dead
		if (Health ==0 ) 
		{
			return true;
		}
		return false;
	}

	/*void CombatCountDown()
	{
		countDown = countDown - 1;
		if (countDown == 0) 
		{
			CancelInvoke("CombatCountDown");
		}
	}*/

	void die()
	{
		if (isDead() && !ended) 
		{
			if(!started)
			{
						//GetComponent<Animation>().Play (dies.name);
				started = true;
						//ClickToMove.dieing = true;
			}
					//if(started&&!GetComponent<Animation>().IsPlaying(dies.name))
					//{
			Debug.Log("You Have Died");
			ended = true;
			gameManager.pauseGame();
					//}
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "smallHealth") {
			Destroy (other.gameObject);
			if (Health + 10 < maxHealth) {
				Health = Health + 10;
			} else {
				Health = maxHealth;
			}
			Debug.Log ("Player Picked up small Health");
		}
		if (other.gameObject.tag == "bigHealth") {
			Destroy (other.gameObject);
			if (Health + 25 < maxHealth) {
				Health = Health + 25;
			} else {
				Health = maxHealth;
			}
			Debug.Log ("Player Picked up big health");
		}
		if (other.gameObject.tag == "Fireball") {
			Health = Health - other.GetComponent<Fireball> ().damage;
			if (Health < 0) {
				Health = 0;
			}
		}
		if (other.gameObject.tag == "spawner") {
			other.GetComponent<SpawnEnemies> ().spawnStuff ();
			Destroy (other);
		}
		if (other.gameObject.tag == "Weapon") {
			Health = Health - other.GetComponent<Weapon> ().damage;
			if (Health < 0) {
				Health = 0;
			}
			Debug.Log("Boss Hit Player");
		}
	}

	public bool isAttack()
	{
		return attacking;
	}








}
