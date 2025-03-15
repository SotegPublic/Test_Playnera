public sealed class DragableObjectsModelsHolder
{
    private ObjectModel[] objectModels;

    public ObjectModel[] ObjectModels => objectModels;

    public DragableObjectsModelsHolder(DragableObjectView[] views)
    {
        objectModels = new ObjectModel[views.Length];

        for (int i = 0; i < views.Length; i++)
        {
            objectModels[i] = new ObjectModel(views[i]);
        }
    }
}
