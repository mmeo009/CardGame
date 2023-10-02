using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMonster : MonoBehaviour
{
    public GameObject player; // 플레이어 오브젝트를 가리키는변수 구현
    public float attackDelay = 2.0f; // 공격 딜레이
    public int numberOfAttacks = 2; // 2번의 공격

    private bool isAttacking = false;

    private void Start()
    {
        // 공격을 주기적으로 하는 코루틴 구현
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        while (true) // 무한반복 실행
        {
            // 플레이어를 향하게 회전 구현
            Vector3 direction = (player.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);

            // 공격을 시작하기 전에 대기구현
            yield return new WaitForSeconds(attackDelay);

            // 공격 실행구현
            for (int i = 0; i < numberOfAttacks; i++)
            {
                Attack();
                yield return new WaitForSeconds(0.5f); // 공격 간격
            }
        }
    }

    private void Attack()
    {
        //공격 로직만들면 작성
      
        Debug.Log("나의 검을 받아랏.");
    }
}


