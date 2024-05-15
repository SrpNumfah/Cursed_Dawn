using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Windows.Speech;

public class VoiceMoveMent : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    public float moveSpeed = 1.0f;
    public float turnSpeed = 50.0f;

    private void Start()
    {
        actions.Add("walk", Walk);
        actions.Add("turnleft", TurnLeft);
        actions.Add("turnright", TurnRight);
        actions.Add("back", Back);
        // actions.Add("attack", Attack);


        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizVoice;
        keywordRecognizer.Start();
    }

    void RecognizVoice(PhraseRecognizedEventArgs speece)
    {
        Debug.Log(speece.text);
        actions[speece.text].Invoke();
    }

    private void Walk()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void TurnLeft()
    {
        transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
    }

    private void TurnRight()
    {
        transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
    }

    private void Back()
    {
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
    }

}
