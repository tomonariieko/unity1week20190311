﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb;
    int key_x = 0;
    int key_y = 0;
    float dx;
    float dy;

    List<CatController> catList;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        catList = new List<CatController>();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(key_x, key_y).normalized * Variable.walkSpeed;
        //rb.velocity = new Vector2(dx, dy) * Variable.walkSpeed;

        float x = key_x;
        if (key_x == 0) x = transform.localScale.x;
        float y = transform.localScale.y;
        float z = transform.localScale.z;
        transform.localScale = new Vector3(x, y, z);


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (Variable.isTimeUp) return;

        if (other.gameObject.tag.Equals("cat"))
        {
            //Debug.Log("ねこ");
            CatAddList(other.gameObject);
        }
        if (other.gameObject.tag.Equals("water"))
        {
            //Debug.Log("水");
            CatRemoveList();
        }

    }



    // Update is called once per frame
    void Update()
    {

        key_x = 0;
        key_y = 0;
        dx = 0;
        dy = 0;

        if (Variable.isTimeUp) return;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            key_y = 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            key_y = -1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            key_x = 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            key_x = -1;
        }


        dx = Input.GetAxis("Horizontal");
        dy = Input.GetAxis("Vertical");

    }

    void CatRemoveList()
    {
        //if (Variable.catCount == 0) return;

        while (Variable.catCount > 0)
        {
            Variable.catCount--;
            catList[Variable.catCount].isChase = false;
            catList.RemoveAt(Variable.catCount);
        }

    }

    void CatAddList(GameObject cat)
    {
        catList.Add(cat.GetComponent<CatController>());
        // cat.name = Variable.catCount.ToString();

        if (Variable.catCount == 0)
        {
            catList[Variable.catCount].targetObj = gameObject;
        }
        else
        {
            catList[Variable.catCount].targetObj = catList[Variable.catCount - 1].gameObject;
        }
        catList[Variable.catCount].isChase = true;
        Variable.catCount++;
    }


}
