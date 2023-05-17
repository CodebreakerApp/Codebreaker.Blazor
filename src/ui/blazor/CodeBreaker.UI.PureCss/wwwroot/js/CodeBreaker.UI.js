function init() {
    const cssId = 'code-breaker-ui-css'
    if (!document.getElementById(cssId)) {
        var head = document.getElementsByTagName('head')[0];
        var link = document.createElement('link');
        link.id = cssId;
        link.rel = 'stylesheet';
        link.type = 'text/css';
        link.href = '_content/CodeBreaker.UI/CodeBreaker.UI.bundle.scp.css';
        head.appendChild(link);
    }

    const pureUICssId = 'code-breaker-pure-ui-css'
    if (!document.getElementById(pureUICssId)) {
        var head = document.getElementsByTagName('head')[0];
        var link = document.createElement('link');
        link.id = pureUICssId;
        link.rel = 'stylesheet';
        link.type = 'text/css';
        link.href = '_content/CodeBreaker.UI/css/CodeBreaker.UI.PureCss.css';
        head.appendChild(link);
    }

    const iconCssId = 'icon-css-id';
    if (!document.getElementById(iconCssId)) {
        var head = document.getElementsByTagName('head')[0];
        var link = document.createElement('link');
        link.id = iconCssId;
        link.rel = 'stylesheet';
        link.type = 'text/css';
        link.href = 'https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css';
        head.appendChild(link);
    }

    const fontId = 'font-id';
    if (!document.getElementById(fontId)) {
        var head = document.getElementsByTagName('head')[0];
        var link = document.createElement('link');
        link.id = fontId;
        link.rel = 'stylesheet';
        link.type = 'text/css';
        link.href = 'https://fonts.googleapis.com/css?family=Montserrat:300,400,500,700&display=swap';
        head.appendChild(link);
    }
}

init();
