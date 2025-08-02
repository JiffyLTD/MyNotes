import React from 'react';
import { Card, Typography } from 'antd';
import type { CSSProperties } from 'react';

const { Title, Paragraph } = Typography;

interface TermsPageProps {
    isDarkTheme?: boolean;
}

const TermsPage: React.FC<TermsPageProps> = ({ isDarkTheme }) => {
    const cardStyle: CSSProperties = {
        maxWidth: 800,
        margin: '0 auto',
        backgroundColor: isDarkTheme ? '#1f1f1f' : '#fff',
    };

    return (
        <Card style={cardStyle}>
            <Title level={2}>Условия использования MyNotes</Title>
            <Paragraph>
                <strong>Версия 1.2</strong>
            </Paragraph>

            <Title level={4}>1. Технологический стек</Title>
            <Paragraph>
                Сервис использует:
                <ul>
                    <li><strong>Keycloak</strong> для управления пользователями и аутентификации</li>
                    <li><strong>Hangfire</strong> для выполнения фоновых задач и напоминаний</li>
                    <li><strong>PostgreSQL</strong> для хранения заметок и метаданных</li>
                </ul>
            </Paragraph>

            <Title level={4}>2. Особенности аутентификации</Title>
            <Paragraph>
                <ul>
                    <li>Для входа требуется учетная запись в Keycloak</li>
                    <li>Поддерживается сброс пароля через email</li>
                    <li>Можно подключить двухфакторную аутентификацию</li>
                    <li>Сессии автоматически завершаются после длительного неиспользования</li>
                </ul>
            </Paragraph>

            <Title level={4}>3. Фоновые задачи</Title>
            <Paragraph>
                Сервис может выполнять в фоне:
                <ul>
                    <li>Оптимизацию хранилища</li>
                </ul>
            </Paragraph>

            <Title level={4}>4. Ограничения</Title>
            <Paragraph>
                <ul>
                    <li>Хранение удаленных заметок - до 30 дней</li>
                </ul>
            </Paragraph>

            <Paragraph type="secondary" style={{ marginTop: 24 }}>
                <strong>Разработчик:</strong> Jiffy (pet-project)<br />
                <strong>Контакты:</strong> support@mynotes.jiffy<br />
                <strong>Дата вступления в силу:</strong> {new Date().toLocaleDateString()}
            </Paragraph>
        </Card>
    );
};

export default TermsPage;