using UnityEngine;

public class bananacat : MonoBehaviour
{
    public GameObject bananacat2; // 복제할 오브젝트

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("TriggerAsset")) // 충돌한 오브젝트가 트리거에 속하는지 확인하여 복제 오브젝트를 생성
        {
            // 복제 오브젝트 생성
            Instantiate(bananacat2, transform.position, transform.rotation);

            // 현재 오브젝트 삭제
            Destroy(gameObject);
        }
    }
}
