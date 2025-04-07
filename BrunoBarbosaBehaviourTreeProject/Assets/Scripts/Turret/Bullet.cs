using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //push
    [SerializeField] private float speed;
    [SerializeField] private float force;
    [SerializeField] private float radius;

    //bullet Travel
    [SerializeField] private Rigidbody bulletRb;
    [SerializeField] private float bulletTravelForce;

    // Start is called before the first frame update
    void Start()
    {
        bulletRb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        bulletRb.AddForce(Vector3.forward * bulletTravelForce);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRB = collision.gameObject.GetComponent<Rigidbody>();
            playerRB.AddExplosionForce(force, Vector3.back, radius);
        }
        else if (collision.gameObject.CompareTag("Turret"))
        {

        }
        else
        {
            GameObject.Destroy(gameObject);
        }

    }

}
