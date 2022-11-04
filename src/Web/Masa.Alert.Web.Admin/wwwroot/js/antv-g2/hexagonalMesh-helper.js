const addPolygon = (domRef, d) => {
    let chart = domRef.chart;
    var dv = new DataSet.View().source(d, {
        type: 'hex',
        width: 120,
        height: 120
    });

    var bgView = chart.view();
    bgView.source(dv);
    bgView.polygon().position('x*y').color('#FFFFFF').opacity(0.5).style({
        stroke: '#299F00',
        lineWidth: 4
    }).label('key', {
        htmlTemplate: function formatter(text, item, index) {
            console.log(text, item, index);
            let d = item._origin;
            var html = `<p style="text-align: center;font-size: 12px;">${d.name}</p>`;
            let items = d.items.slice(0.3);
            items.forEach(e => {
                html += `<p style='width: max-content;background: #A3AED0;font-size: 10px;'>${e.name}</p>`
            });
            return html;
        }
    })
        .tooltip('name');
}


export function init(domRef, data) {
    let chart = new G2.Chart({
        container: 'mountNode',
        forceFit: true,
        height: window.innerHeight,
        padding: window.innerHeight / 24
    });
    domRef.chart = chart;
    chart.scale({
        x: {
            nice: false,
            sync: true
        },
        y: {
            nice: false,
            sync: true
        }
    });
    chart.coord().reflect(); // 视数据而定要不要翻转 Y 轴。
    chart.tooltip({
        showTitle: false
    });
    chart.axis(false);
    chart.tooltip({
        showTitle: false,
        itemTpl: `<p>MASA Project B</p>`
    });
}

export function render(domRef) {
    let chart = domRef.chart;
    chart.render();
}