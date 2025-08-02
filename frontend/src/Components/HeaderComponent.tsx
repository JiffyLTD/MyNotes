import React, {useEffect, useState} from 'react';
import {Layout, Button, Dropdown, Menu, Avatar, Space} from 'antd';
import {MenuFoldOutlined, MenuUnfoldOutlined, LogoutOutlined, UserOutlined} from '@ant-design/icons';
import type {CSSProperties} from 'react';
import {useKeycloak} from "@react-keycloak/web";

const {Header} = Layout;

interface HeaderProps {
    isDarkTheme: boolean;
    collapsed: boolean;
    onToggleCollapse: () => void;
    style?: CSSProperties;
    className?: string;
}

export interface CustomKeycloakToken {
    preferred_username?: string;
}

const HeaderComponent: React.FC<HeaderProps> =
    ({
         isDarkTheme,
         collapsed,
         onToggleCollapse,
         style,
         className
     }) => {
        const {keycloak} = useKeycloak();
        const [userName, setUserName] = useState<string | undefined>();

        useEffect(() => {
            if (keycloak.tokenParsed) {
                const parsed = keycloak.tokenParsed as CustomKeycloakToken;
                setUserName(parsed.preferred_username);
            }
        }, [keycloak.tokenParsed]);

        const headerStyle: CSSProperties = {
            padding: 0,
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'space-between',
            background: isDarkTheme ? '#141414' : '#fff',
            borderBottom: `1px solid ${isDarkTheme ? 'rgba(255, 255, 255, 0.12)' : 'rgba(0, 0, 0, 0.06)'}`,
            ...style
        };

        const buttonStyle: CSSProperties = {
            fontSize: '16px',
            width: 64,
            height: 64,
            color: isDarkTheme ? 'rgba(255, 255, 255, 0.85)' : 'rgba(0, 0, 0, 0.85)'
        };

        const titleStyle: CSSProperties = {
            margin: 0,
            color: isDarkTheme ? 'rgba(255, 255, 255, 0.85)' : 'rgba(0, 0, 0, 0.85)'
        };

        const userMenu = (
            <Menu>
                <Menu.Item key="logout" icon={<LogoutOutlined/>} onClick={() => keycloak.logout()}>
                    Выйти
                </Menu.Item>
            </Menu>
        );

        return (
            <Header style={headerStyle} className={className}>
                <Space>
                    <Button
                        type="text"
                        icon={collapsed ? <MenuUnfoldOutlined/> : <MenuFoldOutlined/>}
                        onClick={onToggleCollapse}
                        style={buttonStyle}
                    />
                    <h1 style={titleStyle}>MyNotes</h1>
                </Space>
                <Dropdown overlay={userMenu} placement="bottomRight">
                    <div style={{
                        display: 'flex',
                        alignItems: 'center',
                        gap: 8,
                        cursor: 'pointer',
                        padding: '8px 12px',
                        borderRadius: 4
                    }}>
                         <span style={{color: isDarkTheme ? 'rgba(255, 255, 255, 0.85)' : 'rgba(0, 0, 0, 0.85)'}}>
                             {userName}
                         </span>
                        <Avatar
                            size="default"
                            icon={<UserOutlined/>}
                            style={{backgroundColor: isDarkTheme ? '#1890ff' : '#87d068'}}
                        />
                    </div>
                </Dropdown>

            </Header>
        );
    };

export default HeaderComponent;