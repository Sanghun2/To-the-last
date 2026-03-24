using UnityEngine;

public class GameBootStrapper
{
    public void StartBootStrap() {
        // 연출

        // 체인 시작
        Managers.UI.OpenUI<GameBootStrapUI>();
        Managers.Process.StartProcess(Define.FlowType.BootStrapGame);
    }

    public void CancelBootStrap() {
        Managers.UI.CloseUI<GameBootStrapUI>();
        Managers.Trait.ResetTraits();
        Managers.Character.ResetCharacters();
    }

    public void CompleteBootStrap() {
        Debug.Log("complete called");
        Managers.UI.CloseUI<GameBootStrapUI>();

        Managers.Scene.TransitionScene(Define.INGAME_SCENE_ID, callback: () => {
            Managers.UI.CloseUI<MainMenuCanvas>();
        });
    }
}
