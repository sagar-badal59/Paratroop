using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField]
    public Camera cam;

    [SerializeField]
    Rigidbody2D rb;
    Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }
    private void FixedUpdate()
    {
        Vector2 lookDir = mousePos - AsVector2(transform.position);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg-90f;
        rb.rotation = angle;
    }

    public static Vector2 AsVector2(Vector3 _v)
    {
        return new Vector2(_v.x, _v.y);
    }
}
