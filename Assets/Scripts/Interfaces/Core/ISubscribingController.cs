public interface ISubscribingController
{
    public void SubscribeOnUpdate(IUpdateble controller);
    public void SubscribeOnLateUpdate(ILateUpdateble controller);
    public void SubscribeOnDispose(IDisposable controller);
}
