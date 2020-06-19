async function getGTable() {
    var values = await getDataToPlot();
    if (values.length > 0) {
        var res = new google.visualization.DataTable();
        res.addColumn('datetime', 'Дата');
        for (var i = 0; i < values.length; i++) {
            res.addColumn('number', values[i].shortName);
        }        

        var ri = 0;
        for (var i = 0; i < values.length; i++) {
            if (values[i].data.length > 0) {
                for (var j = 0; j < values[i].data.length; j++) {
                    res.addRow();
                    res.setCell(ri, 0, new Date(values[i].data[j].dateOfValue));
                    res.setCell(ri, i + 1, values[i].data[j].pointValue);
                    ri++;
                }
            }
        }
        return res;
    }
}

async function plotGraph() {
    await google.charts.load('current', { 'packages': ['corechart'], 'language': 'ru' });
    var dt = await getGTable();     
    google.charts.setOnLoadCallback(drawChart);
    function drawChart() {       
        var data = dt;
        var options = {
            curveType: 'function',
            legend: { position: 'bottom' }
        };
        var chart = new google.visualization.LineChart(document.getElementById('curve_chart'));
        chart.draw(data, options);
    }
}