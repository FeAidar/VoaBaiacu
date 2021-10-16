using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InfiniteScroll : MonoBehaviour
{
    [SerializeField] protected Transform[] parts;
    [SerializeField] private Vector2 speed;
    [SerializeField] private float Tamanho;
    private float width;
    private int _partsLenght;
    protected Action<Transform> onScrollPartRecycled;


    void Awake()
    {
        _partsLenght = parts.Length;
        width = Tamanho * transform.localScale.x;
        Debug.Log(width);
    }

    // Update is called once per frame
    void Update()
    {
        
        foreach (Transform part in parts)
        {
            //Debug.Log(part.position.x + " / " + (width * _partsLenght / 2));
   

            part.Translate(speed * Time.deltaTime);
            if (speed.x < 0)
            {
                if (part.position.x < -width * _partsLenght / 2)
                {
                    //Debug.Log("PASSOU! ATRAS!");
                    part.Translate(new Vector2(_partsLenght * width, 0));
                    onScrollPartRecycled?.Invoke(part);





                }
            }
            if(speed.x > 0)
            { 
                    if (part.position.x > width * _partsLenght/ 2)
                {
                   // Debug.Log("PASSOU!");
                  part.Translate(new Vector2(_partsLenght *- width, 0));
                    onScrollPartRecycled?.Invoke(part);




                }

            }
        }
    }
}
