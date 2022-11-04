﻿function init() {
    const cssId = 'code-breaker-ui-css'
    if (!document.getElementById(cssId)) {
        var head = document.getElementsByTagName('head')[0];
        var link = document.createElement('link');
        link.id = cssId;
        link.rel = 'stylesheet';
        link.type = 'text/css';
        link.href = '_content/CodeBreaker.UI/CodeBreaker.UI.min.css';
        head.appendChild(link);
    }

    const jsfastUiId = 'code-breaker-ui-fastui-js'
    if (!document.getElementById(jsfastUiId)) {
        var head = document.getElementsByTagName('body')[0];
        var script = document.createElement('script');
        script.id = jsfastUiId;
        script.src = 'https://cdn.jsdelivr.net/npm/@fluentui/web-components@2.0.2/dist/web-components.min.js';
        head.appendChild(script);
    }
}

init();
