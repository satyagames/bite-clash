using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
   
    public int score { get; private set; }
    private float spawnRate = 1f;
    public int lives { get; private set; }
    

    public static bool isGameActive { get; private set; }  

    private int difficulty { get; set; } // ENCAPSULATION

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        spawnRate /= difficulty;

        StartCoroutine(SpawnTarget());                   
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
        
    }

    public virtual void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;        
    }

    public virtual void UpdateLives(int livesToChange)
    {
        lives += livesToChange;        
    }

    public virtual void GameOver()
    {
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// The public method is called from Button click in Inspector and value is set there as parameter
    /// </summary>
    /// <param name="value"></param>
    public void SetDifficulty(int value)
    {
        difficulty = value; // Set value
        StartGame(difficulty); // Get value
    }
}
