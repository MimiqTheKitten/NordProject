using UnityEngine;
using UnityEngine.Animations;

public class Turret : MonoBehaviour
{
    [SerializeField] AnimationCurve headRotation;
    float timeTurretOn = 0;
    float currentRotation = 0;
    [SerializeField] float rotationSpeed = 10;
    [SerializeField] float defaultXTilt;
    [SerializeField] float maxYRotation;

    [SerializeField] float offsetYRotation = 180;

    [SerializeField] GameObject target;
    [Header("bullet")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootPos;
    [SerializeField] float fireRate = 0.2f;
    [SerializeField]float bulletSpeed = 10;
    [SerializeField] float bulletLife = 2;
    bool shouldFire = true;

    [Header("Current action")]
    [SerializeField] string action = "lookaround";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (action) 
        {
            case "lookaround":
                LookAround();
                break;
            case "lookat":
                LookAt();
                if (shouldFire)
                {
                    shouldFire = false;
                    Invoke(nameof(Shoot), fireRate);
                }
                break;
            default:
                break;
        }
    }
    void LookAround()
    {
        timeTurretOn += Time.deltaTime*rotationSpeed/maxYRotation;
        currentRotation = headRotation.Evaluate(timeTurretOn)*maxYRotation;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(defaultXTilt,currentRotation +offsetYRotation,0));
        transform.rotation = targetRotation;
    }
    void LookAt()
    {
        transform.LookAt(target.transform, Vector3.up);
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(this.bullet, shootPos.position,shootPos.rotation);
        bullet.GetComponent<Rigidbody>().linearVelocity = bullet.transform.forward * bulletSpeed;
        shouldFire = true;
        Destroy(bullet, bulletLife);
    }
}
