using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfCircle : MonoBehaviour
{   
    Vector2 center;

    // Start is called before the first frame update
    void Start()
    {
        center = Vector2.zero;

        EdgeCollider2D edgeCollider = GetComponent<EdgeCollider2D>();

        // 반원을 만들기 위해 꼭짓점들을 설정합니다.
        Vector2[] points = new Vector2[20]; // 꼭짓점의 개수를 적절히 조정합니다.

        
        float radius = 0.5f;

        // 반원의 꼭짓점들을 계산하여 설정합니다.
        for (int i = 0; i < points.Length; i++)
        {
            float angle = Mathf.PI * i / (points.Length - 1)/1.5f; // 반원의 각도를 계산합니다.
            points[i] = center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        }

        // Edge Collider 2D의 점 배열을 설정합니다.
        edgeCollider.points = points;
    }   
}
