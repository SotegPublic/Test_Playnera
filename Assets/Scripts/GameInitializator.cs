public sealed class GameInitializator
{
    public GameInitializator(GameStarterModel model, ISubscribingController mainController)
    {
        var dragNotificationsManager = new DragNotificationsManager();
        var userInputController = new UserInputController();
        userInputController.Init();

        var objectModelsHolder = new DragableObjectsModelsHolder(model.DragableObjects);

        var dragController = new DragController(userInputController.InputModel, model.DragSystemInitModel, dragNotificationsManager);
        var dragingObjectMoveController = new DragingObjectMoveController(userInputController.InputModel);
        var objectStickingController = new ObjectsStickingController(objectModelsHolder.ObjectModels, model.SurfacesInitModel);
        var freeFallController = new FreeFallObjectsController(objectModelsHolder.ObjectModels, model.GlobalHeightLimit);
        var scaleController = new ScaleController(model.ScaleControllerInitModel);
        var cameraMoveController = new CameraMoveController(userInputController.InputModel, model.CameraMoveInitModel);


        dragNotificationsManager.Subscribe(dragingObjectMoveController);
        dragNotificationsManager.Subscribe(cameraMoveController);
        dragNotificationsManager.Subscribe(scaleController);

        mainController.SubscribeOnUpdate(userInputController);
        mainController.SubscribeOnUpdate(dragingObjectMoveController);
        mainController.SubscribeOnUpdate(objectStickingController);
        mainController.SubscribeOnUpdate(freeFallController);

        mainController.SubscribeOnLateUpdate(cameraMoveController);

        mainController.SubscribeOnDispose(userInputController);
        mainController.SubscribeOnDispose(dragController);
    }
}
