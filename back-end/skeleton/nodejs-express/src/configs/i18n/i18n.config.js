import i18next from 'i18next';
import Backend from 'i18next-fs-backend';
import { LanguageDetector } from 'i18next-http-middleware';

i18next
    .use(Backend)
    .use(LanguageDetector)
    .init({
        detection: {
            order: ['header']
        },
        backend: {
            loadPath: process.cwd() + '/src/locales/{{lng}}.json'
        },
        supportedLngs: ['en-US', 'fr-FR'],
        load: 'currentOnly',
        fallbackLng: 'en-US',
        interpolation: {
            prefix: '{{',
            suffix: '}}',
            maxReplaces: 10
        }
    });

export default i18next;