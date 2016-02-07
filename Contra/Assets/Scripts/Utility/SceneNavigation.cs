using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{
    public enum LevelEnum { Game, EoLMenu, Store, Wait }

    public void NavigateTo(string LevelName)
    {
        SceneManager.LoadScene(LevelName);
    }

    public void NavigateTo(LevelEnum levelEnum)
    {
        switch(levelEnum)
        {
            case LevelEnum.EoLMenu:
                NavigateTo("EoLMenu");
                break;
            case LevelEnum.Game:
                NavigateTo("Runner");
                break;
            case LevelEnum.Store:
                NavigateTo("Splash");
                break;
            case LevelEnum.Wait:
                NavigateTo("Waitforinput");
                break;
        }
    }

    public void NavigateToStart()
    {
        NavigateTo(LevelEnum.Wait);
    }
    
    public void NavigateToPlay()
    {
        NavigateTo(LevelEnum.Game);
    }

    public void NavigateToStore()
    {
        NavigateTo(LevelEnum.Store);
    }
}
