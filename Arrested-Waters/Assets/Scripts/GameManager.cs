using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pathfinding;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isDead;
    public PlayerController player;
    public BoatController boat;
    public CameraFollowTarget mainCamera;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this) // If there is already an instance and it's not `this` instance
        {
            Destroy(gameObject); // Destroy the GameObject, this component is attached to
        }
        AstarPath.active.logPathResults = PathLog.None;
    }

    private void OnEnable()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        boat = GameObject.Find("Boat").GetComponent<BoatController>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<CameraFollowTarget>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        boat = GameObject.Find("Boat").GetComponent<BoatController>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<CameraFollowTarget>();
    }
        void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void SetBoat(BoatController newBoat)
    {
        boat = newBoat;
    }

    public void OnDeath()
    {
        isDead = false;
        SceneManager.LoadScene("MapCreation");
    }

    public void EndGame()
    {
        SceneManager.LoadScene("EndGame");
    }

}
