# 📝 MyNotes

**MyNotes** — pet-проект для создания и управления заметками с поддержкой избранного. Построен с использованием современной технологической базы, микросервисной архитектуры и принципов чистой архитектуры.

---

## 🔧 Технологии

### 🖥️ Frontend

- React
- Ant Design (Ant UI Kit)

### 🧠 Backend (.NET 9)

- ASP.NET Core
- Чистая архитектура (слои: `Domain`, `Application`, `Infrastructure`, `Presentation`)
- Вынесенные проекты:
    - `InternalEvents` — внутренняя событийная модель
    - `Grpc` — gRPC контракты и взаимодействие между микросервисами

### 🧩 Микросервисы

- `NoteService` — CRUD для заметок
- `FavoriteNoteService` — работа с избранными заметками

### 🛠️ Инфраструктура

- PostgreSQL — хранилище данных
- RabbitMQ — асинхронная коммуникация между сервисами
- Keycloak — аутентификация и авторизация
- Docker Compose — управление запуском компонентов

---

## 🚀 Запуск инфраструктуры
> Из папки infrastucture

```
docker-compose up --build
```

## Urls

Frontend: http://localhost:3000

NoteService Swagger: http://localhost:5000/swagger

FavoriteNoteService Swagger: http://localhost:5100/swagger

Keycloak UI: http://localhost:8080

RabbitMq UI: http://localhost:15672