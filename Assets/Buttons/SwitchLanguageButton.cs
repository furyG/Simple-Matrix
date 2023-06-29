using System.Collections;
using UnityEngine.Localization.Settings;

public class SwitchLanguageButton : Clickable
{
    private bool active = false;
    private int localeID = 1;
    protected override ButtonType type => ButtonType.ChangeLanguage;

    protected override void OnClick()
    {
        base.OnClick();

        localeID = localeID > 0 ? 0 : 1;

        ChangeLocale(localeID);
    }
    private void ChangeLocale(int localeID)
    {
        if (active == true)
        {
            return;
        }
        StartCoroutine(SetLocale(localeID));
    }
    IEnumerator SetLocale(int localeID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        active = false;
    }

}
