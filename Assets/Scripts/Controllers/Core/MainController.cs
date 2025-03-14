using System.Collections.Generic;

public sealed class MainController: IControlledController, ISubscribingController
{
    private List<IUpdateble> updatebleControllers = new List<IUpdateble>(8);
    private List<ILateUpdateble> lateUpdatebleControllers = new List<ILateUpdateble>(8);
    private List<IDisposable> disposebleControllers = new List<IDisposable>(8);

    public void SubscribeOnUpdate(IUpdateble controller)
    {
        updatebleControllers.Add(controller);
    }

    public void SubscribeOnLateUpdate(ILateUpdateble controller)
    {
        lateUpdatebleControllers.Add(controller);
    }

    public void SubscribeOnDispose(IDisposable controller)
    {
        disposebleControllers.Add(controller);
    }

    void IControlledController.LocalLateUpdate()
    {
        for(int i = 0; i < lateUpdatebleControllers.Count; i++)
        {
            lateUpdatebleControllers[i].LocalLateUpdate();
        }
    }

    void IControlledController.LocalUpdate()
    {
        for (int i = 0; i < updatebleControllers.Count; i++)
        {
            updatebleControllers[i].LocalUpdate();
        }
    }

    void IControlledController.Dispose()
    {
        for (int i = 0; i < disposebleControllers.Count; i++)
        {
            disposebleControllers[i].Dispose();
        }
    }
}
