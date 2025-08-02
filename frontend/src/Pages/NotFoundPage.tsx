import React from 'react';
import { Result, Button, Card } from 'antd';
import { Link } from 'react-router-dom';
import type { CSSProperties } from 'react';

interface NotFoundProps {
    isDarkTheme?: boolean;
}

const NotFoundPage: React.FC<NotFoundProps> = ({ isDarkTheme }) => {
    const cardStyle: CSSProperties = {
        maxWidth: 800,
        margin: '0 auto',
        textAlign: 'center',
        backgroundColor: isDarkTheme ? '#1f1f1f' : '#fff',
    };

    return (
        <Card style={cardStyle}>
            <Result
                status="404"
                title="404"
                subTitle="Страница не найдена"
                extra={
                    <Link to="/main">
                        <Button type="primary">На главную</Button>
                    </Link>
                }
            />
        </Card>
    );
};

export default NotFoundPage;