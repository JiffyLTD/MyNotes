## Локальная инфраструктура

### Компоненты
* Keycloak 26.0.7
* Postgres 17.2

---

### Инструкция
Все действия проводить в папке localInfrastructure и по необходимости
#### Запуск
Путь /localInfrastructure

```
docker compose up -d
```

#### Генерация ssl сертификатов
Путь /localInfrastructure/ssl (в консоли bash)

Генерировать в случае отсутствия сертификатов или окончания срока их действия

```
sh generate.sh
```

#### Доверие сертификатам
Путь /localInfrastructure/ssl (требуются права администратора)

```
trust.bat
```

или

```
./trust.bat
```

В случае когда браузер не верит сертификату, но локально сертификат доверенный, проверьте строки

```
127.0.0.1       localhost
::1             localhost
```
В файле hosts по пути C:\Windows\System32\drivers\etc

---

### Keycloak

Url :
```
https://localhost:8443
```

Realm name :
```
mynotes-dev
```

Clients :
```
mynotes-user
```

Test users

| Username  | Password |Role|
|-----------|----------|----|
| ii.ivanov | 12345678 |User|

---
