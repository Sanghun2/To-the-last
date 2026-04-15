using BilliotGames;
using UnityEngine;

public class StartButton : ButtonBase
{
    protected override void ButtonAction() {
        //Managers.Scene.TransitionScene(Define.INGAME_SCENE_ID);
        Managers.Process.TryStartProcess(Define.FlowType.BootStrapGame);
    }
}
