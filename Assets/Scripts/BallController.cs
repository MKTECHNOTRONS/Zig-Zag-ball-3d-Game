using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private bool gameOver;
    private bool started;
    private Rigidbody rb;

    public GameObject particle; // Assign a particle prefab with ParticleSystem in Inspector

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        started = false;
        gameOver = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!started)
            {
                rb.velocity = new Vector3(speed, 0, 0);
                started = true;
            }
            else if (!gameOver)
            {
                SwitchDirection();
            }
        }

        // Draw ground check ray
        Debug.DrawRay(transform.position, Vector3.down * 1.1f, Color.red);

        // Check if ball is falling (not on platform)
        if (!Physics.Raycast(transform.position, Vector3.down, 1.1f))
        {
            if (!gameOver)
            {
                gameOver = true;
                rb.velocity = new Vector3(0, -25f, 0);
                Camera.main.GetComponent<CameraFollow>().gameOver = true;
            }
        }
    }

    void SwitchDirection()
    {
        if (rb.velocity.x > 0)
        {
            rb.velocity = new Vector3(0, 0, speed); // Switch to Z direction
        }
        else if (rb.velocity.z > 0)
        {
            rb.velocity = new Vector3(speed, 0, 0); // Switch to X direction
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Diamond"))
        {
            // Instantiate and play particle system
            GameObject part = Instantiate(particle, col.transform.position, Quaternion.identity);

            ParticleSystem ps = part.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Play();
            }

            Destroy(col.gameObject); // Remove diamond
            Destroy(part, 2f);       // Clean up particle after 2 seconds
        }
    }
}
