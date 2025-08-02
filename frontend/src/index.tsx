import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import keycloak from './Auth/keycloak';
import {ReactKeycloakProvider} from '@react-keycloak/web';

ReactDOM.createRoot(document.getElementById('root')!).render(
    <ReactKeycloakProvider
        authClient={keycloak}
        initOptions={{
            onLoad: 'check-sso',
            silentCheckSsoRedirectUri: window.location.origin + '/silent-check-sso.html'
        }}
    >
        <App/>
    </ReactKeycloakProvider>
);