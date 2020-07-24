using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {
    public Animator transition;
    public float transitionTime = 1f;

    public void LoadNextLevel() {
      StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadStart() {
      StartCoroutine(LoadLevel(0));
    }

    IEnumerator LoadLevel(int levelIndex) {
      transition.SetTrigger("Start");
      yield return new WaitForSeconds(transitionTime);
      SceneManager.LoadScene(levelIndex);
    }

    void OnEnable() {
      TDEvents.GameOver.AddListener(LoadStart);
    }

    void OnDisable() {
      TDEvents.GameOver.RemoveListener(LoadStart);
    }
}
