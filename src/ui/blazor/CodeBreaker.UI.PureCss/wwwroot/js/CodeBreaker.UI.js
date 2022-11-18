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
}

init();
