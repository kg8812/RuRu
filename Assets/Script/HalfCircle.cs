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

        // �ݿ��� ����� ���� ���������� �����մϴ�.
        Vector2[] points = new Vector2[20]; // �������� ������ ������ �����մϴ�.

        
        float radius = 0.5f;

        // �ݿ��� ���������� ����Ͽ� �����մϴ�.
        for (int i = 0; i < points.Length; i++)
        {
            float angle = Mathf.PI * i / (points.Length - 1)/1.5f; // �ݿ��� ������ ����մϴ�.
            points[i] = center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        }

        // Edge Collider 2D�� �� �迭�� �����մϴ�.
        edgeCollider.points = points;
    }   
}
