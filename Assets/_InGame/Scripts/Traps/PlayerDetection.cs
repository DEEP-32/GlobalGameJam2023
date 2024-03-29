using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] private bool _canShoot = false;
    [SerializeField] private float _fireCooldown = .7f;
    [SerializeField] private ParticleSystem _damageParticle;
    private Transform playerTransform;
    private float _lastTimeFire = 0f;

    private Transform _parent;
 
    private void Awake()
    {
        _parent = transform.parent;
    }
    private void Update()
    {
        if (_canShoot)
        {
            if (transform.position.x < playerTransform.position.x)
            {
                _parent.eulerAngles = Vector3.zero;
            }
            else if (transform.position.x > playerTransform.position.x)
            {
                _parent.eulerAngles = new Vector3(transform.eulerAngles.x, 180f, transform.eulerAngles.z);
            }
            if (Time.time > _fireCooldown + _lastTimeFire && _canShoot)
            {
                Debug.Log("Bursting Spores");

                _damageParticle.Play();
                _lastTimeFire = Time.time;

            }
           
        }
        else
        {
            _damageParticle.Stop();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger enter");
        _canShoot = true;
        playerTransform = other.transform;
        
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
         _canShoot=false;
       
       
    }








}
