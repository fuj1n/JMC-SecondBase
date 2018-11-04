using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private const int ALPHA_START = (int)KeyCode.Alpha0;
    private const int NUMPAD_START = (int)KeyCode.Keypad0;
    private static readonly KeyCode[] KEYS = new KeyCode[20];

    public float moveSpeed = 5F;
    public GameObject bulletTemplate;

    private new Rigidbody2D rigidbody;

    private TextMeshPro text;

    private Bullet bullet;

    private int typed;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        text = GetComponentInChildren<TextMeshPro>();

        // Update the typed display
        Typed(int.MinValue);
    }

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        rigidbody.velocity = Vector2.right * moveX * moveSpeed;

        for (int i = 0; i < KEYS.Length; i++)
        {
            if (Input.GetKeyDown(KEYS[i]))
            {
                Typed(Mathf.FloorToInt(i / 2F));
                break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
            Typed(-1);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            Submit();
    }

    private void Typed(int key)
    {
        if (key == int.MinValue)
            typed = 0;
        if (key < 0 && typed > 0)
        {
            typed = Mathf.FloorToInt(typed / 10);
        }
        else if (key >= 0 && key <= 9)
        {
            int test = typed * 10 + key;

            if ((IsValidBinary(test) && test <= 11111111) || test < 256)
                typed = test;
        }

        text.text = typed.ToString();
    }

    private void Submit()
    {
        if (bullet)
            return;

        GameObject bulletObject = Instantiate(bulletTemplate, transform.position, bulletTemplate.transform.rotation);

        bullet = bulletObject.GetComponent<Bullet>();
        bullet.isValidBinary = IsValidBinary(typed);
        bullet.Text = typed.ToString();

        EventBus.Post(new EventPlayerShoot());

        Typed(int.MinValue);
    }

    private bool IsValidBinary(int i)
    {
        try
        {
            Convert.ToInt32(i.ToString(), 2);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    // Generates an array of keycodes from ALPHA0-ALPHA9 and from KEYPAD0-KEYPAD9
    // Staggered so that ALPHAs occupy the even spots in the array and KEYPADs occupy odd spots
    static Player()
    {
        for (int i = ALPHA_START; i < ALPHA_START + 10; i++)
        {
            int index = (i - ALPHA_START) * 2; // Even numbers

            KEYS[index] = (KeyCode)i;
        }

        for (int i = NUMPAD_START; i < NUMPAD_START + 10; i++)
        {
            int index = (i - NUMPAD_START) * 2 + 1; // Odd numbers

            KEYS[index] = (KeyCode)i;
        }
    }
}
