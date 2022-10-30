function init() {
    const cssId = 'code-breaker-ui-mudblazor-css'
    if (!document.getElementById(cssId)) {
        var head = document.getElementsByTagName('head')[0];
        var link = document.createElement('link');
        link.id = cssId;
        link.rel = 'stylesheet';
        link.type = 'text/css';
        link.href = '_content/MudBlazor/MudBlazor.min.css';
        head.appendChild(link);
    }
    if (!document.getElementById(cssId)) {
        var head = document.getElementsByTagName('head')[0];
        var link = document.createElement('link');
        link.id = cssId;
        link.rel = 'stylesheet';
        link.type = 'text/css';
        link.href = 'https://fonts.googleapis.com/css?family=Roboto:300,400,500,700&display=swap';
        head.appendChild(link);
    }

    const jsId = 'code-breaker-ui-mudblazor-js'
    if (!document.getElementById(jsId)) {
        var head = document.getElementsByTagName('body')[0];
        var script = document.createElement('script');
        script.id = jsId;
        script.src = '_content/MudBlazor/MudBlazor.min.js';
        head.appendChild(script);
    }
}

init();
