using UnityEngine;

public class PoleMovement : MonoBehaviour
{
    public float speed = 5f;  // Vitesse de déplacement du Pole

    void Update()
    {
        // Obtenez l'entrée horizontale (valeur entre -1 et 1)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculez le déplacement
        Vector3 movement = new Vector3(horizontalInput, 0, 0) * speed * Time.deltaTime;

        // Appliquez le déplacement à l'objet Pole
        transform.Translate(movement);
    }
}
