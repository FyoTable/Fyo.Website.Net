import { HOST_ROOT } from '../../config/host-config';

export const AUTH_CONFIG = {
    domain: 'fyo.auth0.com',
    clientId: 'rHzoYMuoJajmQcDGdJjp5MLP14xUk7F4',
    callbackUrl: `${HOST_ROOT}/portal/callback`,
    audience: 'https://fyo.auth0.com/api/v2/'
};
