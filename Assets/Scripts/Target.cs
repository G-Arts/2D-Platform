using UnityEngine;

public class Target : MonoBehaviour
{
    private float health = 100f;
    private SpriteRenderer sprite;
    private Color targetColor = Color.red;

    private void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    public void getDamage(float damage)
    {
        health -= damage;
        sprite.color = targetColor;
        if (health <= 0)Destroy(gameObject);

        
    }
}
