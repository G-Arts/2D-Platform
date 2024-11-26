using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class WeaponController : MonoBehaviour
{
    private bool flip = false;

    private float lastFireTime = 0f;

    [SerializeField] private Transform _gunPoint;
    [SerializeField] private GameObject _bulletTrail;
    [SerializeField] private float _weaponRange = 10f;

    void Update()
    {
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int flipDeg = 0;
        float angle = Mathf.Atan2(mouseWorldPosition.y - transform.position.y, mouseWorldPosition.x - transform.position.x) * Mathf.Rad2Deg;
        if (flip) flipDeg = 180;
        else flipDeg = 0;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - flipDeg));
        transform.rotation = targetRotation;

        if (Input.GetMouseButtonDown(0) && Time.time - lastFireTime > 0.5f)
        {
            FireBullet();
            lastFireTime = Time.time;
        }

    }

    public void Flip()
    {
        flip = !flip;
    }

    private void FireBullet()
    {
        var hit = Physics2D.Raycast(
            _gunPoint.position,
            transform.right,
            _weaponRange);

        var trail = Instantiate(
            _bulletTrail,
            _gunPoint.position,
            transform.rotation
            );

        var trailScript = trail.GetComponent<BulletTrail>();

        if(hit.collider != null)
        {
            trailScript.SetTargetPosition(hit.point);
            var target = hit.collider.GetComponent<Target>();
            target?.getDamage(25);
        }
        else
        {
            var endPosition = _gunPoint.position + transform.right * _weaponRange;
            trailScript.SetTargetPosition(endPosition);
        }
    }
}
