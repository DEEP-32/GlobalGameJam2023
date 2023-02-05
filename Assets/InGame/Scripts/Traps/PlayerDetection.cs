using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] private bool _canShoot = false;
    [SerializeField] private float _fireCooldown = .7f;
    [SerializeField] private ParticleSystem _damageParticle;
    private float _lastTimeFire = 0f;

    private Transform _parent;


    private void Awake()
    {
        _parent = transform.parent;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger enter");
        _canShoot = true;
        if(transform.position.x < other.transform.position.x)
        {
            _parent.eulerAngles = Vector3.zero;
        }
        else if(transform.position.x > other.transform.position.x)
        {
            _parent.eulerAngles = new Vector3(transform.eulerAngles.x,180f,transform.eulerAngles.z);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
         _canShoot=false;
        _damageParticle.Stop();
       
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Time.time > _fireCooldown + _lastTimeFire && _canShoot)
        {
            Debug.Log("Bursting Spores");
            _damageParticle.Play();
            _lastTimeFire = Time.time;
        }

       
    }






}
