using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_scoreText;
    [SerializeField]
    private TextMeshProUGUI m_GameOver;
    [SerializeField]
    private GameObject[] moons;
    [SerializeField]
    private GameObject[] glares;
    [SerializeField]
    private GameObject m_day_image;
    [SerializeField]
    private GameObject m_night_image;
    [SerializeField]
    private GameObject m_lifes_glares;
    [SerializeField]
    private GameObject m_lifes_moons;
    [SerializeField]
    private GameObject background_day;
    [SerializeField]
    private GameObject background_night;
    [SerializeField]
    private TextMeshProUGUI night_image_text;
    [SerializeField]
    private TextMeshProUGUI day_image_text;

    private PlayerType type;
    private string Night = "Night";
    private string Day = "Day";
    private string TimeOfDay;
    private int score = 0;
    private int index = 0;

    private void Awake()
    {
        type = NewBehaviourScript.Instance.GetPlayerType();

        PrepareScene(type);
    }

    private void PrepareScene(PlayerType type)
    {
        if (type.Equals(PlayerType.MOONY))
        {
            TimeOfDay = Night;
            m_night_image.SetActive(true);
            m_lifes_moons.SetActive(true);
            background_night.SetActive(true);
        }
        else
        {
            TimeOfDay = Day;
            m_day_image.SetActive(true);
            m_lifes_glares.SetActive(true);
            background_day.SetActive(true);
        }
    }

    internal void AddPoints(int value)
    {
        score += value;

        m_scoreText.text = "Score: " + score;
    }

    internal void LifeMinus()
    {
        if (type.Equals(PlayerType.MOONY))
        {
            if(index < moons.Length)
            {
                moons[index].SetActive(false);
            }

            index++;
        }
        else
        {
            if(index < glares.Length)
            {
                glares[index].SetActive(false);
            }
            index++;
        }
    }

    internal void ShowGameOver()
    {
        m_GameOver.gameObject.SetActive(true);
    }

    internal void ChangeEnvironment(string timeofday)
    {
        TimeOfDay = timeofday;

        if (timeofday.Equals(Night))
        {
            background_night.SetActive(true);
            m_night_image.SetActive(true);
            background_day.SetActive(false);
            m_day_image.SetActive(false);
        }
        else
        {
            background_night.SetActive(false);
            m_night_image.SetActive(false);
            background_day.SetActive(true);
            m_day_image.SetActive(true);
        }
    }

    internal void ShowTimer()
    {
        StartCoroutine(nameof(Timer));
    }

    private IEnumerator Timer()
    {
        int count = 15;

        while(count >= 0)
        {
            yield return new WaitForSeconds(1);

            if (TimeOfDay.Equals(Night))
            {
                night_image_text.text = "Sunset in: " + count;
            }
            else
            {
                day_image_text.text = "Dawn in: " + count;
            }

            count--;
        }
    }
}
