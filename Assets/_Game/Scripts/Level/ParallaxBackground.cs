using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Camera cam;

    [SerializeField] private float parallaxEffect;

    private float length;
    private float xPosition;
    private float initialXPosition;

    void Start()
    {
        cam = Camera.main;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        xPosition = transform.position.x;
        initialXPosition = xPosition;
    }

    
    void Update()
    {
        if(GameManager.Instance.IsState(GameState.GamePlay))
        {
            float distanceMoved = cam.transform.position.x * (1 - parallaxEffect);
            float distanceToMove = cam.transform.position.x * parallaxEffect;

            transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);

            if (distanceMoved > xPosition + length)
            {
                xPosition = xPosition + length;
            }
        }

    }

    public void OnInit()
    {
        // Thiết lập lại về màn đầu
        xPosition = initialXPosition;
        transform.position = new Vector3(initialXPosition, transform.position.y);
    }
}
