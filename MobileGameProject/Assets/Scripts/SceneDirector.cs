using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneDirector
{

    public class SceneDirector : MonoBehaviour
    {
        [SerializeField] List<string> challenges;
        public void LoadRandomScene()
        {

            int r = Random.Range(0, challenges.Count);

            SceneManager.LoadScene(challenges[r]);
            Debug.Log(challenges[r]);
        }

        public void MainMenu()
        {
            SceneManager.LoadScene("Main Menu");
        }

        public void StartGame()
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}