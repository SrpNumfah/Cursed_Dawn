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

    [Header("MoveMent")]
    public float speed = 6f;
    [SerializeField] CharacterController characterController;
    
    

    [Header("Attack")]
    public float attackSpeed;
    public float attackRange = 0.5f;
    public int attackDamage = 5;
    public Transform attackPoint;
    public LayerMask enemy;
    public Animator voiceAnim;

    
  //  [SerializeField] HUD_Manager hud_Manager;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
       // hud_Manager = FindObjectOfType<HUD_Manager>();

        actions.Add("walk", Walk);
        actions.Add("left", TurnLeft);
        actions.Add("right", TurnRight);
        actions.Add("back", Back);
        actions.Add("attack", Attack);
        actions.Add("healing", Healing);

       

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
        MoveByVoice(new Vector3(0, 0, -20));
    }
    private void Back()
    {
        MoveByVoice(new Vector3(0, 0, 20));
    }
    private void TurnLeft()
    {
        MoveByVoice(new Vector3(-20, 0, 0));
    }

    private void TurnRight()
    {
        MoveByVoice(new Vector3(20, 0, 0));
    }

   
   public void MoveByVoice(Vector3 direction)
    {
        if (direction.magnitude >= 0.1f)
        {
            characterController.Move(direction * speed * Time.deltaTime);


            if (direction.x > 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            } 
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
        else
        {
             voiceAnim.SetTrigger("Isrun");
            // walkDust.Stop();
        }

        characterController.Move(Physics.gravity * Time.deltaTime);
    }

    private void Attack()
    {
        voiceAnim.SetTrigger("Attack");
    }

    private void Healing()
    {
       
    }

}
