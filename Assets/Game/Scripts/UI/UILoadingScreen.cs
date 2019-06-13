using UnityEngine;
using UnityEngine.UI;

public class UILoadingScreen : MonobehaviourSingleton<UILoadingScreen>
{
    public Text loadingText;
    public Image loadingBar;

    public override void Awake()
    {
        base.Awake();
    }

    public void SetVisible(bool show)
    {
        gameObject.SetActive(show);
    }

    public void Update()
    {
        TextLoader();
        BarLoader();
    }

    void TextLoader()
    {
        int loadingVal = (int)(LoaderManager.Get().loadingProgress * 100);
        loadingText.text = loadingVal.ToString() + "%";
        if (LoaderManager.Get().loadingProgress >= 1)
            SetVisible(false);
    }

    void BarLoader()
    {
        int loadingVal = (int)(LoaderManager.Get().loadingProgress * 100);
        Debug.Log(loadingVal * 0.01);
        loadingBar.fillAmount = (loadingVal*0.01f);
        if (LoaderManager.Get().loadingProgress >= 1)
            SetVisible(false);
    }

}