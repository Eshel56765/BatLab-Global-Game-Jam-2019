using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProblemTrigger : MonoBehaviour
{
    public GameObject MiniGamePrefab;
    private Collider problemCollider;

    private GameObject CurrentGame;

    // Start is called before the first frame update
    void Start()
    {
        problemCollider = gameObject.GetComponent<Collider>();
    }

    private void OnTriggerStay(Collider Col)
    {
        if (Col.CompareTag("Player") && Input.GetKeyDown(KeyCode.F) && CurrentGame == null)
        {
            CurrentGame = Instantiate(MiniGamePrefab, GameManger.Instance.UICanvas);
        }
    }
}
