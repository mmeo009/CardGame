using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMonster : MonoBehaviour
{
    public GameObject player; // �÷��̾� ������Ʈ�� ����Ű�º��� ����
    public float attackDelay = 2.0f; // ���� ������
    public int numberOfAttacks = 2; // 2���� ����

    private bool isAttacking = false;

    private void Start()
    {
        // ������ �ֱ������� �ϴ� �ڷ�ƾ ����
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        while (true) // ���ѹݺ� ����
        {
            // �÷��̾ ���ϰ� ȸ�� ����
            Vector3 direction = (player.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);

            // ������ �����ϱ� ���� ��ⱸ��
            yield return new WaitForSeconds(attackDelay);

            // ���� ���౸��
            for (int i = 0; i < numberOfAttacks; i++)
            {
                Attack();
                yield return new WaitForSeconds(0.5f); // ���� ����
            }
        }
    }

    private void Attack()
    {
        //���� ��������� �ۼ�
      
        Debug.Log("���� ���� �޾ƶ�.");
    }
}


