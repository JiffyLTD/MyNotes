import React from 'react';
import { Card, Typography } from 'antd';
import type { CSSProperties } from 'react';

const { Title, Paragraph } = Typography;

interface PrivacyPageProps {
    isDarkTheme?: boolean;
}

const PrivacyPage: React.FC<PrivacyPageProps> = ({ isDarkTheme }) => {
    const cardStyle: CSSProperties = {
        maxWidth: 800,
        margin: '0 auto',
        backgroundColor: isDarkTheme ? '#1f1f1f' : '#fff',
    };

    return (
        <Card style={cardStyle}>
            <Title level={2}>Политика конфиденциальности MyNotes</Title>
            <Paragraph>
                <strong>Версия 1.2</strong>
            </Paragraph>

            <Title level={4}>1. Какие данные мы собираем</Title>
            <Paragraph>
                <ul>
                    <li><strong>Учетные данные</strong>: аутентификация через Keycloak (логин, email, идентификатор)</li>
                    <li><strong>Заметки пользователей</strong>: текст, метки и метаданные ваших заметок</li>
                    <li><strong>Технические данные</strong>: IP-адрес, тип браузера, информация об устройстве</li>
                    <li><strong>Журналы фоновых задач</strong>: информация о выполнении отложенных задач (Hangfire)</li>
                </ul>
            </Paragraph>

            <Title level={4}>2. Как мы используем данные</Title>
            <Paragraph>
                <ul>
                    <li>Для предоставления функционала приложения (хранение и синхронизация заметок)</li>
                    <li>Для выполнения отложенных задач (синхронизация, напоминания через Hangfire)</li>
                    <li>Для аутентификации и авторизации (интеграция с Keycloak)</li>
                    <li>Для технической поддержки и устранения неисправностей</li>
                </ul>
            </Paragraph>

            <Title level={4}>3. Хранение и защита данных</Title>
            <Paragraph>
                <ul>
                    <li>Аутентификация полностью делегирована Keycloak</li>
                    <li>Hangfire задачи выполняются в изолированной среде</li>
                </ul>
            </Paragraph>

            <Title level={4}>4. Планировщик задач (Hangfire)</Title>
            <Paragraph>
                Мы используем Hangfire для:
                <ul>
                    <li>Очистки неактивных данных</li>
                </ul>
                В логах задач сохраняется только техническая информация без содержимого заметок.
            </Paragraph>

            <Title level={4}>5. Аутентификация (Keycloak)</Title>
            <Paragraph>
                <ul>
                    <li>Все операции аутентификации выполняются через Keycloak</li>
                    <li>Мы не храним пароли - только идентификатор пользователя</li>
                    <li>Поддерживается 2FA и социальные логины</li>
                    <li>Сессии автоматически завершаются после 30 дней неактивности</li>
                </ul>
            </Paragraph>

            <Title level={4}>6. Права пользователей</Title>
            <Paragraph>
                Вы можете:
                <ul>
                    <li>Удалить аккаунт через панель управления Keycloak</li>
                    <li>Отозвать права доступа для устройства</li>
                    <li>Просматривать активные сессии</li>
                </ul>
                Для выполнения запросов напишите на privacy@mynotes.jiffy
            </Paragraph>

            <Paragraph type="secondary" style={{ marginTop: 24 }}>
                <strong>Разработчик:</strong> Jiffy (pet-project)<br />
                <strong>Контакты:</strong> support@mynotes.jiffy<br />
                <strong>Последнее обновление:</strong> {new Date().toLocaleDateString()}
            </Paragraph>
        </Card>
    );
};

export default PrivacyPage;