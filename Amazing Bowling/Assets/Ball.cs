using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public LayerMask whatIsProp;
    public ParticleSystem explosionParticle;
    public AudioSource explosionAudio;

    public float maxDamage = 100f;
    public float explosionForce = 1000f;
    public float lifeTime = 10f;
    public float explosionRadius = 20f;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position,explosionRadius,whatIsProp);
        for(int i = 0; i < colliders.Length; i++)
        {
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

            targetRigidbody.AddExplosionForce(explosionForce,transform.position,explosionRadius);

            Prop targetProp = colliders[i].GetComponent<Prop>();

            float damage = CalculateDamage(colliders[i].transform.position);

             targetProp.TakeDamage(damage); 
        }
        explosionParticle.transform.parent = null;
        explosionParticle.Play();
        explosionAudio.Play();

        Destroy(explosionParticle.gameObject,explosionParticle.duration);
        Destroy(gameObject);

    }

    private float CalculateDamage(Vector3 targetPosition)
    {
        //자신의 위치에서 타겟까지의 벡터
        Vector3 explosionToTarget = targetPosition - transform.position;
        //자신의 위치에서 타겟까지의 거리
        float distance = explosionToTarget.magnitude;
        //폭팔반경 끝에서부터 물체가 얼마나 떨어져있는지 
        float edgeToCenterDistance = explosionRadius - distance;
        //구의 중심으로 갈수록 100%, 원의 둘레로 갈수록 0% 데미지를 입는다.
        float percentage = edgeToCenterDistance/explosionRadius;
        float damage = maxDamage * percentage;
        
        //폭발반경에 살짝걸치는 물체가 있을경우 데미지가 -로 들어가 체력이 회복될수있기때문에 이를 방지
        damage = Mathf.Max(0,damage);

        return damage;
    }
}
