using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Win Panel")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TextMeshProUGUI winText;


    private void Start()
    {
        GameManager.instance.uiManager = this;
        winPanel.SetActive(false);
    }

    public void EnableWinPanel(string winnerPlayer)
    {
        winPanel.SetActive(true);
        winText.text = winnerPlayer + " WIN!";
    }
}
