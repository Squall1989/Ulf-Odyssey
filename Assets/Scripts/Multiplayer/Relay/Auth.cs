using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;

public class Auth 
{


    public async Task AuthenticatingAPlayer()
    {
        try
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            var playerID = AuthenticationService.Instance.PlayerId;
            //Debug.Log("playerID: "  + playerID);
        }
        catch (Exception e)
        {
            throw new Exception("Can't auth: " + e.Message);
        }
    }


}
