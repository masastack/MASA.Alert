let util = {
    scrollTop: (dom, id) => {
        if (id) {
            document.getElementById(id).scrollTop = 0;
        }
        else {
            dom.scrollTop = 0;
        }
    },
    isJson: (str) => {
        if (typeof str == 'string') {
            try {
                var obj = JSON.parse(str);
                if (typeof obj == 'object' && obj) {
                    return true;
                } else {
                    return false;
                }

            } catch (e) {
                console.log('error：' + str + '!!!' + e);
                return false;
            }
        }
    },
    jsonFormat: (dom, str) => {
        if (!dom || !isJSON(str)) {
            return;
        }

        str = JSON.stringify(JSON.parse(str), null, 2);
        str = str
            .replace(/&/g, '&')
            .replace(/</g, '<')
            .replace(/>/g, '>');
        str = str.replace(/("(\\u[a-zA-Z0-9]{4}|\\[^u]|[^\\"])*"(\s*:)?|\b(true|false|null)\b|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?)/g, function (match) {
            var cls = 'number';
            if (/^"/.test(match)) {
                if (/:$/.test(match)) {
                    cls = 'key';
                } else {
                    cls = 'string';
                }
            } else if (/true|false/.test(match)) {
                cls = 'boolean';
            } else if (/null/.test(match)) {
                cls = 'null';
            }
            return '<span class="' + cls + '">' + match + '</span>';
        });
        dom.innerHTML = str;
    },
    screenSize: (editorDom) => {
        let screenWidth = document.body.clientWidth || document.documentElement.clientWidth;
        let screenHeight = document.body.clientHeight || document.documentElement.clientHeight;
        let defWidth = 1920;
        let defHeight = 1080;
        let xScale = screenWidth / defWidth;
        let yScale = screenHeight / defHeight;
        editorDom.style.cssText += ';transform: scale(' + xScale + ',' + yScale + ');transform-origin: 0 0 0';

        window.addEventListener('resize', () => {
            let screenWidth = document.body.clientWidth || document.documentElement.clientWidth;
            let screenHeight = document.body.clientHeight || document.documentElement.clientHeight;
            xScale = screenWidth / defWidth;
            yScale = screenHeight / defHeight;
            editorDom.style.cssText += ';transform: scale(' + xScale + ',' + yScale + ');transform-origin: 0 0 0';
        }, false)
    }
}

window.util = util;

function isJSON(str) {
    if (typeof str == 'string') {
        try {
            var obj = JSON.parse(str);
            if (typeof obj == 'object' && obj) {
                return true;
            } else {
                return false;
            }

        } catch (e) {
            console.log('error：' + str + '!!!' + e);
            return false;
        }
    }
    console.log('It is not a string!')
}