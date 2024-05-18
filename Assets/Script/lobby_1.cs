using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class lobby_1 : MonoBehaviour
{
    public void lobby(string name)
    {
        SceneManager.LoadScene(name);

    }
}
