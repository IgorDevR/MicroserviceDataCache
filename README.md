# MicroserviceDataCache
Описание
Проект MicroserviceDataCache предназначен для управления задачами пользователей и администраторов, а также для кэширования данных с целью оптимизации производительности. 
Приложение использует in-memory базу данных для эмуляции работы с данными и предоставляет REST API для взаимодействия с данными.

## Структура проекта
Проект состоит из нескольких основных компонентов:

## Контроллеры:
UserTasksController: Управляет задачами пользователей и кэшированием задач пользователей.
AdminTasksController: Управляет задачами администраторов и кэшированием задач администраторов.
UserCategoriesController: Управляет категориями пользователей и ответственностью за задачи.

## Сервисы:
TaskUserCacheAggregate: Агрегирует данные задач пользователей и обновляет кэш.
AdminTaskUserCacheAggregate: Агрегирует данные задач администраторов и обновляет кэш.

## Использует in-memory базу данных для хранения данных во время работы приложения.

## Требования
.NET 8.0 SDK
Visual Studio 2022 или другой поддерживаемый редактор кода

## Использование
Приложение предоставляет REST API для взаимодействия с данными. Ниже приведены примеры доступных конечных точек:

## UserTasksController
### Получение всех задач пользователей:
GET /api/usertasks/tasks

### Добавление новой задачи пользователя:
POST /api/usertasks/tasks

### Получение кэшированных данных задач пользователей:
GET /api/usertasks/taskcache

### Агрегация данных задач пользователей и обновление кэша:
POST /api/usertasks/taskcache/aggregate

## AdminTasksController

### Получение всех задач администраторов:
GET /api/admintasks/tasks

### Добавление новой задачи администратора:
POST /api/admintasks/tasks

### Получение кэшированных данных задач администраторов:
GET /api/admintasks/taskcache

### Агрегация данных задач администраторов и обновление кэша:
POST /api/admintasks/taskcache/aggregate

## UserCategoriesController
### Получение всех категорий пользователей:
GET /api/usercategories/categories

### Добавление новой категории пользователя:
POST /api/usercategories/categories

### Получение всех задач с ответственностью:
GET /api/usercategories/taskresponsibilities

### Добавление новой ответственности задачи:
POST /api/usercategories/taskresponsibilities
