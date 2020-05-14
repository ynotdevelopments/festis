﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionCreatorManager : MonoBehaviour
{
    
    public GameObject content;
    public GameObject panelQuestion;

    public TMP_Text textNickname;

    TouchScreenKeyboard keyboard;

    void Start()
    {
        textNickname.text = SaveData.LoadNickname();
    }

    public int GetLastCustomId()
    {
        int lastId = 0;
        foreach(Question question in GlobalVariables.questionList)
            if(question.id < lastId) lastId = question.id;
        return lastId;
    }

    public void ShowExistingQuestions()
    {
        foreach(Question question in GlobalVariables.questionList)
        {
            if(question.id<0)
            {
                GameObject card = CreateQuestionCard();
                card.GetComponent<QuestionManager>().SetQuestionData(question);
            }
        }
    }

    public void CreateQuestion()
    {
        
        //add card to scene 
        GameObject card = CreateQuestionCard();

        //add question to local database
        Question question = new Question(GetLastCustomId()-1, "", 0, "custom", "french");

        //set created question attributes
        card.GetComponent<QuestionManager>().SetQuestionData(question);
        
        //open keyboard and to type question
        card.GetComponent<QuestionManager>().StartEditQuestion();   
    }

    public GameObject CreateQuestionCard()
    {
        GameObject card = Instantiate(panelQuestion) as GameObject; 
        card.transform.SetParent(content.transform, false);
        return card;
    }


    public void OpenKeyboard()
    {
        GlobalVariables.NICKNAME = "AAAAAAAA";
        textNickname.text = GlobalVariables.NICKNAME;
        SaveData.SaveNickname();
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }

    void Update()
    {
        if(TouchScreenKeyboard.visible == false && keyboard != null)
        {
            if(keyboard.status == TouchScreenKeyboard.Status.Done)
            {
                GlobalVariables.NICKNAME = keyboard.text;
                textNickname.text = GlobalVariables.NICKNAME; 
            }
        }
    }   
}
