var switchgears;
async function refreshSwithgears() {
    var myRequest = new Request('/api/Switchgears');
    var response = await fetch(myRequest);
    response = await response.json();
    switchgears = response;
}

async function postData(url = '', data = {}) {
    const response = await fetch(url, {
        method: 'POST',
        mode: 'cors',
        cache: 'no-cache',
        credentials: 'same-origin',
        headers: {
            'Content-Type': 'application/json'
        },
        redirect: 'follow',
        referrerPolicy: 'no-referrer',
        body: JSON.stringify(data)
    });
    return await response.json();
}

function getPointsToPlot() {
    var res = [];
    var vpal = document.querySelector('div[name="pointsPallete"]');
    for (var i = 0; i < vpal.children.length; i++) {
        if (vpal.children[i].hasAttribute('point')) {
            var pm = JSON.parse(vpal.children[i].getAttribute("point"));
            res.push(pm)
        }
    }
    return res;
}

async function getDataToPlot() {
    var points = getPointsToPlot();    
    if (points.length > 0) {
        var res = [];
        for (var i = 0; i < points.length; i++) {
            points[i].data = [];
            res.push(points[i].id);
        }
        if (getDateBetwen()) {
            var ManyPointsData = getDateBetwen();
            ManyPointsData.PointsId = res;
            await postData('/api/PointData', ManyPointsData).then((data) => {               
                if (data.length > 0) {
                    data.forEach(function (m) {
                        var point = points.find(function (elem) {
                            return elem.id == m.idSGSPoint;
                        });
                        if (point) {
                            point.data.push(m);

                        }
                    });

                }

            });
            return points;
        } else {
            await postData('/api/PointData/GetPoinstData', res).then((data) => {                
                if (data.length > 0) {
                    data.forEach(function (m) {
                        var point = points.find(function (elem) {
                            return elem.id == m.idSGSPoint;
                        });
                        if (point) {
                            point.data.push(m);

                        }
                    });

                }

            });
            return points;
        }        
    }
    else {
        alert("Не удалось получить данные о точках для построения./n Проверьте, указаны ли у вас точки!");
    }
}

function getDateBetwen() {
    var ds = document.getElementById("DateS").value;
    if (ds) {
       var ts = document.getElementById("TimeS").value;
        if (ts) {
            ds = ds + 'T' + ts;
        }
        var dp = document.getElementById("DatePo").value;
        if (dp) {
            var tp = document.getElementById("TimePo").value;
            if (tp) {
                dp = dp + 'T' + tp;
            }
        }
        else {
            dp = new Date();
        }
        var res = {};
        res.DateFrom =ds;
        res.DateTo = dp;

        return res;
    }
}

function viewData() {
    var cpal = document.querySelector('div[name="configPallete"]');
    var rb = cpal.querySelector('input[type="radio"]:checked');
    clearChart();
    switch (rb.getAttribute("valueAsData")) {
        case "Graph": plotGraph(); break;
        case "Table": BuildArhiveTable(); break;
    }
}

function clearChart() {
    var chart = document.getElementById('curve_chart');
    for (var i = chart.children.length - 1; i > -1; i--) {
        chart.children[i].remove();
    }
}

async function sendPoints(event) {
    var table = event.target.parentElement.querySelector("table");
    var res = [];
    for (var i = 1; i < table.rows.length; i++) {
        var row = table.rows[i];
        var cell = row.cells[0];
        if (cell.hasAttribute("point")) {
            res.push(getPointDataFR(row));
        }        
    }  
    console.log(res);
    var pd = await putData("/api/PointData/-1", res);    
    for (var i = 0; i < pd.length; i++) {
        table.rows[pd[i].mNumber].cells[3].innerText = pd[i].messadgeString;
    }
    
}

function getPointDataFR(row) {
    var res = {};
    var cell = row.cells[0];
    var point = JSON.parse(cell.getAttribute("point"));
    res.IdSGSPoint = point.id;
    cell = row.cells[1];
    res.PointValue = cell.innerText.replace(",",".");
    cell = row.cells[2];
    res.DateOfValue = cell.innerText;
    res.Explantation = "added in browser";
    return res;
}

async function putData(url = '', data = {}) {
    const response = await fetch(url, {
        method: 'PUT',
        mode: 'cors',
        cache: 'no-cache',
        credentials: 'same-origin',
        headers: {
            'Content-Type': 'application/json'
        },
        redirect: 'follow',
        referrerPolicy: 'no-referrer',
        body: JSON.stringify(data)
    });
    return await response.json();
}

async function BuildArhiveTable() {
    var aTable = await getArhTable();
    clearChart();
    var res = document.createElement('TABLE');
    res.classList.add("scrolableTable");
    res.appendChild(getArhHead(aTable.columnNames));
    res.appendChild(document.createElement('tbody'));
    for (var i = 0; i < aTable.Rows.length; i++) {
        var row = res.children[1].insertRow(-1);
        for (var j = 0; j < aTable.columnNames.length; j++) {
            var cell = row.insertCell(-1);
            cell.innerText = getAText(aTable.Rows[i], j);
        }
    }
    let sortedRows = Array.from(res.rows)
        .slice(1)
        .sort((rowA, rowB) => rowA.cells[0].innerHTML > rowB.cells[0].innerHTML ? 1 : -1);

    res.tBodies[0].append(...sortedRows);

    document.getElementById('curve_chart').appendChild(res);
}

function getArhHead(aHeadCells) {
    var res = document.createElement('thead');
    var tr = document.createElement('tr');
    res.appendChild(tr);
    for (var i = 0; i < aHeadCells.length; i++) {
        var th = document.createElement('th');
        tr.appendChild(th);
        th.innerText = aHeadCells[i].text;
    }
    return res;
}

function getAText(Row, ci) {
    var res = Row.cells.filter(
        function (cell) {
            return cell.index == ci;
        }
    )[0];
    if (res) {
        if (ci == 0) {
            return getDTStringRuFormat(res.value);
        } else {
            return res.value.toFixed(3);
        }
    } else {
        return '-';
    }
}

function getDTStringRuFormat(date) {
    var res = "";
    var yyyy = date.getFullYear();
    var mm = date.getMonth() < 9 ? "0" + (date.getMonth() + 1) : (date.getMonth() + 1); // getMonth() is zero-based
    var dd = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
    var hh = date.getHours() < 10 ? "0" + date.getHours() : date.getHours();
    var min = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
    var ss = date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds();

    res = dd + "." + mm + "." + yyyy + " " + hh + ":" + min + ":" + ss;
    return res;
}

async function getArhTable() {
    var res = {};
    var pointData = await getDataToPlot();
    res.columnNames = [];
    res.Rows = [];
    var hc = {};
    hc.index = 0;
    hc.text = "";
    res.columnNames.push(hc);
    if (pointData.length > 0) {
        for (var i = 0; i < pointData.length; i++) {
            var hc = {};
            hc.index = i + 1;
            hc.text = pointData[i].explantation;
            res.columnNames.push(hc);
        }

        for (var i = 0; i < pointData.length; i++) {
            setArhRow(res, pointData[i]);
        }
    }
    return res;
}

function setArhRow(arhTable, point) {
    if (point.data && point.data.length > 0) {
        var ci = getCellIndex(arhTable, point);
        for (var i = 0; i < point.data.length; i++) {
            var row = addDatePointValue(arhTable, point.data[i].dateOfValue);
            var bc = {
                index: ci,
                value: point.data[i].pointValue
            };
            row.cells.push(bc);
        }
    }
}

function getCellIndex(arhTable, point) {
    return arhTable.columnNames.filter(
        function (column) { return column.text == point.explantation; }
    )[0].index;
}

function addDatePointValue(arhTable, date) {
    var res = new Date(date);
    if (!arhTable.Rows.length || arhTable.Rows.length == 0) {
        var bc = {
            index: 0,
            value: res
        };
        var row = {
            index: arhTable.Rows.length + 1,
            cells: []
        };
        row.cells.push(bc);
        arhTable.Rows.push(row);
        return row;
    } else {
        var rowF = arhTable.Rows.filter(
            function (r) {
                return r.cells[0].value.valueOf() == res.valueOf();
            }
        )[0];
        if (rowF) {
            return rowF;
        } else {
            var bc = {
                index: 0,
                value: res
            };
            var row = {
                index: arhTable.Rows.length + 1,
                cells: []
            };
            row.cells.push(bc);
            arhTable.Rows.push(row);
            return row;
        }
    }

}

async function GetLastData() {
    await getLastPointData();
    var panel = document.querySelector('div[name="LastData"]');
    for (var i = panel.children.length - 1; i > -1; i--) {
        panel.children[i].remove();
    }
    for (var i = 0; i < switchgears.length; i++) {
        panel.appendChild(getCheckSwitch(switchgears[i]));
        panel.appendChild(getLabelSwitch(switchgears[i]));
        panel.appendChild(getPanelSwitch(switchgears[i]));
    }
}

function getCheckSwitch(switchgear) {   
    var res = document.createElement('input');
    res.type = "checkbox";
    res.id = "sw_" + switchgear.id;
    res.classList.add("hide");   
    return res;
}

function getLabelSwitch(switchgear) {
    var res = document.createElement('label');
    res.setAttribute("for", "sw_" + switchgear.id);
    res.innerText = switchgear.nameSw;
    res.style.width = "500px";
    return res;
}

function getPanelSwitch(switchgear) {
    var res = document.createElement('div');
    res.style.minWidth = "320px";
    for (var i = 0; i < switchgear.sections.length; i++) {
        res.appendChild(getCheckSection(switchgear.sections[i]));
        res.appendChild(getLabelSection(switchgear.sections[i]));
        res.appendChild(getPanelSection(switchgear.sections[i]));
    }
    return res;
}

function getCheckSection(section) {
    var res = document.createElement('input');
    res.type = "checkbox";
    res.id = "sec_" + section.id;
    res.classList.add("hide");
    return res;
}

function getLabelSection(section) {
    var res = document.createElement('label');
    res.setAttribute("for", "sec_" + section.id);
    res.innerText = section.shortName;
    res.style.width = "500px";
    return res;
}

function getPanelSection(section) {
    var res = document.createElement('div');
    res.style.minWidth = "300px";   
    res.appendChild(getPointsTable(section.points));
    return res;
}

function getPointsTable(points) {
    var res = document.createElement('table');
    res.classList.add('pointsTable');
    if (points && points.length > 0){
        var row = res.insertRow();
        var row1 = res.insertRow();
        for (var i = 0; i < points.length; i++) {
            var cell = row.insertCell();
            cell.style.background = '#00ff90';
            cell.colSpan = 2;
            var cb = getPointCB(points[i]);
            cb.setAttribute('onclick', 'checkPointData(event)');
            cell.appendChild(cb);
            cell = row1.insertCell();
            cell.id = "dtp_" + points[i].id;
            var cell1 = row1.insertCell();
            cell1.id = "vp_" + points[i].id;
            if (points[i].LastData) {
                var ld = points[i].LastData;
                if (ld.DT) {
                    cell.innerText = getDTStringRuFormat(ld.DT);
                } else {
                    cell.innerText = '-';
                }
                if (ld.value) {
                    cell1.innerText = ld.value.toFixed(3);
                } else {
                    cell1.innerText = '-';
                }
            } else {
                cell.innerText = '-';
                cell1.innerText = '-';
            }
        }
    } else {
        var row = res.insertRow();
        var cell = row.insertCell();
        cell.style.background = '#00ff90';
        cell.innerText = '-';
        row = res.insertRow();
        cell = row.insertCell();
        cell.innerText = '-';
    }
    return res;
}

async function getLastPointData() {
    var myRequest = new Request('/api/PointData');
    var response = await fetch(myRequest);
    response = await response.json();
    for (var i = 0; i < switchgears.length; i++) {
        if (switchgears[i].sections) {
            for (var j = 0; j < switchgears[i].sections.length; j++) {
                var sec = switchgears[i].sections[j];
                if (sec.points && sec.points.length > 0) {
                    for (var k = 0; k < sec.points.length; k++) {
                        var point = sec.points[k];                        
                        var pd = response.filter(
                            function (p) {
                                return p.idSGSPoint == point.id;
                            }
                        )[0];
                        if (pd) {
                            point.LastData = {};
                            point.LastData.DT = new Date(pd.dateOfValue);
                            point.LastData.value = pd.pointValue;
                        }
                        
                        
                    }
                    
                }
            }
        }
    }
}

async function refreshLastData() {
    try {
        var myRequest = new Request('/api/PointData');
        var response = await fetch(myRequest);
        response = await response.json();
        for (var i = 0; i < response.length; i++) {
            var dc = document.getElementById("dtp_" + response[i].idSGSPoint);
            dc.innerText = getDTStringRuFormat(new Date(response[i].dateOfValue));
            var vc = document.getElementById("vp_" + response[i].idSGSPoint);
            vc.innerText = response[i].pointValue.toFixed(3);
        }
    } catch{
        try {
            GetLastData();
        } catch{
            setTimeout(GetLastData, 5000);
        }
    }
}

function setSelectedPointInTable() {
    var div = document.querySelector('div[name="LastData"]');
    var vpal = document.querySelector('div[name="pointsPallete"]');
    for (var i = 0; i < vpal.children.length; i++) {
        var node = vpal.children[i];
        if (node.hasAttribute('point')) {
            var pm = JSON.parse(node.getAttribute("point"));
            var check = div.querySelector("input[type='checkbox'][pointId='" + pm.id + "']");
            check.checked = true;
        }
    }
}

function checkPointData(event) {
    var sgf = document.querySelector('div[name="LastData"]');
    var schb = sgf.querySelectorAll('[type="checkbox"][pointid]:checked');
    var vpal = document.querySelector('div[name="pointsPallete"]');
    for (var i = vpal.childNodes.length - 1; i > -1; i--) {
        var node = vpal.childNodes[i];
        node.remove();
    }
    if (schb.length > 0) {
        for (var i = 0; i < schb.length; i++) {
            vpal.appendChild(getPointSpan(schb[i]));
        }
        viewData();
    } else {
        clearChart()
    }
}


