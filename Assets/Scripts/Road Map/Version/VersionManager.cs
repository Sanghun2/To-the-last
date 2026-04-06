using System;
using UnityEngine;

public sealed class VersionManager
{
    public enum VersionResult {

    }

    // 현재 버전
    private string currentVersion;

    // 업데이트 필요 여부 판단
    public VersionResult CheckVersion() {
        var latestVersion = GetLastestVersion();
        return CheckVersion(currentVersion, latestVersion);
    }


    private VersionResult CheckVersion(string currentVersion, object latestVersion) {
        throw new NotImplementedException();
    }
    private object GetLastestVersion() {
        throw new NotImplementedException();
    }
}
