using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Asteroid : MonoBehaviour
{
  public float rotationSpeed = 1.0f;
  public float asteroidSize = 1.0f;
  public float minAsteroidSize = 0.5f;
  public float maxAsteroidSize = 1.5f;
  public float speed = 5;
  [SerializeField] private float asteroidLifeTime = 30f;
  private Rigidbody2D rb;
  private SpriteRenderer spriteRenderer;
  public Sprite[] sprites;

  void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  void Start()
  {
    spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    this.transform.eulerAngles = new Vector3(0, 0, Random.value * 360);
    this.transform.localScale = Vector3.one * this.asteroidSize;
    rb.mass = this.asteroidSize * 2;
  }

  public void SetProjectory(Vector2 direction)
  {
    rb.AddForce(direction * speed);
    Destroy(this.gameObject, this.asteroidLifeTime);
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "Bullet")
    {
      if ((this.asteroidSize * 0.5f) > this.minAsteroidSize)
      {
        CreateSplit();
        CreateSplit();
      }
      FindObjectOfType<GameManager>().AsteroidDestroyed(this);
      Destroy(this.gameObject);
    }
  }

  private void CreateSplit()
  {
    Vector2 position = this.gameObject.transform.position;
    position += Random.insideUnitCircle * 0.5f;
    Asteroid halfPrevAsteroid = Instantiate(this, position, this.transform.rotation);
    halfPrevAsteroid.asteroidSize = this.asteroidSize * 0.5f;
    halfPrevAsteroid.SetProjectory(Random.insideUnitCircle.normalized * this.speed);
  }
}
