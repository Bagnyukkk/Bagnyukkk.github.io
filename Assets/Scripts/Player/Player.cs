using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public bool hasDied = false;
    [SerializeField] private BoxingOpponentAI opponent;

    public float maxHealthPoints = 100f;
    public float healthPoints = 100f;
    public Image healthBar;

    public bool isInBlock;

    private bool hittedRecently = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void GetHit(float damage)
    {
        if (!isInBlock)
        {
            hittedRecently = true;
            StartCoroutine(GetDamageRoutine(damage));
        }
    }

    IEnumerator GetDamageRoutine(float damage)
    {
        yield return new WaitForSeconds(0.4f);
        float targetHealth = healthPoints - damage;
        float time = 0;
        float duration = 2f;
        while (time <= duration)
        {
            time = time + Time.deltaTime;
            healthPoints = Mathf.Lerp(healthPoints, targetHealth, time / duration);
            healthBar.fillAmount = healthPoints / maxHealthPoints;
            if (healthPoints <= 0)
            {
                Die();
                break;
            }
            yield return null;
        }
    }

    void Die()
    {

        GameUIManager.Instance.ChangeEndScreenTextColor(Color.red);
        GameUIManager.Instance.ChangeEndScreenText("You lost");
        GameUIManager.Instance.ShowEndScreen();
        hasDied = true;
        opponent.enabled = false;
        Destroy(GetComponent<Rigidbody>());
        GetComponent<Animator>().enabled = true;
        GetComponent<Animator>().SetTrigger("Death");
    }
}
