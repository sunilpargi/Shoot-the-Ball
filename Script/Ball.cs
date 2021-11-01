using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	Vector3 ballSpwanPos;

	// Use this for initialization
	void Start()
	{
		ballSpwanPos = transform.position;
	}


	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.CompareTag("Resetter"))
		{
            if (GameManager.instance.totalBalls > 0)
            {
                RepositionBall();

			}

		}

	}



	public void RepositionBall()
	{
		gameObject.SetActive(false);
		transform.position = ballSpwanPos;
		this.GetComponent<Animator>().enabled = true;
		gameObject.SetActive(true);
		StartCoroutine(SetReadyToShoot());

	}

	IEnumerator SetReadyToShoot()
	{
		yield return new WaitForSeconds(2.0f);
		GameManager.instance.readyToshoot = true;
	}
}
