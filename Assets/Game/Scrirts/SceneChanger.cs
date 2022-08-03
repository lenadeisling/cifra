using UnityEngine;
using UnityEngine.SceneManagement;

namespace SortItems
{
    public class SceneChanger: MonoBehaviour {

        private void Start() {
            var level = PlayerPrefs.GetInt("Level", 0);
            if (level != SceneManager.GetActiveScene().buildIndex) {
                LoadLevel(level);
            }
        }

        public void LoadLevel(int levelIdx) {
            var sceneCount = SceneManager.sceneCountInBuildSettings;
            var nextLevel = (levelIdx) % sceneCount;
            SceneManager.LoadScene(nextLevel);
        }
        public void LoadNextLevel() {
            var currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
            var sceneCount = SceneManager.sceneCountInBuildSettings;
            var nextLevel = (currentSceneIdx + 1) % sceneCount;
            PlayerPrefs.SetInt("Level", nextLevel);               
            SceneManager.LoadScene((currentSceneIdx+1) % sceneCount);
            
        }

        public void ReloadScene() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void Update(){
            if(Input.GetKeyUp(KeyCode.R)){
                ReloadScene();
            }
        }
    }

}
