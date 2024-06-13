using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]

    private float moveSpeed;

    [SerializeField]
    private GameObject[] weapons;

    private int weaponIndex = 0;

    [SerializeField]
    private Transform shootTransform;

    [SerializeField]
    private float shootInterval = 0.05f;

    private float lastShotTime = 0f;

    // Update is called once per frame
    void Update()
    {
        //float horizontalInput = Input.GetAxisRaw("Horizontal");
        //float verticalInput = Input.GetAxisRaw("Vertical");
        //Vector3 moveTo = new Vector3(horizontalInput, 0f, 0f);
        //transform.position += moveTo * moveSpeed * Time.deltaTime;
    
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float toX = Mathf.Clamp(mousePos.x, -2.35f, 2.35f);
        transform.position = new Vector3(toX, transform.position.y, transform.position.z);

        if (GameManager.Instance.isGameOver == false)
        {
            Shoot();
        }
    }

    void Shoot() {
        if (Time.time - lastShotTime > shootInterval) {
            Instantiate(weapons[weaponIndex], shootTransform.position, Quaternion.identity);
            lastShotTime = Time.time;

        }
    }
     private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss") {
                GameManager.Instance.SetGameOver();
                Destroy(gameObject);
        } else if (other.gameObject.tag == "Coin") {
            GameManager.Instance.IncreaseCoin();
            Destroy(other.gameObject);
        }
     }


    public void Upgrade() {
        weaponIndex += 1;   
        if (weaponIndex >= weapons.Length) {
            weaponIndex = weapons.Length - 1;
        }
    }
}

