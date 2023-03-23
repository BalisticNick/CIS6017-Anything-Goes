using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneSystem
{

    public class SceneDirector : MonoBehaviour
    {
        private static List<string> challenges;
        [SerializeField] public List<string> ChallengeSceneNames;
        public static void LoadRandomScene()
        {

            int r = Random.Range(0, challenges.Count);

            SceneManager.LoadScene(challenges[r]);
            Debug.Log(challenges[r]);
        }

        private void FixedUpdate()
        {
            challenges = ChallengeSceneNames;
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