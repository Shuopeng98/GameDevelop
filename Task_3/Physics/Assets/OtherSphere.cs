using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherSphere : MonoBehaviour
{
    public float friction;
    public float mass;
    public float radius;
    public float length, width;

    [NonSerialized]
    public Vector3 currentV;
    private bool isWall;

    // Start is called before the first frame update
    void Start()
    {
        isWall = false;
    }

    // Update is called once per frame
    void Update()
    {

        //摩擦力
        Vector3 frictionDeltaV = -Time.deltaTime * friction * currentV.normalized;
        //防止摩擦力反向运动
        Vector3 finalV = currentV + frictionDeltaV;
        if (finalV.x * currentV.x <= 0)
            frictionDeltaV.x = -currentV.x;
        if (finalV.y * currentV.y <= 0)
            frictionDeltaV.y = -currentV.y;
        if (finalV.z * currentV.z <= 0)
            frictionDeltaV.z = -currentV.z;

        //应用加速度
        Vector3 prePos = transform.position;
        Vector3 curV = currentV + frictionDeltaV;
        transform.Translate((curV + currentV) * Time.deltaTime / 2);
        currentV = curV;

        Vector3 pos = transform.position;

        //检测是否碰撞到墙边
        if (pos.x >= length - 0.5 - radius || pos.x <= -(length - 0.5 - radius) || pos.z >= width - 0.5 - radius || pos.z <= -(width - 0.5 - radius))
        {
            transform.position = prePos;
            isWall = true;
        }
        else
        {
            isWall = false;
        }

        if (isWall)
        {
            if (pos.x >= length - 0.5-radius || pos.x <= -(length - 0.5 - radius))
            {
                currentV.x = -(currentV.x);
            }
            if (pos.z >= width - 0.5 - radius || pos.z <= -(width - 0.5 - radius))
            {
                currentV.z = -(currentV.z);
            }
        }
    }

}
