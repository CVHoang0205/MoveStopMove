using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TargetIndicator : MonoBehaviour
{
    public Character character;
    public Image coloImage;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI nameText;
    public Camera Camera => CameraFollower.Ins.gameCamera;
    Vector3 viewPoint;
    Vector3 ScreenHalf = new Vector2(Screen.width, Screen.height) / 2;

    public float distanceIndicator = 3f;
    private void LateUpdate()
    {
        viewPoint = Camera.WorldToViewportPoint(character.transform.position + Vector3.up * distanceIndicator);
        //viewPoint = Camera.WorldToScreenPoint(character.skin.indicatorHead.transform.position);
        viewPoint.x = Mathf.Clamp(viewPoint.x, 0.075f, 0.925f);
        viewPoint.y = Mathf.Clamp(viewPoint.y, 0.075f, 0.875f);
        GetComponent<RectTransform>().anchoredPosition = Camera.ViewportToScreenPoint(viewPoint) - ScreenHalf;
    }

    public void InitTarget(UnityEngine.Color color, int level, string name) 
    {
        coloImage.color = color;
        levelText.text = level.ToString();
        nameText.text = name;
    }

    public void InitTarget(int level) 
    {
        levelText.text = level.ToString();
        distanceIndicator = 3f + level * 0.3f;
    }
}
