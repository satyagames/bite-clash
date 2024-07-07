using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -2;

    private UIHandler uIHandler;

    public int pointValue;
    public ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {

        uIHandler = GameObject.Find("UI Handler").GetComponent<UIHandler>();
        targetRb = GetComponent<Rigidbody>();

        Move(); // Abstraction or Code refactoring

        //create random object to spawn
        transform.position = RandomSpawnPos();
    }

    private void Move()
    {
        // Add force to move the object
        targetRb.AddForce(RandomMove(minSpeed, maxSpeed), ForceMode.Impulse);

        //Rotate object and we use random values on all axes

        targetRb.AddTorque(RandomMove(maxTorque), RandomMove(maxTorque), RandomMove(maxTorque), ForceMode.Impulse);
    }

    /// <summary>
    /// Destroy objects when you click them and show explosion effects. Also the score is increased based on what object has been clicked.
    /// </summary>
    private void OnMouseDown()
    {
        if (GameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);

            if (uIHandler != null)
            {
                uIHandler.UpdateScore(pointValue);
            }
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if (!gameObject.CompareTag("Bad") && GameManager.isGameActive && uIHandler != null)
        {
            uIHandler.UpdateLives(-1);
        }
        
    }

    #region POLYMORPHISM. The method is overloaded with different parameters and return type

    /// <summary>
    /// Moves the target within a certain range
    /// </summary>
    /// <param name="minSpeed"></param>
    /// <param name="maxSpeed"></param>
    /// <returns></returns>
    private Vector3 RandomMove(float minSpeed, float maxSpeed)
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    /// <summary>
    /// Rotates the target
    /// </summary>
    /// <param name="torque"></param>
    /// <returns></returns>
    private float RandomMove(float torque)
    {
        return Random.Range(-torque, torque);
    }

    #endregion

    private Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
}
