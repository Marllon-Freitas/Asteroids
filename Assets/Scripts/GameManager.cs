using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  public Player player;
  public Text scoreText;
  public MenuManager menuManager;
  public Text livesText;
  public ParticleSystem explosion;
  public int lives = 3;
  public float respawnTime = 2.0f;
  public float invulnerabilityTime = 2.0f;
  public bool isGameOver = false;

  public int score = 0;

  public void AsteroidDestroyed(Asteroid asteroid)
  {
    this.explosion.transform.position = asteroid.transform.position;
    this.explosion.Play();
    if (asteroid.asteroidSize < 0.75f)
    {
      this.score += 100;
    }
    else if (asteroid.asteroidSize < 1.5f)
    {
      this.score += 50;
    }
    else
    {
      this.score += 25;
    }

    this.scoreText.text = this.score.ToString();
  }

  public void PlayerDie()
  {
    this.explosion.transform.position = this.player.transform.position;
    this.explosion.Play();
    this.lives--;
    if (this.lives >= 0)
    {
      this.livesText.text = this.lives.ToString();
    }
    if (this.lives <= 0)
    {
      this.GameOver();
    }
    else
    {
      Invoke(nameof(Respawn), this.respawnTime);
    }
  }

  public void Respawn()
  {
    this.player.transform.position = Vector3.zero;
    this.player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
    this.player.gameObject.SetActive(true);
    Invoke(nameof(TurnOnCollision), this.invulnerabilityTime);
  }

  private void TurnOnCollision()
  {
    this.player.gameObject.layer = LayerMask.NameToLayer("Player");
  }

  private void GameOver()
  {
    this.isGameOver = true;
    this.menuManager.gameObject.SetActive(true);
  }
}
