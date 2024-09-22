using UnityEngine;

[System.Serializable]
public class PlayerLightShot : MonoBehaviour
{
    private bool isLightning = false;
    [SerializeField] private float timeToReduceScale = 0.1f;
    [SerializeField] private float scaleReductionAmount = 0.1f;
    [SerializeField] private float minScale = 1.5f;

    private float nextScaleReductionTime;

    public void ToggleLightning(GameObject lightObject)
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!isLightning)
            {
                lightObject.SetActive(true);
                isLightning = true;
                Debug.Log("ILUMINA SENHOOOR");
                nextScaleReductionTime = Time.time + timeToReduceScale;
            }
            else
            {
                lightObject.SetActive(false);
                isLightning = false;
                Debug.Log("BELLIGOL O DESILUMINADO");
            }
        }

        if (isLightning && lightObject.activeSelf)
        {
            ReduceLightScale(lightObject);
        }
    }

    private void ReduceLightScale(GameObject lightObject)
    {
        if (Time.time >= nextScaleReductionTime)
        {
            Vector3 currentScale = lightObject.transform.localScale;

            float newScaleX = Mathf.Max(minScale, currentScale.x - scaleReductionAmount);
            float newScaleY = Mathf.Max(minScale, currentScale.y - scaleReductionAmount);
            float newScaleZ = Mathf.Max(minScale, currentScale.z - scaleReductionAmount);

            lightObject.transform.localScale = new Vector3(newScaleX, newScaleY, newScaleZ);

            nextScaleReductionTime = Time.time + timeToReduceScale;
        }
    }
}
