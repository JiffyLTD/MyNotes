import React, {useState, useEffect} from 'react';
import {Layout, ConfigProvider, theme, Space, Spin} from 'antd';
import {BrowserRouter as Router} from 'react-router-dom';
import AppRoutes from "./Routes/AppRoutes";
import FooterComponent from "./Components/FooterComponent";
import HeaderComponent from "./Components/HeaderComponent";
import SidebarComponent from "./Components/SidebarComponent";
import {useKeycloak} from "@react-keycloak/web";
import {LoadingOutlined} from '@ant-design/icons';
import NoteApiClient from "./Api/NoteApiClient";

const {Content} = Layout;

const App: React.FC = () => {
    const [collapsed, setCollapsed] = useState(false);
    const [isDarkTheme, setIsDarkTheme] = useState(() => {
        return localStorage.getItem('theme') === 'dark';
    });

    useEffect(() => {
        document.body.style.backgroundColor = isDarkTheme ? '#000' : '#fff';
        document.body.style.color = isDarkTheme ? 'rgba(255, 255, 255, 0.85)' : 'rgba(0, 0, 0, 0.85)';
        localStorage.setItem('theme', isDarkTheme ? 'dark' : 'light');
    }, [isDarkTheme]);

    const {keycloak, initialized} = useKeycloak();

    useEffect(() => {
        if (!initialized || !keycloak.authenticated) return;

        const interval = setInterval(async () => {
            try {
                const refreshed = await keycloak.updateToken(60);
                if (refreshed) {
                    console.log('Токен обновлён');
                }
                NoteApiClient.SetAccessToken(keycloak.token);
            } catch {
                console.warn('Ошибка при обновлении токена');
                keycloak.logout();
            }
        }, 30000);

        return () => clearInterval(interval);
    }, [keycloak, initialized]);

    if (!initialized) {
        return <div style={{
            height: '100vh',
            display: 'flex',
            justifyContent: 'center',
            alignItems: 'center',
            background: isDarkTheme ? '#000' : '#fff',
        }}>
            <Space direction="vertical" align="center">
                <Spin indicator={<LoadingOutlined style={{fontSize: 48}} spin/>}/>
                <span style={{color: isDarkTheme ? 'rgba(255, 255, 255, 0.85)' : 'rgba(0, 0, 0, 0.85)'}}>
                    Авторизация пользователя...
                </span>
            </Space>
        </div>;
    }

    if (!keycloak.authenticated)
        keycloak.login();

    NoteApiClient.SetAccessToken(keycloak.token);

    return (
        <Router>
            <ConfigProvider
                theme={{
                    algorithm: isDarkTheme ? theme.darkAlgorithm : theme.defaultAlgorithm,
                    components: {
                        Menu: {
                            itemSelectedBg: isDarkTheme ? '#434343' : '#f0f0f0',
                            itemSelectedColor: isDarkTheme ? '#ffffff' : '#1890ff',
                            itemHoverBg: isDarkTheme ? '#434343' : '#f5f5f5',
                        },
                    },
                }}
            >
                <Layout style={{minHeight: '100vh', background: 'transparent'}}>
                    <SidebarComponent
                        isDarkTheme={isDarkTheme}
                        collapsed={collapsed}
                        onThemeChange={setIsDarkTheme}
                    />
                    <Layout>
                        <HeaderComponent
                            isDarkTheme={isDarkTheme}
                            collapsed={collapsed}
                            onToggleCollapse={() => setCollapsed(!collapsed)}
                        />
                        <Content style={{margin: '16px'}}>
                            <AppRoutes isDarkTheme={isDarkTheme}/>
                        </Content>
                        <FooterComponent isDarkTheme={isDarkTheme}/>
                    </Layout>
                </Layout>
            </ConfigProvider>
        </Router>
    );
};

export default App;