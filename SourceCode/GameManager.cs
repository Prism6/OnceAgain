using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("게임 규칙")]
    public float goalScore;
    public float maxFallCount;

    [Header("UI 요소")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI fallcountText;

    [Header("씬 오브젝트")]
    public GameObject nextStage;
    public GameObject youSuck;
    public GameObject retryStage;

    [Header("게임 진행 변수")]
    public float score;
    public float fallcount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (score == goalScore)
        {
            nextStage.SetActive(true);
        }

        if (fallcount == maxFallCount)
        {
            youSuck.SetActive(true);
            retryStage.SetActive(true);
        }
    }

    public void PlusScore()
    {
        score++;
        scoreText.text = $"Score : {score}";
    }

    public void FallCount()
    {
        fallcount++;
        fallcountText.text = $"Fall Count : {fallcount}";
    }
}