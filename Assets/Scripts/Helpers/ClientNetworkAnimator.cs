namespace Game1.Helpers
{
    using Unity.Netcode.Components;

    public class ClientNetworkAnimator : NetworkAnimator
    {
        protected override bool OnIsServerAuthoritative()
        {
            return false;
        }
    }
}