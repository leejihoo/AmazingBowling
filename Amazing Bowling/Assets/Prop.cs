using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    //파괴될때마다 증가될 점수
    public int score = 5;
    public ParticleSystem explosionParticle;
    public float hp = 10f;

    //외부에서 prop에 데미지를  받으라고 전달하는 함수
    public void TakeDamage(float damage)
    {
        hp -= damage;
        
        if(hp <= 0)
        {
            ParticleSystem instance = Instantiate(explosionParticle, transform.position, transform.rotation);

            AudioSource explosionAudio = instance.GetComponent<AudioSource>();
            explosionAudio.Play();
            Destroy(instance.gameObject, instance.duration);
            // 인스턴스를 삭제,생성 반복할 경우 메모리낭비가 크기때문에 보이지 않게만 해놓는다.
            gameObject.SetActive(false);

        }
     }

}
