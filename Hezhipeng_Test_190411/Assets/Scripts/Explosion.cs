using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AutoDestroy();
    }

    private void AutoDestroy()
    {
        AnimatorStateInfo animState = anim.GetCurrentAnimatorStateInfo(0);
        if (animState.normalizedTime >= 1)
        {
            Destroy(gameObject);
        }
    }
}
