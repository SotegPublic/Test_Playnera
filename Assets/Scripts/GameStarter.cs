using System.Collections;
using UnityEngine;

public sealed class GameStarter : MonoBehaviour
{
    [SerializeField] private GameStarterModel model;

    private IControlledController mainController;

    private void Awake()
    {
        if (Screen.orientation != ScreenOrientation.LandscapeLeft &&
            Screen.orientation != ScreenOrientation.LandscapeRight)
        {
            Screen.orientation = ScreenOrientation.LandscapeRight;
        }
    }

    private void Start()
    {
        var controller = new MainController();
        mainController = controller;

        new GameInitializator(model, controller);
    }

    private void Update()
    {
        mainController.LocalUpdate();
    }

    private void LateUpdate()
    {
        mainController.LocalLateUpdate();
    }

    private void OnDestroy()
    {
        mainController.Dispose();
    }
}
