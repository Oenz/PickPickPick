using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPowerUp
{
    [SerializeField] float _moveDefaultSpeed = 2f;
    [SerializeField] float _mouseSpeed = 2f;
    [SerializeField] Vector2 _maxViewAngle = new Vector2(80, 280);
    [SerializeField] GameObject _camera;

    Rigidbody _rb;
    CapsuleCollider _collider;
    Vector3 _dir;
    float _moveSpeed;
    bool _wantsSprint;
    bool _sprinting;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _moveSpeed = _moveDefaultSpeed;
    }

    void Update()
    {
        Movement();

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        CameraUpRotate(mouseX * _mouseSpeed, mouseY * _mouseSpeed);

        if (Input.GetButtonDown("Sprint"))
        {
            SprintStart();
        }
        if (Input.GetButtonUp("Sprint"))
        {
            SprintEnd();
        }

        if (Input.GetButtonUp("Interact"))
        {
            Interact();
        }

        
    }

    void Movement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 ForwardVector = this.gameObject.transform.forward * moveZ * Time.deltaTime;
        Vector3 RightVector = this.gameObject.transform.right * moveX * Time.deltaTime;
        _dir = ForwardVector + RightVector;
        _dir.Normalize();

        if (_wantsSprint && moveZ >= 0.1f)
        {
            _sprinting = true;
            _dir *= 1.5f;
        }
        else
        {
            _sprinting = false;
        }

        _dir.y = _rb.velocity.y;
        _rb.velocity = _dir * _moveSpeed;
    }

    void SprintStart()
    {
        _wantsSprint = true;
    }

    void SprintEnd()
    {
        _wantsSprint = false;
    }

    void CameraUpRotate(float valueX, float valueY)//Cinemachine‚ÉˆÚs‚·‚é
    {
        transform.RotateAround(transform.position,  transform.up, valueX);

        float temp = _camera.transform.eulerAngles.x - valueY;
        if (temp > _maxViewAngle.x & temp < _maxViewAngle.y) return;
        _camera.transform.RotateAround(_camera.transform.position, _camera.transform.right, -valueY);
    }

    private void Interact()
    {
        Debug.DrawLine(_camera.transform.position, _camera.transform.position + _camera.transform.forward * 2);
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward * 2);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log($"Interact{gameObject.name} -> {hit.collider.gameObject.name}");
            IInteract iin = hit.collider.GetComponent<IInteract>();
            if (iin != null)
            {
                iin.Interact(this.gameObject);
            }
        }
    }

    public void SpeedUp(float amount)
    {
        _moveSpeed += amount;
    }
}