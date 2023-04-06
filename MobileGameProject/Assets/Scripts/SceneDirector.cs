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

        private void FixedUpdate() =>  challenges = ChallengeSceneNames;


        public static void MainMenu() => SceneManager.LoadScene("Main Menu");      
            
        public static void StartGame() => SceneManager.LoadScene("Warehouse 1");


        public static void WinGame() => SceneManager.LoadScene("");

        public static void LoseGame() => SceneManager.LoadScene("");
    }
}