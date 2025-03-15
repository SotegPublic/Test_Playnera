# Test_Playnera

Реализовал визуальные механики референса:
- Движение камеры (при захваченном предмете смещением, без предмета перетягиванием, ограничение по краям)
- Размещение предметов на полу, столах, полках
- Доводка предметов к ближайшей точке на полке/столе
- Свободное падение предметов (в случае отсутствия на определенной дистанции "точек притяжения")
- После последней доработки были убраны все коллайдеры (кроме коллайдеров-триггеров у перетаскиваемых объектов, для реагирования на них рейкаста из камеры), система работает через расчет по высоте.
- Ну и коробочку добавил от себя))

Для поиска ближайших "точек притяжения" не используется Physics2D.OverlapCircle, только sqrMagnitude, с целью оптимизации. Все работает на едином апдейте/лейт апдейте.

Описание:
Запуск игры происходит через единую точку входа: [GameStarter](Assets/Scripts/GameStarter.cs), он создает инициализатор системы, передавая в него все модели и данные, необходимые для игры. Инициализатор создает все классы-контроллеры, а так же подписывает обсерверов на [DragNotificationsManager](Assets/Scripts/Controllers/DragFeature/DragNotificationsManager.cs) и [MainController](Assets/Scripts/Controllers/Core/MainController.cs), первый отвечает за информирование о наличии или отсутствии перетаскиваемого объекта в момент получения событий от инпута, второй занимается раздачей апдейтов и лейтапдейтов всем кому это нужно. Система управления камерой [CameraMoveController](Assets/Scripts/Controllers/Camera/CameraMoveController.cs) содержит в себе ~~стейтмашину(на минималках)~~ свитч обыкновенный, переключая способы движения в зависимости от параметров входящего оповещения. [DragController](Assets/Scripts/Controllers/DragFeature/DragController.cs) - является прослойкой межу инпутом и DragNotificationsManager, его задача определить схватил ли игрок что-либо или нет. За изменение скейла хватаемого объекта отвечает [ScaleController](Assets/Scripts/Controllers/DragFeature/ScaleController.cs). 

[DragingObjectMoveController](Assets/Scripts/Controllers/DragFeature/DragingObjectMoveController.cs) - контроллер управления объектами в момент когда игрок их хватает и перетягивает. 
[FreeFallObjectsController](Assets/Scripts/Controllers/Object/FreeFallObjectsController.cs) - контроллер управления объектами в момент свободного падения, когда игрок отпускает их. 
[ObjectsStickingController](Assets/Scripts/Controllers/Object/ObjectsStickingController.cs) - контроллер поиска и сближения с ближайшей "точкой притяжения", если таковая есть на дистанции притягивания. 

Все реализовано на новой инпут системе.


https://github.com/user-attachments/assets/abd15686-d35c-49b9-813c-0fed13dc4b5d

