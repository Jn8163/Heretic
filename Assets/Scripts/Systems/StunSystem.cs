using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.AI;

public class StunSystem : MonoBehaviour
{
    [SerializeField] private int minStunValue = 1;
    [SerializeField] private int maxStunValue = 1;
	[SerializeField] private int knockbackResist = 0;
	[SerializeField] private Animator anim;
	public bool isStunned, isKnockback;
	private float stunTimer = 0f;
	public bool refreshTimer;
	private float enemySpeed, enemyAcceleration, enemyAngularSpeed, knockbackAmt;
	int rand;

	private void Start()
	{
		enemySpeed = GetComponent<NavMeshAgent>().speed;
		enemyAcceleration = GetComponent<NavMeshAgent>().acceleration;
		enemyAngularSpeed = GetComponent<NavMeshAgent>().angularSpeed;
	}
	public void TryStun(int stunValue, int knockbackValue)
    {
		rand = UnityEngine.Random.Range(minStunValue, maxStunValue + 1);
		if (stunValue >= rand)
		{
			Debug.Log("stunned! " + stunValue);
			isStunned = true;
			refreshTimer = true;
			GetComponentInChildren<EnemyAudioCalls>().PlayTdamage();
			ApplyKnockback(knockbackValue);
		}
		
	}

	public void ApplyKnockback(int knockback)
	{
		if (knockback - knockbackResist > 0)
		{
			Debug.Log("Apply Knockback!");
			knockbackAmt = knockback - knockbackResist;
			isKnockback = true;
		}
		else
		{
			Debug.Log("No knockback!");
		}
	}

	public void Update()
	{
		if (refreshTimer)
		{
			refreshTimer = false;
			stunTimer = 0f;
		}
		if (isStunned)
		{
			GetComponent<NavMeshAgent>().speed = 0f;
			GetComponent<NavMeshAgent>().acceleration = 500f;
			GetComponent<NavMeshAgent>().angularSpeed = 0;

			anim.SetBool("isHurt", true);
			stunTimer += Time.deltaTime;
			if (isKnockback)
			{
				transform.Translate(new Vector3(0, 0, -1) * 10 * Time.deltaTime * knockbackAmt);
			}
			
		}
		if (stunTimer > 0.166f)
		{
			stunTimer = 0f;
			isStunned = false;
			isKnockback = false;
			anim.SetBool("isHurt", false);
			if (GetComponent<HealthSystem>().currentHealth > 0)
			{
				GetComponent<NavMeshAgent>().speed = enemySpeed;
				GetComponent<NavMeshAgent>().acceleration = enemyAcceleration;
				GetComponent<NavMeshAgent>().angularSpeed = enemyAngularSpeed;
			}
		}
	}
}
