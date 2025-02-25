this.pkg = require('./package.json');

this.apiVersion = () => `v${this.pkg.version.substring(0, 1)}`;

this.devDependencies = () => {

    return Object.keys(this.pkg.devDependencies).map(key => {
        return `!node_modules/${key}/**`
    });
}

this.patterns = () => {
    return [
        'src/**/*',
        'config/**/*.json'].concat(this.devDependencies());
}

module.exports = this;
