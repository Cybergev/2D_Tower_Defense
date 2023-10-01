using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatistics : MonoBehaviour
{
    //public int numKills;
    public int gold;
    public int time;

    public void Reset()
    {
        //numKills = 0;
        gold = 0;
        time = 0;
    }
    public void CalculateLevelStatistic()
    {
        //numKills = Player.Instance.NumKills;
        gold = TDPlayer.Instance.NumGold;
        time = (int)LevelController.Instance.LevelTime;
    }
    public void SaveLevelStatistic()
    {
        /*
        if (PlayerPrefs.GetInt("SpaceShooter:MaxKills") < numKills)
        {
            PlayerPrefs.SetInt("SpaceShooter:MaxKills", numKills);
        }
        if (PlayerPrefs.GetInt("SpaceShooter:MaxScore") < score)
        {
            PlayerPrefs.SetInt("SpaceShooter:MaxScore", score);
        }
        if (PlayerPrefs.GetInt("SpaceShooter:MinTime") > time || PlayerPrefs.GetInt("SpaceShooter:MinTime") == 0)
        {
            PlayerPrefs.SetInt("SpaceShooter:MinTime", time);
        }
        */
    }
}