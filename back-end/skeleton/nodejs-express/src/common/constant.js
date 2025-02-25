const RegexPattern = {
    PASSWORD: /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*()_+\-=\[\]{};:'"\\|,.<>\/?]).{8,}$/
};

const UserField = {
    USERNAME: 'username',
    EMAIL: 'email',
    MOBILE_PHONE: 'mobilePhone'
};

const RoleType = {
    SYSTEM: 'System',
    ADMIN: 'Admin',
    CLIENT: 'Client'
};

const QueryOperator = {
    AND: 'and',
    OR: 'or'
};

const DateTimeConstant = {
    YEAR: 'year',
    MONTH: 'month',
    WEEK: 'week',
    DAY: 'day',
    HOUR: 'hour',
    MINUTE: 'minute',
    SECOND: 'second',
    LONG_FORMAT: 'YYYY-MM-DD HH:mm:ss',
    SHORT_FORMAT: 'YYYY-MM-DD',
    US_FORMAT: 'MM/DD/YYYY HH:mm:ss'
};

const EmailTemplateFileName = {
    FORGOT_PASSWORD: 'forgot-password',
    RESET_PASSWORD_SUCCESSFUL: 'reset-password-successful'
};

const EmailSubject = {
    RESET_PASSWORD: 'RESET PASSWORD',
    RESET_PASSWORD_SUCCESSFUL: 'RESET PASSWORD SUCCESSFUL'
};

export {
    RegexPattern,
    UserField,
    RoleType,
    QueryOperator,
    DateTimeConstant,
    EmailTemplateFileName,
    EmailSubject
};