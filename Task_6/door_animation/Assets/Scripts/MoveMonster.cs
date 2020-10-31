using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MoveMonster : MonoBehaviour
{
    public Transform kPlayer;
    public Camera m_camera;
    public float fDistance = 5f;
    public float waitTime = 1f;
    bool fire;
    bool firePlay;
    bool isWall;
    private Animator animator;
    public ParticleSystem ps;
    int counts;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWall = false;
        firePlay = false;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float speed = Mathf.Sqrt(h * h + v * v);
        bool isRun= Input.GetKey(KeyCode.LeftShift);
        speed = isRun ?  2 : speed;
        

        fire = Input.GetButtonDown("Fire1");
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")&&fire)
        {
            animator.SetTrigger("TriggerAttack");
            if (isWall) Invoke("PlayFire", waitTime);
                //firePlay = true;
        }
        animator.SetFloat("MoveSpeed", speed);
        // if(firePlay)
    }

    private void FixedUpdate()
    {
        Vector3 kNewPos = Camera.main.transform.position + fDistance * Camera.main.transform.forward;
        kNewPos.y = 0;

        //kPlayer.position = kNewPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Wall"))
            isWall = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Wall"))
            isWall = false; ;
    }
    void PlayFire()
    {
        if(ps.isPlaying)
        {
            ps.Stop();
        }
        if(ps.isStopped)
        {
            ps.Play();
        }
        StartCoroutine(m_camera.GetComponent<CameraShake>().Shake());
    }


}
