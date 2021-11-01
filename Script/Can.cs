using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can : MonoBehaviour
{
    public bool hasFallen;
    public bool hasCollided;

    public bool isBombCan;
    public bool isLifeCan;

    private int blastForce = 1000;
    private int blastRadius = 20;

    public GameObject blastFX;
    public GameObject lifeFx;
    public GameObject duseFX;

    public AudioSource touchSound;
    public AudioClip normalCanSound;
    public AudioClip lifeCanSound;
    public AudioClip bombCanSound;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Resetter"))
        {
            hasFallen = true;
            GameManager.instance.GroupFallenCheck();
            UIManager.instance.UpdateScore();
        }
    }

    void OnCollisionEnter(Collision collision)
    {

        if (hasCollided == true)
        {
            return;
        }

        if (collision.gameObject.name == "Ball")
        {
            hasCollided = true;

            if (isBombCan)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

                foreach (Collider c in colliders)
                {
                    Rigidbody rb = c.GetComponent<Rigidbody>();
                    if (rb != null)
                    {

                        touchSound.PlayOneShot(bombCanSound);
                        rb.AddExplosionForce(blastForce, transform.position, blastRadius, 4, ForceMode.Impulse);
                    }



                    Instantiate(blastFX, transform.position, Quaternion.identity);
                }
            }
            else if (isLifeCan)
            {
                touchSound.PlayOneShot(lifeCanSound);
                GameManager.instance.AddExtraBall(1);

                GameObject fx = Instantiate(lifeFx, transform.position, Quaternion.identity);
                fx.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                touchSound.PlayOneShot(normalCanSound);
                Instantiate(duseFX, transform.position, Quaternion.identity);
                
            }



        }


    }

}
