<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>Вход — MyNotes</title>
    <link rel="stylesheet" href="${url.resourcesPath}/css/styles.css">
</head>
<body>
<div class="page-wrapper">
    <h1 class="site-title">MyNotes</h1>

    <div class="login-box">
        <h2>Вход</h2>
        <form action="${url.loginAction}" method="post">
            <input type="text" name="username" placeholder="Имя пользователя" value="${(login.username!'')}" required />
            <input type="password" name="password" placeholder="Пароль" required />
            <button type="submit">Войти</button>
        </form>
        <div class="links">
            <a href="${url.registrationUrl}">Регистрация</a>
        </div>
    </div>
</div>
</body>
</html>
