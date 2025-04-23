using UnityEngine;
using System.Collections;

public class CrankPull : MonoBehaviour
{

    public Transform player;
    public float interactionDistance = 2f;
    public float pullAngle = 80f;
    public float pullSpeed = 100f;
    public KeyCode interactKey = KeyCode.E;

    public ReelSpinner[] reels; // 🎰 Array of 3 reels

    private bool isPulling = false;
    private float currentAngle = 0f;
    private bool goingBack = true;
    private bool hasSpun = false;
    private bool isSpinning = false;

    void Start()
    {
        Random.InitState(System.DateTime.Now.Ticks.GetHashCode());
    }


    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < interactionDistance && Input.GetKeyDown(interactKey) && !isPulling)
        {
            StartCrankMotion();
        }

        if (isPulling)
        {
            float rotationStep = pullSpeed * Time.deltaTime;
            float rotateAmount = Mathf.Min(rotationStep, pullAngle - currentAngle);

            if (goingBack)
                transform.Rotate(Vector3.forward, rotateAmount);
            else
                transform.Rotate(Vector3.forward, -rotateAmount);

            currentAngle += rotationStep;

            if (currentAngle >= pullAngle)
            {
                currentAngle = 0f;

                if (goingBack)
                    goingBack = false;
                else
                {
                    isPulling = false;

                    if (!hasSpun)
                    {
                        hasSpun = true;
                        StartCoroutine(SpinReelsInSequence());
                    }
                }
            }
        }
    }

    public void StartCrankMotion()
    {
        if (!isPulling && !isSpinning)
        {
            isPulling = true;
            currentAngle = 0f;
            goingBack = true;
            hasSpun = false;
        }
    }

    private IEnumerator SpinReelsInSequence()
    {
        foreach (var reel in reels)
        {
            if (reel != null)
                reel.StartSpin();
        }

        yield return new WaitForSeconds(2.5f); // Enough time for all to stop

        Debug.Log("All reels stopped:");
        foreach (var reel in reels)
        {

            Debug.Log(reel.currentSymbol); // You now have the exact result!
            reel.StopSpin();
            yield return new WaitForSeconds(0.75f); // delay between stops
        }

        // Optionally: Check if player won etc.
    }
}
