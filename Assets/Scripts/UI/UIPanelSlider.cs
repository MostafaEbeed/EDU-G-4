using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelSlider : MonoBehaviour
{
    [SerializeField] private GameObject _panelActivation;
    [SerializeField] private RectTransform _targetPanel;
    [SerializeField] private bool horizontal;
    [SerializeField] private float animationTime = 1f;
    [SerializeField] private LeanTweenType animationType;
    private Vector2 openedPos;
    private Vector2 closedPos;


    void Start()
    {
        if(horizontal)
        {
            closedPos = new Vector2(Screen.width, 0);
            openedPos = _targetPanel.localPosition;
        }
        else
        {
            closedPos = new Vector2(0, Screen.height);
            openedPos = _targetPanel.localPosition;
        }

        Debug.Log(closedPos + " " + openedPos);
        _targetPanel.localPosition = closedPos;
    }

    [Button]
    public void OpenPanel()
    {
        _targetPanel.GetComponent<Image>().raycastTarget = true;
        _panelActivation.SetActive(true);
        LeanTween.cancel(_targetPanel);
        LeanTween.moveLocal(_targetPanel.gameObject, openedPos, animationTime).setEase(animationType);

        /*LeanTween.cancel(_targetPanel);
        LeanTween.alpha(_targetPanel, 1.0f, 0.5f).setRecursive(false);   */
    }

    [Button]
    public void ClosePanel()
    {
        _targetPanel.GetComponent<Image>().raycastTarget = false;

        LeanTween.cancel(_targetPanel);
        LeanTween.moveLocal(_targetPanel.gameObject, closedPos, 0.5f).setEase(LeanTweenType.easeInCubic);

        /*LeanTween.cancel(_targetPanel);
        LeanTween.alpha(_targetPanel, 1.0f, 0.5f).setRecursive(false);   */
    }

    public void TestDelay()
    {
        LeanTween.delayedCall(2f, StartGame);
    }

    public void StartGame()
    {
        Debug.Log("Finished Checking");
    }
}
