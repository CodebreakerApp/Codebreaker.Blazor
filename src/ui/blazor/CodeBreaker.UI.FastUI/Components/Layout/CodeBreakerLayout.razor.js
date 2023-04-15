export function isDevice() {
    return /android|webos|iphone|ipad|ipod|blackberry|iemobile|opera mini|mobile/i.test(navigator.userAgent);
}

export function switchHighlightStyle(dark) {
    if (dark) {
        const linkDark = document.querySelector(`link[title="dark"]`);
        if (linkDark) {
            linkDark.removeAttribute("disabled");
        }
        const linkLight = document.querySelector(`link[title="light"]`);
        if (linkLight) {
            linkLight.setAttribute("disabled", "disabled");
        }   
    }
    else {
        const linkLight = document.querySelector(`link[title="light"]`);
        if (linkLight) {
            linkLight.removeAttribute("disabled");
        }
        const linkDark = document.querySelector(`link[title="dark"]`);
        if (linkDark) {
            linkDark.setAttribute("disabled", "disabled");
        }
    }
}

export function isDarkMode() {
    let matched = window.matchMedia("(prefers-color-scheme: dark)").matches;

    if (matched)
        return true;
    else
        return false;
}

export function switchDirection(dir) {
    document.dir = dir;
}
