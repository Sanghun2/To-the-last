using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public string UserId { get; set; }
    public string DisplayName { get; set; }
    public string Email { get; set; }

    [SerializeField] List<string> availableTraitList = new List<string>();
}
