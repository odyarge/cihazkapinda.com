const ENV = {
  dev: {
    apiUrl: 'http://localhost:44310',
    oAuthConfig: {
      issuer: 'http://localhost:44310',
      clientId: 'Cihazkapinda_App',
      clientSecret: '1q2w3e*',
      scope: 'offline_access Cihazkapinda',
    },
    localization: {
      defaultResourceName: 'Cihazkapinda',
    },
  },
  prod: {
    apiUrl: 'http://localhost:44310',
    oAuthConfig: {
      issuer: 'http://localhost:44310',
      clientId: 'Cihazkapinda_App',
      clientSecret: '1q2w3e*',
      scope: 'offline_access Cihazkapinda',
    },
    localization: {
      defaultResourceName: 'Cihazkapinda',
    },
  },
};

export const getEnvVars = () => {
  // eslint-disable-next-line no-undef
  return __DEV__ ? ENV.dev : ENV.prod;
};
