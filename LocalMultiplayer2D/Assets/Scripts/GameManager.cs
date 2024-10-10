using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public InputManager inputManager { get; private set; }
    public UIManager uiManager;

    private List<PlayerInput> players = new List<PlayerInput>();
    [SerializeField] private List<Transform> initialSpawnPoints;
    [SerializeField] private List<LayerMask> playerLayers;
    [SerializeField] private List<string> playerIdentification;

    private PlayerInputManager playerInputManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        inputManager = new InputManager();
        playerInputManager = GetComponent<PlayerInputManager>();
    }

    public void EnterScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    private void AddPlayer(PlayerInput player)
    {
        players.Add(player);

        player.transform.position = initialSpawnPoints[players.Count - 1].position;

        int layerToAdd = (int)Mathf.Log(playerLayers[players.Count - 1].value, 2);
        print("layer to add " + layerToAdd);

        player.gameObject.layer = layerToAdd;
        //player.GetComponentInChildren<CinemachineVirtualCamera>().gameObject.layer = layerToAdd;
        //player.GetComponentInChildren<Camera>().cullingMask |= 1 << layerToAdd;
    }

    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += AddPlayer;
    }

    private void OnDisable()
    {
        playerInputManager.onPlayerJoined -= AddPlayer;
    }
}
