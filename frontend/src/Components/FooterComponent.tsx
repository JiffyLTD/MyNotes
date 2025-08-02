import React from 'react';
import { Layout, Typography } from 'antd';
import { Link } from 'react-router-dom';
import type { CSSProperties } from 'react';

const { Footer } = Layout;
const { Text } = Typography;

interface FooterProps {
    isDarkTheme: boolean;
    style?: CSSProperties;
    className?: string;
}

const FooterComponent: React.FC<FooterProps> = ({ isDarkTheme, style, className }) => {
    const footerStyle: CSSProperties = {
        textAlign: 'center',
        background: isDarkTheme ? '#141414' : '#fff',
        color: isDarkTheme ? 'rgba(255, 255, 255, 0.65)' : 'rgba(0, 0, 0, 0.65)',
        borderTop: `1px solid ${isDarkTheme ? 'rgba(255, 255, 255, 0.12)' : 'rgba(0, 0, 0, 0.06)'}`,
        padding: '16px 24px',
        ...style,
    };

    return (
        <Footer style={footerStyle} className={className}>
            <div style={{ display: 'flex', justifyContent: 'center', gap: 24, flexWrap: 'wrap' }}>
                <Link to="/privacy" style={{ color: 'inherit' }}>
                    Политика конфиденциальности
                </Link>
                <Link to="/terms" style={{ color: 'inherit' }}>
                    Условия использования
                </Link>
            </div>
            <Text style={{ display: 'block', marginTop: 8 }}>
                © {new Date().getFullYear()} MyNotes - пет-проект Jiffy
            </Text>
        </Footer>
    );
};

export default FooterComponent;