export function currentTheme() {
    const current = localStorage.getItem('theme');
    if (current) {
        console.log('current theme was available');
        return current;
    } else {

        const darkThemeMq = window.matchMedia("(prefers-color-scheme: dark)");
        let result = 'light';
        if (darkThemeMq.matches) {
            result = 'dark';
        }

        localStorage.setItem('theme', result);
        return result;
    }
}

export function switchTheme(isDark) {
    localStorage.setItem('theme', isDark ? 'dark' : 'light');
    document.body.classList = isDark ? 'dark' : 'light';
}
