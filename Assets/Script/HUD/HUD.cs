using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    
    private void Awake()
    {
        SceneManager.LoadScene("HUD", LoadSceneMode.Additive);
    }

    

}
