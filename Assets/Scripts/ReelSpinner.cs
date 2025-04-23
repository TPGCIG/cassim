using UnityEngine;
using System.Collections;


public enum SymbolType
{
    Lion,
    Banana,
    Grapes,
    Seven,
    Cherry,
    Apple
}



public class ReelSpinner : MonoBehaviour
{
    public SymbolType[] symbolOrder = new SymbolType[]
    {
        SymbolType.Lion,
        SymbolType.Banana,
        SymbolType.Grapes,
        SymbolType.Seven,
        SymbolType.Cherry,
        SymbolType.Apple
    };

    public SymbolType currentSymbol { get; private set; }
    public bool isSpinning = false;
    public float spinDuration = 2f; // Seconds to "spin" visually
    public float spinSpeed = 2880f; // Degrees per second

    void Start()
    {
        int seed = System.DateTime.Now.Millisecond; // or any fixed number for predictable results
        Random.InitState(seed);
        Debug.Log($"Random seed set to: {seed}");
    }


    public void StartSpin()
    {
        isSpinning = true;
    }

    void Update()
    {
        if (isSpinning)
        {
            transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);
        }
    }

    public void StopSpin()
    {
        // Optional: Add animation here like rotating or flashing

        // Randomly select a symbol index
        int index = Random.Range(0, symbolOrder.Length);
        currentSymbol = symbolOrder[index];
        int angle = index * 60 + 30;

        Vector3 alignedRotation = new Vector3(
        angle,  // Keep X as-is
        transform.eulerAngles.y,                 // Snap Y
        transform.eulerAngles.z   // Keep Z as-is
        );

        transform.eulerAngles = alignedRotation;

        Debug.Log($"Reel stopped at: {currentSymbol}");

        isSpinning = false;
    }
}