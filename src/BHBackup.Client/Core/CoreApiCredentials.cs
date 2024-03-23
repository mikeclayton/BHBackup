namespace BHBackup.Client.Core;

public sealed class CoreApiCredentials
{

    public CoreApiCredentials(string accessToken, string deviceId)
    {
        this.AccessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
        this.DeviceId = deviceId ?? throw new ArgumentNullException(nameof(deviceId));
    }

    /// <summary>
    ///  The value to use in the x-family-access-token header in api requests.
    /// </summary>
    internal string AccessToken
    {
        get;
    }

    internal string DeviceId
    {
        get;
    }

}
