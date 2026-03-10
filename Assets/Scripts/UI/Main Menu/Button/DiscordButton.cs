using BilliotGames;
using UnityEngine;

public class DiscordButton : ButtonBase
{
    [SerializeField] string discordURL = "https://discord.gg/aHkuXAwfn3";

    protected override void ButtonAction() {
        Application.OpenURL(discordURL);
    }
}
