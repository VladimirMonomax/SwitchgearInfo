function removeDialogWindow() {    
    var dw = document.getElementsByClassName("dialogWindow");
    for (var i = 0; i < dw.length; i++) {
        dw[i].remove();
    }
}

function getSGDialog() {
    removeDialogWindow();
    var res = document.createElement('div');
    res.style.minWidth = "400px";
    res.style.minHeight = "200px";
    res.classList.add("dialogWindow");
    setSGControlPanel(res);
    res.appendChild(getSGForm());
    res.appendChild(safePanel());    
    document.body.appendChild(res); 
    getSelectedPoints(res);
}

function setSGControlPanel(panel) {
    var res = document.createElement('div');
    res.setAttribute("onmousedown", "ReplaceControl(this.parentElement)");
    res.classList.add("dialogWindowControlPanel");
    var sp = document.createElement('span');
    sp.setAttribute("onselectstart", "return false");
    sp.innerText = "Справочник точек на распред устройствах";//
    res.appendChild(sp);
    var but = document.createElement('input');
    but.setAttribute("type", "submit");
    but.value = "Закрыть"
    but.setAttribute('onclick', 'removeDialogWindow()');
    res.appendChild(but);
    panel.appendChild(res);
}

function getSGForm() {
    var res = document.createElement('form');
    res.classList.add("treeHTML");
    res.style.minHeight = "115px";
    var sgp = switchgears;
    for (var i = 0; i < sgp.length; i++)
    {
        addSGToForm(sgp[i], res);
    }  
    setClicking(res);
    return res;
}

function addSGToForm(model, form) {
    var res = document.createElement("input");
    res.type = "checkbox";    
    var t = document.createTextNode(model.nameSw);
    var lab = document.createElement("label");
    lab.appendChild(res);
    lab.appendChild(t);
    lab.title = model.fullName;
    form.appendChild(lab);
    if (model.sections.length > 0) {
        form.appendChild(getSectionFieldSet(model.sections));
    }    
}

function getSectionFieldSet(model) {
    var res = document.createElement("fieldset");
    res.appendChild(getLegend());
    for (var i = 0; i < model.length; i++) {
        res.appendChild(getSectionCB(model[i]));
        if (model[i].points.length > 0) {
            res.appendChild(getPointsFieldSet(model[i].points));
        }
    }
    return res;
}

function getLegend() {
    var res = document.createElement("legend");
    res.setAttribute("onclick", "Razvernut(event)");
    return res;
}

function Razvernut(evt) {
    if (evt.target.parentElement.nodeName && evt.target.parentElement.nodeName == 'FIELDSET') {
        if (evt.target.parentElement.className && evt.target.parentElement.className == 'razvernut') {
            evt.target.parentElement.className = '';
        } else {
            evt.target.parentElement.className = 'razvernut';
        }
    }
}

function getSectionCB(model) {
    var res = document.createElement("input");
    res.type = "checkbox";
    var t = document.createTextNode(model.shortName);
    var lab = document.createElement("label");
    lab.appendChild(res);
    lab.appendChild(t);
    lab.title = model.fullName;
    return lab;
}

function getPointsFieldSet(model) {    
    var res = document.createElement("fieldset");
    res.appendChild(getLegend());
    for (var i = 0; i < model.length; i++) {
        res.appendChild(getPointCB(model[i]));
    }
    return res;
}

function getPointCB(model) {
    var res = document.createElement("input");
    res.type = "checkbox";
    res.setAttribute("pointId", model.id);
    res.setAttribute("point", JSON.stringify(model));
    var t = document.createTextNode(model.shortName);
    var lab = document.createElement("label");
    lab.appendChild(res);
    lab.appendChild(t);
    lab.title = model.fullName + "\n" + model.explantation;
    return lab;
}

function setClicking(t) {
    [].forEach.call(t.querySelectorAll('fieldset'), function (eFieldset) {
        var main = [].filter.call(t.querySelectorAll('[type="checkbox"]'), function (element) { return element.parentNode.nextElementSibling == eFieldset; });
        main.forEach(function (eMain) {
            var l = [].filter.call(eFieldset.querySelectorAll('legend'), function (e) { return e.parentNode == eFieldset; });
            l.forEach(function (eL) {
                var all = eFieldset.querySelectorAll('[type="checkbox"]');
                eL.onclick = Razvernut;
                eFieldset.onchange = Razvernut;
                function Razvernut() {
                    var allChecked = eFieldset.querySelectorAll('[type="checkbox"]:checked').length;
                    eMain.checked = allChecked == all.length;
                    eMain.indeterminate = allChecked > 0 && allChecked < all.length;
                    if (eMain.indeterminate || eMain.checked || ((eFieldset.className == '') && (allChecked == "0"))) {
                        eFieldset.className = 'razvernut';
                    } else {
                        eFieldset.className = '';
                    }
                }
                eMain.onclick = function () {
                    for (var i = 0; i < all.length; i++)
                        all[i].checked = this.checked;
                    if (this.checked) {
                        eFieldset.className = 'razvernut';
                    } else {
                        eFieldset.className = '';
                    }
                }
            });
        });
    });
}

function safePanel() {
    var res = document.createElement("div");
    res.style.width = "100%";
    res.style.height = "35px";
    res.align = "center";
    var but = document.createElement('input');
    but.setAttribute("type", "submit");
    but.value = "Сохранить"
    but.setAttribute('onclick', 'setSelectedPoints(event)');
    res.appendChild(but);
    return res;
}

function setSelectedPoints(event) {
    var dw = event.target.parentElement.parentElement;
    var sgf = dw.querySelector("form");
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
    }
    try
    {
        setSelectedPointInTable();
    } catch{ }    
    removeDialogWindow();
}

function getPointSpan(check) {
    var res = document.createElement("span");
    var point = JSON.parse(check.getAttribute("point"));
    res.innerText = point.fullName +"\n"+point.explantation;
    res.setAttribute("point", check.getAttribute("point"));
    res.title = point.shortName;
    res.classList.add("graphColumnPoint");
    return res;
}

function getSelectedPoints(div) {
    var vpal = document.querySelector('div[name="pointsPallete"]');
    for (var i = 0; i<vpal.children.length ;  i++) {
        var node = vpal.children[i];
        if (node.hasAttribute('point')) {
            var pm = JSON.parse(node.getAttribute("point"));
            var check = div.querySelector("input[type='checkbox'][pointId='" + pm.id +"']");
            check.checked = true;                        
        }
    }
    setViewSGCheckState(div);
}

function setViewSGCheckState(t) {
    [].forEach.call(t.querySelectorAll('fieldset'), function (eFieldset) {
        var main = [].filter.call(t.querySelectorAll('[type="checkbox"]'), function (element) { return element.parentNode.nextElementSibling == eFieldset; });
        main.forEach(function (eMain) {
            var l = [].filter.call(eFieldset.querySelectorAll('legend'), function (e) { return e.parentNode == eFieldset; });
            l.forEach(function (eL) {
                var all = eFieldset.querySelectorAll('[type="checkbox"]');
                var allChecked = eFieldset.querySelectorAll('[type="checkbox"]:checked').length;
                eMain.checked = allChecked == all.length;
                eMain.indeterminate = allChecked > 0 && allChecked < all.length;
                if (eMain.indeterminate || eMain.checked || ((eFieldset.className == '') && (allChecked == 0))) {
                    eFieldset.className = 'razvernut';
                } else {
                    eFieldset.className = '';
                }
            });
        });
    });
}

function getSGPointDialog(event) {
    var cell = event.target;
    var res = document.createElement('div');
    res.style.minWidth = "400px";
    res.style.minHeight = "200px";
    res.classList.add("dialogWindow");
    setSGControlPanel(res);
    res.appendChild(getSGPointForm(cell));
    document.body.appendChild(res); 
}

function getSGPointForm(cell) {
    removeDialogWindow();
    var res = document.createElement('form');
    res.classList.add("treeHTML");
    res.style.minHeight = "115px";
    var sgp = switchgears;
    for (var i = 0; i < sgp.length; i++) {
        addSGToPontForm(sgp[i], res, cell);
    }    
    return res;
}

function addSGToPontForm(model, form, cell) {    
    var t = document.createTextNode(model.nameSw);
    var lab = document.createElement("label");    
    lab.appendChild(t);
    lab.title = model.fullName;
    form.appendChild(lab);
    if (model.sections.length > 0) {
        form.appendChild(getSectionPointFieldSet(model.sections, cell));
    }
}

function getSectionPointFieldSet(model, cell) {
    var res = document.createElement("fieldset");
    res.appendChild(getLegend());
    for (var i = 0; i < model.length; i++) {
        res.appendChild(getSectionPointCB(model[i]));
        if (model[i].points.length > 0) {
            res.appendChild(getPointsRBFieldSet(model[i].points, cell));
        }
    }
    return res;
}

function getSectionPointCB(model) {    
    var t = document.createTextNode(model.shortName);
    var lab = document.createElement("label");   
    lab.appendChild(t);
    lab.title = model.fullName;
    return lab;
}

function getPointsRBFieldSet(model, cell) {
    var res = document.createElement("fieldset");
    res.appendChild(getLegend());
    for (var i = 0; i < model.length; i++) {
        res.appendChild(getPointRB(model[i], cell));
    }
    return res;
}

function getPointRB(model, cell) {
    var res = document.createElement("input");
    res.type = "radio";
    res.setAttribute("name", "PointRB");
    res.onclick = function () {
        PointRBClick(model, cell);
        res.parentElement.parentElement.parentElement.parentElement.parentElement.remove();
    };    
    var t = document.createTextNode(model.shortName);
    var lab = document.createElement("label");
    lab.appendChild(res);
    lab.appendChild(t);
    lab.title = model.fullName + "\n" + model.explantation;
    return lab;
}

function PointRBClick(model, cell) {
    cell.setAttribute("point", JSON.stringify(model));
    cell.innerText = model.fullName;
}

function addNewRow(event){
    var row = event.target.parentElement.parentElement;
    var copyRow = row.cloneNode(true);
    for (var i = 0; i < copyRow.cells.length; i++) {
        var cell = copyRow.cells[i]
        if (i == 0) {
            cell.removeAttribute("point");
            
        }
        if (i < 4) {
            cell.innerText = "";
        }        
    }
    row.parentElement.appendChild(copyRow);
    var button = row.cells[4].querySelector("input[type='button']");
    button.value = "удалить";
    button.onclick = function () {
        row.remove();
    };

   
}

function clearRows(event) {
    var table = event.target.parentElement.parentElement.parentElement.parentElement;
    var rda = [];
    for (var i = 1; i < table.rows.length - 1; i++) {
        rda.push(table.rows[i]);
    }
    for (var i = 0; i < table.rows[table.rows.length - 1].cells.length; i++) {
        var cell = table.rows[table.rows.length - 1].cells[i]
        if (i == 0) {
            cell.removeAttribute("point");

        }
        if (i < 4) {
            cell.innerText = "";
        }
    }
    for (var i = 0; i < rda.length; i++) {
        rda[i].remove();
    }
    
}



