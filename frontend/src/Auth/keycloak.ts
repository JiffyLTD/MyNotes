import Keycloak from 'keycloak-js';

const keycloakConfig = {
    url: 'http://localhost:8080/',
    realm: 'mynotes-dev',
    clientId: 'mynotes-user',
};

// @ts-ignore
const KeycloakClientService = new Keycloak(keycloakConfig);

export default KeycloakClientService;

