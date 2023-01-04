using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
  [SerializeField] private float rotationSpeed = 1.0f;
  public float asteroidSize = 1.0f;
  [SerializeField] private float minAsteroidSize = 0.5f;
  [SerializeField] private float maxAsteroidSize = 1.5f;
  private Rigidbody2D rb;
  private SpriteRenderer spriteRenderer;
  private Sprite[] sprites;

  private void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  private void Start()
  {
    spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    this.transform.eulerAngles = new Vector3(0, 0, Random.value * 360);
    this.transform.localScale = Vector3.one * this.asteroidSize;
    rb.mass = this.asteroidSize;
  }
}
