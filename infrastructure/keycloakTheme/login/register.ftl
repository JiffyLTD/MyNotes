<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>Регистрация — MyNotes</title>
    <link rel="stylesheet" href="${url.resourcesPath}/css/styles.css">
</head>
<body>
<div class="page-wrapper">
    <h1 class="site-title">MyNotes</h1>

    <div class="login-box">
        <h2>Регистрация</h2>
        <form action="${url.registrationAction}" method="post">
            <input type="text" id="username" name="username" placeholder="Имя пользователя" value="${(register.formData.username!'')}" required />

            <input type="email" id="email" name="email" placeholder="Email" value="${(register.formData.email!'')}" required />

            <input type="password" id="password" name="password" placeholder="Пароль" required />

            <input type="password" id="password-confirm" name="password-confirm" placeholder="Подтвердите пароль" required />

            <button type="submit">Зарегистрироваться</button>
        </form>
        <div class="links">
            <a href="${url.loginUrl}">Уже есть аккаунт? Войти</a>
        </div>
    </div>
</div>
</body>
</html>
