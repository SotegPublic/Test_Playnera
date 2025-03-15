public sealed class GameInitializator
{
    public GameInitializator(GameStarterModel model, ISubscribingController mainController)
    {
        var dragNotificationsManager = new DragNotificationsManager();
        var userInputController = new UserInputController();
        userInputController.Init();

        var dragController = new DragController(userInputController.InputModel, model.DragSystemInitModel, dragNotificationsManager);
        var dragingObjectMoveController = new DragingObjectMoveController(userInputController.InputModel);
        var freeFallController = new FreeFallObjectsController(model.DragableObjects, model.GlobalHeightLimit);
        var scaleController = new ScaleController(model.ScaleControllerInitModel);
        var cameraMoveController = new CameraMoveController(userInputController.InputModel, model.CameraMoveInitModel);


        dragNotificationsManager.Subscribe(dragingObjectMoveController);
        dragNotificationsManager.Subscribe(cameraMoveController);
        dragNotificationsManager.Subscribe(scaleController);

        mainController.SubscribeOnUpdate(userInputController);
        mainController.SubscribeOnUpdate(dragingObjectMoveController);

        for(int i = 0; i < model.DragableObjects.Length; i++)
        {
            model.DragableObjects[i].InjectParameters(model.SurfacesInitModel);
            mainController.SubscribeOnUpdate(model.DragableObjects[i]);
        }
        mainController.SubscribeOnUpdate(freeFallController);

        mainController.SubscribeOnLateUpdate(cameraMoveController);

        mainController.SubscribeOnDispose(userInputController);
        mainController.SubscribeOnDispose(dragController);
    }
}
