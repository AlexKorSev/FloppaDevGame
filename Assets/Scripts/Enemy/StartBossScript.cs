using System.Collections;
using UnityEngine;

public class StartBossScript : MonoBehaviour
{
    public float startDelay;

    [SerializeField] private GameObject startGate;
    [SerializeField] private GameObject boss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            startGate.SetActive(true);
            boss.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    //private IEnumerator StartFight()
    //{
    //    yield return new WaitForSeconds(startDelay);

    //    startGate.SetActive(true);
    //    boss.SetActive(true);
    //    this.gameObject.SetActive(false);
    //}
}
