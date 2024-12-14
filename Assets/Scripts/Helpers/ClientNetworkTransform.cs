namespace Game1.Helpers
{
    using Unity.Netcode.Components;

    public class ClientNetworkTransform : NetworkTransform
    {
        protected override bool OnIsServerAuthoritative()
        {
            return false;
        }
    }
}