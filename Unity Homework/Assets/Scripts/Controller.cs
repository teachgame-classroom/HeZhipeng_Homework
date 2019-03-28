using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour {

    private ScoreModel scoreModel;
    private string saveFilePath;

	// Use this for initialization
	void Start () {
        saveFilePath = Application.persistentDataPath + "/save.sav";
        Load();
        DisplayScore();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddScore(int score)
    {
        if (score <= 0)
        {
            return;
        }

        scoreModel.score += score;
        scoreModel.level = scoreModel.score / 100 + 1;
        Save();
        DisplayScore();
    }

    private void DisplayScore()
    {
        Text scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        scoreText.text = "Score =" + scoreModel.score.ToString();

        Text levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text = "Level =" + scoreModel.level.ToString();
    }

    private void Load()
    {
        scoreModel = (ScoreModel)FileTool.ReadSaveFile(saveFilePath);
        if(scoreModel == null)
        {
            scoreModel = new ScoreModel();
        }
    }

    private void Save()
    {
        FileTool.WriteSaveFile(saveFilePath, scoreModel);
    }
}
