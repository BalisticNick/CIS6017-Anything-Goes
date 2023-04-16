using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneSystem
{

    public class SceneDirector : MonoBehaviour
    {
        private static List<string> challenges;
        [SerializeField] public List<string> ChallengeSceneNames;
        [SerializeField] public TextMeshProUGUI Message;
        public string message = "";
        private static int index = 0;

        public static void LoadRandomScene()
        {

            int r = Random.Range(0, challenges.Count);

            SceneManager.LoadScene(challenges[r]);
            Debug.Log(challenges[r]);
        }

        public static void LoadChallenge()
        {
            SceneManager.LoadScene(challenges[index]);
        }

        private void Awake()
        {
            message = PlayerPrefs.GetString("message");
            if (Message != null)
            {
                Message.text = message;
            }
        }

        private void FixedUpdate() => challenges = ChallengeSceneNames;


        public static void MainMenu() => SceneManager.LoadScene("MainMenu");

        public static void StartGame() => SceneManager.LoadScene("Warehouse 1");


        public static void WinGame(string message = "")
        {
            PlayerPrefs.SetString("message", message);
            SceneManager.LoadScene("WinGame");
        }

        public static void LoseGame(string message = "")
        {
            PlayerPrefs.SetString("message", message);
            SceneManager.LoadScene("LoseGame");
        }
    }
}