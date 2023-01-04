using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  private Rigidbody2D rb;
  public MenuManager menuManager;
  private bool isTrusting;
  [SerializeField] private float thrustSpeed = 1.0f;
  [SerializeField] private float turnSpeed = 1.0f;
  private float turnDirection;
  public Bullet bulletPrefab;
  public GameManager gameManager;

  private void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  void Update()
  {

    if (Input.GetKey(KeyCode.R))
    {
      this.menuManager.Restart();
    }

    isTrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
    
    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
    {
      turnDirection = 1;
    }
    else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
    {
      turnDirection = -1;
    }
    else
    {
      turnDirection = 0;
    }

    if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
    {
      Shoot();
    }
  }

  private void FixedUpdate()
  {
    if (isTrusting)
    {
      rb.AddForce(this.transform.up * this.thrustSpeed);
    }
    if (turnDirection != 0.0f)
    {
      rb.AddTorque(this.turnDirection * this.turnSpeed);
    }
  }

  private void Shoot()
  {
    Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
    bullet.Project(this.transform.up);
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "Asteroid")
    {
      rb.velocity = Vector3.zero;
      rb.angularVelocity = 0;
      this.gameObject.SetActive(false);
      this.gameManager.PlayerDie();
    }
  }
}
