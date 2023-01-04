using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
  public void Restart()
  {
    this.gameObject.SetActive(false);
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
}