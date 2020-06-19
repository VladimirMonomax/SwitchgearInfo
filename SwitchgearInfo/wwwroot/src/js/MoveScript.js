function typeBrowser() {
    var browser = {
        ie: 0,
        op: 0,
        ff: 0
    };
    return browser;
}

function PointXY() {
    var xy = {
        x: 0,
        y: 0
    };

    return xy;
}

function PixelPoint() {
    var pxy = {
        px: '0px',
        py: '0px'
    };

    return pxy;
}

function clearXY() {
    document.onmousemove = null;
}

function GetTypeBrowser() {
    var browserT = typeBrowser();
    var browser = navigator.userAgent;
    if (browser.indexOf('Opera') != -1) browserT.op = 1;
    else {
        if (browser.indexOf('MSIE') != -1) browserT.ie = 1;
        else {
            if (browser.indexOf('Firefox') != -1) browserT.ff = 1;
        }
    }

    return browserT;
}

function GetObjPoint(obj_event) {
    var point = PointXY();
    var browser = GetTypeBrowser();
    if (obj_event) {
        point.x = obj_event.pageX;
        point.y = obj_event.pageY;
    }
    else {
        point.x = window.event.clientX;
        point.y = window.event.clientY;
        if (browser.ie) {
            point.y -= 2;
            point.x -= 2;
        }
    }

    return point;
}

function GetBlockPoint(obj) {
    var point = PointXY();
    point.x = obj.offsetLeft;
    point.y = obj.offsetTop;
    return point;
}

function GetDeltaPoint(obj1, obj2) {
    var delta = PointXY();
    var point = GetObjPoint(obj1);
    var blockP = GetBlockPoint(obj2);
    delta.x = blockP.x - point.x;
    delta.y = blockP.y - point.y;
    return delta;
}

function GetPixelPoint(Delta, obj_event) {
    var ppoint = PixelPoint();
    var point = GetObjPoint(obj_event);
    new_x = Delta.x + point.x;
    new_y = Delta.y + point.y;
    ppoint.px = new_x + 'px';
    ppoint.py = new_y + 'px';
    return ppoint;
}

function SetPPoint(ppoint, obj) {
    obj.style.top = ppoint.py;
    obj.style.left = ppoint.px;
}

function ReplaceControl(block) {

    var browser = GetTypeBrowser();
    delta_x = 0;
    delta_y = 0;
    var delta = PointXY();

    block.onmousedown = saveXY;
    if (browser.op || browser.ff) {
        block.addEventListener('onmousedown', saveXY, false);
    }
    document.onmouseup = clearXY;  

    function moveBlock(obj_event) {
        var ppoint = GetPixelPoint(delta, obj_event);
        SetPPoint(ppoint, block);
    }

    function saveXY(obj_event) {
        delta = GetDeltaPoint(obj_event, block);
        /* При движении курсора устанавливаем вызов функции moveWindow */
        document.onmousemove = moveBlock;
        if (browser.op || browser.ff)
            document.addEventListener('onmousemove', moveBlock, false);
    }   
}

function SetCordinateByObj(obj, obj_Id) {
    var robj = document.getElementById(obj_Id);
    var point = GetObjPoint(obj);
    var ppoint = GetPixelPoint(PointXY(), obj);
    SetPPoint(ppoint, robj);
}