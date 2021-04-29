using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int playerKillCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerKillCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerKillCount >= 10)
            SceneManager.LoadScene("Level2");
    }
}
