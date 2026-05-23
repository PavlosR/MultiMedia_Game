using UnityEngine;

public class UISwapInfo : MonoBehaviour
{
    [Header("General")]

    [SerializeField] public bool fade;

    [Header("Swap UI")]
    [SerializeField] public GameObject current;
    [SerializeField] public GameObject enable;

    [Header("Swap Scene")]
    [SerializeField] public string Scene;
}
