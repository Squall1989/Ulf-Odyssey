using Ulf;
using UnityEngine;

public class TestAnimControl : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed, runSpeed;
    private Animator _anim;
    private PlanetMono _planet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _anim = FindAnyObjectByType<Animator>();
        _planet = FindAnyObjectByType<PlanetMono>();
    }
    bool isDead = false;
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            isDead ^= true;
        }

        _anim.SetBool("isDead", isDead);

        if (isDead)
        {
            return;
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            _anim.SetTrigger("damage");
        }

            float speed = 0;
        if(Input.GetAxis("Horizontal") > 0)
        {
            speed = walkSpeed;
            _anim.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            speed = -walkSpeed;
            _anim.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            speed =  0;
        }

        if(Input.GetAxis("Shift") > 0)
        {
            if(speed != 0) 
                speed = speed > 0 ? runSpeed : -runSpeed;
        }

        int attack = 0;

        if (Input.GetAxis("Fire1") > 0)
        {
            attack = 1;
        }
        else if (Input.GetAxis("Fire2") > 0)
        {
            attack = 2;
        }

        _anim.SetInteger("attack", attack);

        if(attack > 0)
            speed = 0;
        
        _anim.SetFloat("speed", Mathf.Abs( speed));
        _planet.transform.Rotate(new Vector3(0, 0, speed) * Time.deltaTime);

    }
}
