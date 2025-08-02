import React from 'react';
import { Layout, Menu, Switch, Divider, Typography } from 'antd';
import {
    FileTextFilled,
    LockFilled,
    MoonOutlined,
    SunOutlined, CarryOutFilled, HeartFilled, DeleteFilled,
} from '@ant-design/icons';
import { Link, useLocation } from 'react-router-dom';
import type { CSSProperties } from 'react';

const { Sider } = Layout;
const { Text } = Typography;

interface SidebarProps {
    isDarkTheme: boolean;
    collapsed: boolean;
    onThemeChange: (isDark: boolean) => void;
    style?: CSSProperties;
    className?: string;
}

const SidebarComponent: React.FC<SidebarProps> = ({
                                                      isDarkTheme,
                                                      collapsed,
                                                      onThemeChange,
                                                      style,
                                                      className,
                                                  }) => {
    const location = useLocation();

    const mainMenuItems = [
        {
            key: '/main',
            icon: <CarryOutFilled />,
            label: <Link to="/main">Главная</Link>,
        },
        {
            key: '/favorite',
            icon: <HeartFilled />,
            label: <Link to="/favorite">Избранное</Link>,
        },
        {
            key: '/deleted',
            icon: <DeleteFilled />,
            label: <Link to="/deleted">Удаленные</Link>,
        },
    ];

    const footerMenuItems = [
        {
            key: '/privacy',
            icon: <LockFilled />,
            label: <Link to="/privacy">Политика конфиденциальности</Link>,
        },
        {
            key: '/terms',
            icon: <FileTextFilled />,
            label: <Link to="/terms">Условия использования</Link>,
        },
    ];

    const siderStyle: CSSProperties = {
        background: isDarkTheme ? '#141414' : '#fff',
        display: 'flex',
        flexDirection: 'column',
        ...style,
    };

    return (
        <Sider
            trigger={null}
            collapsible
            collapsed={collapsed}
            width={200}
            style={siderStyle}
            className={className}
        >
            <div style={{ flex: 1 }}>
                <div className="demo-logo-vertical" />
                <Menu
                    theme={isDarkTheme ? 'dark' : 'light'}
                    mode="inline"
                    selectedKeys={[location.pathname]}
                    items={mainMenuItems}
                    style={{ borderRight: 'none', backgroundColor: 'transparent' }}
                />
            </div>

            {/* Дополнительные ссылки */}
            <div style={{ marginTop: 'auto' }}>
                <Divider style={{
                    margin: '8px 0',
                    borderColor: isDarkTheme ? 'rgba(255, 255, 255, 0.12)' : 'rgba(0, 0, 0, 0.06)'
                }} />
                <Menu
                    theme={isDarkTheme ? 'dark' : 'light'}
                    mode="inline"
                    selectedKeys={[location.pathname]}
                    items={footerMenuItems}
                    style={{ borderRight: 'none', backgroundColor: 'transparent' }}
                />
            </div>

            {/* Переключатель темы */}
            <div style={{ padding: '16px', textAlign: 'center' }}>
                <Divider style={{
                    margin: '8px 0',
                    borderColor: isDarkTheme ? 'rgba(255, 255, 255, 0.12)' : 'rgba(0, 0, 0, 0.06)'
                }} />
                <Switch
                    checked={isDarkTheme}
                    onChange={onThemeChange}
                    checkedChildren={<MoonOutlined />}
                    unCheckedChildren={<SunOutlined />}
                    style={{ marginBottom: '8px' }}
                />
                <Text style={{
                    color: isDarkTheme ? 'rgba(255, 255, 255, 0.45)' : 'rgba(0, 0, 0, 0.45)',
                    fontSize: 12,
                    display: 'block'
                }}>
                    MyNotes by Jiffy
                </Text>
            </div>
        </Sider>
    );
};

export default SidebarComponent;