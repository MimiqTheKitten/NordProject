using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveListDoer : MonoBehaviour
{
    [SerializeField] GameObject hitBox;

    bool isHitting = false;
 
    //Normals
    public void LeftNormal()
    {
        if (isHitting)
        {
            return;
        }
        isHitting = true;
        StartCoroutine(LeftNormalIE(0.3f));
    }
    public void RightNormal()
    {
        if (isHitting)
        {
            return;
        }
        isHitting = true;
        StartCoroutine(RightNormalIE(0.3f));
    }
    IEnumerator LeftNormalIE(float hitlag)
    {
        HitBoxes(new Vector3(-0.8f,0,0), 10, .4f,1.1f,0.8f);
        yield return new WaitForSeconds(hitlag);

        isHitting = false;
    }
    IEnumerator RightNormalIE(float hitlag)
    {
        HitBoxes(new Vector3(0.8f, 0, 0), 10, .4f, 1.1f, 0.8f);
        yield return new WaitForSeconds(hitlag);

        isHitting = false;
    }
    //Aerials
    public void LeftAir()
    {
        if (isHitting)
        {
            return;
        }
        isHitting = true;
        StartCoroutine(LeftAirIE(0.15f));
    }
    public void RightAir()
    {
        if (isHitting)
        {
            return;
        }
        isHitting = true;
        StartCoroutine(RightAirIE(0.15f));
    }
    IEnumerator LeftAirIE(float hitlag)
    {
        HitBoxes(new Vector3(-0.8f, -0.8f, 0), 10, .4f, 1.1f, 0.8f);
        yield return new WaitForSeconds(hitlag);

        isHitting = false;
    }
    IEnumerator RightAirIE(float hitlag)
    {
        HitBoxes(new Vector3(0.8f, -0.8f, 0), 10, .4f, 1.1f, 0.8f);
        yield return new WaitForSeconds(hitlag);

        isHitting = false;
    }
    //Enemy attacks
    public void EnemeyAttack(float offset = -0.8f)
    {
        if (isHitting)
        {
            return;
        }
        isHitting = true;
        StartCoroutine(EnemyLeft(0.3f, offset));
    }
    IEnumerator EnemyLeft(float hitlag, float offset = 0)
    {
        HitBoxes(new Vector3(offset, -0.15f, 0), 10, .2f, 0.8f, 0.6f);
        yield return new WaitForSeconds(hitlag);

        isHitting = false;
    }
    void HitBoxes(Vector3 hitboxOffset, int dmg, float lifeTime, float xScale = 1, float yScale = 1)
    {
        Vector3 pos = this.gameObject.transform.position + hitboxOffset;
        GameObject hitbox = (Instantiate(hitBox, pos, this.gameObject.transform.rotation));
        hitbox.GetComponent<HitBox>().Setup(this.gameObject, dmg, lifeTime, xScale, yScale);
    }
}
