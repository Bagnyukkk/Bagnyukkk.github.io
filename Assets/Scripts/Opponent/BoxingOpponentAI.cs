using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BoxingOpponentAI : MonoBehaviour
{
    public Transform player;
    private Animator animator;

    private bool isAttacking = false;
    private bool isBlocking = false;
    private bool isStunned = false;
    private bool hasDied = false;
    private bool hittedRecently = false;

    public float moveSpeed = 2f;
    public float strafeSpeed = 1.5f;

    public float attackRange = 2f;
    public float blockProbability = 0.5f;

    public float rotationSpeed = 5f;

    public float maxHealthPoints = 100f;
    public float healthPoints = 100f;
    public Image healthBar;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isStunned || hasDied) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > attackRange)
        {
            MoveTowardsPlayer();
        }
        else
        {
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveZ", 0);
            DecideCombatAction();

            if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && !isBlocking)
            {
                if (!hittedRecently)
                    GetHit(10f);
            }
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Vector3 movement = new Vector3(direction.x * strafeSpeed, 0, direction.z * moveSpeed);
        transform.Translate(movement * Time.deltaTime, Space.World);

        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveZ", direction.z);
    }

    void DecideCombatAction()
    {
        if (!isAttacking && !isBlocking)
        {
            if (Random.value < blockProbability)
                StartBlocking();
            else
                StartCoroutine(PunchRoutine());
        }
    }

    IEnumerator PunchRoutine()
    {
        isAttacking = true;
        animator.SetBool("IsPunching", true);
        int punchSide = Random.Range(0, 2);
        animator.SetInteger("PunchSide", punchSide);

        float punchDuration = 0.5f;
        float damageMoment = 0.3f;
        float elapsedTime = 0f;
        bool damageDealt = false;

        while (elapsedTime < punchDuration)
        {
            if (hasDied || isStunned || isBlocking || hittedRecently)
            {
                // Interrupted punch
                animator.SetBool("IsPunching", false);
                isAttacking = false;
                yield break;
            }

            elapsedTime += Time.deltaTime;

            if (!damageDealt && elapsedTime >= damageMoment)
            {
                damageDealt = true;
                if (Vector3.Distance(transform.position, player.position) <= attackRange)
                    player.GetComponent<Player>().GetHit(PlayerPrefs.GetInt("DamageOnPlayer"));
            }

            yield return null;
        }

        animator.SetBool("IsPunching", false);
        isAttacking = false;
    }

    void StartBlocking()
    {
        isBlocking = true;
        animator.SetBool("IsBlocking", true);
        StartCoroutine(StopBlockingAfterDelay(Random.Range(1f, 2f)));
    }

    IEnumerator StopBlockingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isBlocking = false;
        animator.SetBool("IsBlocking", false);
    }

    public void GetHit(float damage)
    {
        if (hasDied || isStunned) return;

        hittedRecently = true;
        animator.SetTrigger("Hit");
        StartCoroutine(GetDamageRoutine(damage));
        Invoke(nameof(EndHitted), 0.5f);

        if (Random.value < 0.05f)
            StartStunned();
    }

    IEnumerator GetDamageRoutine(float damage)
    {
        yield return new WaitForSeconds(0.4f);

        float targetHealth = healthPoints - damage;
        float time = 0;
        float duration = 2f;

        while (time <= duration)
        {
            time += Time.deltaTime;
            healthPoints = Mathf.Lerp(healthPoints, targetHealth, time / duration);
            healthBar.fillAmount = healthPoints / maxHealthPoints;

            if (healthPoints <= 0)
            {
                Die();
                yield break;
            }

            yield return null;
        }
    }

    void Die()
    {
        hasDied = true;
        animator.SetTrigger("Die");
        GameUIManager.Instance.ChangeEndScreenTextColor(Color.green);
        GameUIManager.Instance.ChangeEndScreenText("You won");
        GameUIManager.Instance.ShowEndScreen();
        Debug.Log("Enemy died");
    }

    void StartStunned()
    {
        isStunned = true;
        animator.SetBool("IsStunned", true);
        StartCoroutine(EndStunAfterDelay(3f));
    }

    IEnumerator EndStunAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isStunned = false;
        animator.SetBool("IsStunned", false);
    }

    void EndHitted()
    {
        hittedRecently = false;
    }
}
